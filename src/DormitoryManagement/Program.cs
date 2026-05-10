using System.Security.Claims;
using System.Text;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Services;
using DormitoryManagement.Application.Services.Implements;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using DormitoryManagement.Infrastructure.ExternalServices;
using DormitoryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.Configure<MailSettings>(
            builder.Configuration.GetSection("MailSettings"));

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Dependency Injection Repositories and Services
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IContractRepository, ContractRepository>();

        builder.Services.AddScoped<IRoomService, RoomService>();
        builder.Services.AddScoped<IContractService, ContractService>();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        // Check biến môi trường và chuỗi kết nối
        Console.WriteLine($"ENV: {builder.Environment.EnvironmentName}");
        Console.WriteLine($"CONN: {builder.Configuration.GetConnectionString("DefaultConnection")}");
        Console.WriteLine(builder.Configuration["ConnectionStrings:DefaultConnection"]);

        builder.Services
            .AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["JwtSettings:Audience"],

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                // 🔥 QUAN TRỌNG để Navbar nhận đúng
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            };

            options.Events = new JwtBearerEvents
            {
                // 🔥 Lấy token từ Cookie
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["JWTToken"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token;
                    }
                    return Task.CompletedTask;
                },

                // ❌ Tránh redirect vòng lặp API
                OnChallenge = context =>
                {
                    context.HandleResponse();

                    // Nếu là request từ trình duyệt → redirect login
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = 401;
                    }
                    else
                    {
                        context.Response.Redirect("/Account/Login");
                    }

                    return Task.CompletedTask;
                }
            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        if (app.Environment.IsDevelopment())
        {
            await DbSeeder.SeedAsync(app.Services);
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
