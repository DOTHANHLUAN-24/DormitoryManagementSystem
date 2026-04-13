# 🔑 Hướng dẫn Authentication & Roles Setup

## Mục lục
1. [JWT Authentication Setup](#jwt-authentication-setup)
2. [Khởi tạo Roles](#khởi-tạo-roles)
3. [Middleware Configuration](#middleware-configuration)
4. [Seeding Data](#seeding-data)

---

## JWT Authentication Setup

### 1. Cài đặt Packages

```bash
# NuGet Package Manager
Install-Package System.IdentityModel.Tokens.Jwt
Install-Package Microsoft.IdentityModel.Tokens
```

Hoặc qua .csproj:
```xml
<ItemGroup>
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.0.0" />
    <PackageReference Include="Microsoft.IdentityModel.Tokens" Version="7.0.0" />
</ItemGroup>
```

---

### 2. Cấu hình appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=DormitoryManagementDB;Trusted_Connection=true;Encrypt=false;"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-min-32-characters-long-here",
    "Issuer": "DormitoryManagement",
    "Audience": "DormitoryManagementUsers",
    "ExpirationMinutes": 60
  },
  "AllowedHosts": "*"
}
```

**⚠️ Quan trọng**: 
- `SecretKey` phải tối thiểu 32 ký tự
- Trong production, lưu trữ trong `User Secrets` hoặc environment variables
- **KHÔNG** commit SecretKey vào git

---

### 3. Tạo JWT Generator Service

**Tệp**: `Services/Implementations/JwtTokenService.cs`

```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DormitoryManagement.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;

namespace DormitoryManagement.Services.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtTokenService> _logger;

        public JwtTokenService(IConfiguration configuration, ILogger<JwtTokenService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GenerateAccessTokenAsync(User user, IList<string> roles)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key not configured")
                ));

                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("PhoneNumber", user.PhoneNumber ?? "")
                };

                // Thêm roles vào claims
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(
                        int.Parse(jwtSettings["ExpirationMinutes"] ?? "60")
                    ),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi tạo JWT token: {ex.Message}");
                throw;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? "")
                    ),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi lấy principal từ token: {ex.Message}");
                return null;
            }
        }
    }
}
```

---

### 4. Tạo Interface IJwtTokenService

**Tệp**: `Services/Interfaces/IJwtTokenService.cs`

```csharp
using DormitoryManagement.Data.Entities;
using System.Security.Claims;

namespace DormitoryManagement.Services.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateAccessTokenAsync(User user, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
```

---

### 5. Cấu hình trong Program.cs

```csharp
// Thêm sau AddIdentity
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? "")
        )
    };
});

// Register Jwt Token Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
```

---

## Khởi tạo Roles

### 1. Tạo RoleSeeder Service

**Tệp**: `Services/Implementations/RoleSeederService.cs`

```csharp
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Services.Implementations
{
    public class RoleSeederService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleSeederService> _logger;

        public RoleSeederService(RoleManager<IdentityRole> roleManager, ILogger<RoleSeederService> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedRolesAsync()
        {
            var roles = new[]
            {
                "Admin",        // Quản trị viên
                "Manager",      // Quản lý
                "Accountant",   // Kế toán
                "Security",     // Bảo vệ
                "Student"       // Sinh viên
            };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"Role '{role}' đã được tạo thành công");
                    }
                    else
                    {
                        _logger.LogError($"Lỗi tạo role '{role}': {string.Join(", ", result.Errors)}");
                    }
                }
                else
                {
                    _logger.LogInformation($"Role '{role}' đã tồn tại");
                }
            }
        }
    }
}
```

---

### 2. Gọi RoleSeeder trong Program.cs

```csharp
// Sau khi build app
using (var scope = app.Services.CreateScope())
{
    var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeederService>();
    await roleSeeder.SeedRolesAsync();
}

app.Run();
```

---

## Middleware Configuration

### 1. Cấu hình Authentication Middleware

Trong `Program.cs`, đảm bảo thứ tự middleware:

```csharp
// ĐÚNG - Thứ tự quan trọng
app.UseRouting();

app.UseAuthentication();  // ← Phải trước UseAuthorization
app.UseAuthorization();   // ← Phải sau UseAuthentication

app.MapControllers();
app.Run();
```

### 2. Thêm CORS nếu Frontend khác domain

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000", "https://yourdomain.com")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Trong app.MapControllers() trước
app.UseCors("AllowFrontend");
```

---

## Seeding Data

### Tạo Admin user tự động

**Tệp**: `Services/Implementations/AdminSeederService.cs`

```csharp
using DormitoryManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Services.Implementations
{
    public class AdminSeederService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AdminSeederService> _logger;

        public AdminSeederService(UserManager<User> userManager, ILogger<AdminSeederService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task SeedAdminUserAsync()
        {
            var adminExists = await _userManager.FindByNameAsync("admin@dormitory");
            
            if (adminExists != null)
            {
                _logger.LogInformation("Admin user đã tồn tại");
                return;
            }

            var adminUser = new User(
                Guid.NewGuid().ToString(),
                "admin@dormitory",
                "System",
                "Administrator",
                "admin@dormitory.local",
                "0000000000"
            );

            var result = await _userManager.CreateAsync(adminUser, "Admin@12345");
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                _logger.LogInformation("Admin user đã được tạo thành công");
            }
            else
            {
                _logger.LogError($"Lỗi tạo admin user: {string.Join(", ", result.Errors)}");
            }
        }
    }
}
```

Gọi trong Program.cs:

```csharp
using (var scope = app.Services.CreateScope())
{
    var adminSeeder = scope.ServiceProvider.GetRequiredService<AdminSeederService>();
    await adminSeeder.SeedAdminUserAsync();
}
```

---

## Checklist cấu hình

- [ ] Cài đặt JWT NuGet packages
- [ ] Cấu hình appsettings.json (Secret Key, Issuer, Audience)
- [ ] Tạo JwtTokenService
- [ ] Thêm JWT Authentication vào Program.cs
- [ ] Tạo RoleSeederService
- [ ] Gọi SeedRolesAsync() trong Program.cs
- [ ] Kiểm tra thứ tự middleware (Authentication trước Authorization)
- [ ] Test tạo token qua login
- [ ] Test protected endpoints
- [ ] Cấu hình CORS nếu cần

---

## Lỗi thường gặp

| Lỗi | Nguyên nhân | Giải pháp |
|-----|-----------|----------|
| "Unauthorized" trên protected endpoint | Không gửi JWT token | Thêm header `Authorization: Bearer {token}` |
| "Invalid token" | Token hết hạn hoặc sai secret key | Tạo token mới |
| Token không có roles | Roles không được thêm vào claims | Kiểm tra JwtTokenService |
| 401 trên tất cả endpoint | Middleware sai thứ tự | Kiểm tra Authentication trước Authorization |
| Role không hoạt động | Role không được tạo trong database | Chạy RoleSeederService |

---
