using System.Security.Claims;
using System.Text;
using DormitoryManagement.Application;
using DormitoryManagement.Infrastructure;
using DormitoryManagement.Infrastructure.Data;
using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // MVC
        builder.Services.AddControllersWithViews();

        // Mail settings
        builder.Services.Configure<dynamic>(builder.Configuration.GetSection("MailSettings"));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Dependency Injection from respective projects
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = "RequestVerificationToken";
        });

        // Identity
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

        // JWT
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

                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["JWTToken"];
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token;
                    }
                    return Task.CompletedTask;
                },

                OnChallenge = context =>
                {
                    context.HandleResponse();

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

        // Load global PageSize config into BaseController
        DormitoryManagement.Controllers.BaseController.PageSize = app.Configuration.GetValue<int>("Pagination:PageSize", 5);

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
