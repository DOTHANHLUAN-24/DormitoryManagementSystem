using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DormitoryManagement.Application.Dtos.Requests; // Đảm bảo folder Dtos hay DTOs viết đúng chính tả nhé
using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DormitoryManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào (Validation)
            if (!ModelState.IsValid) return View(request);

            // 1. Kiểm tra tài khoản
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại!");
                return View(request);
            }

            // 2. Kiểm tra mật khẩu
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu sai rồi Luân ơi!");
                return View(request);
            }

            // 3. Tạo JWT Token
            var token = CreateToken(user);

            // 4. LƯU TOKEN VÀO COOKIE
            Response.Cookies.Append("JWTToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict, // Ngăn chặn tấn công CSRF
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            // Redirect về trang chủ sau khi thành công
            return RedirectToAction("Index", "Home");
        }

        private string CreateToken(User user)
        {
            // Lấy Key từ cấu hình, thêm dấu ! để báo với trình biên dịch là nó không null
            var keyStr = _configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key is missing");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Tạo các thông tin định danh (Claims)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? "Unknown"),
                new Claim(ClaimTypes.Name, user.FullName), // Để hiển thị tên thật lên giao diện
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost] // Logout nên dùng HttpPost để bảo mật hơn
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JWTToken");
            return RedirectToAction("Login");
        }
    }
}