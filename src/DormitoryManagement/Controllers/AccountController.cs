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
    [Route("Account")]
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
        [Route("Login")]
        public IActionResult Login() => View();

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại!");
                return View(request);
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu sai rồi bạn ơi!");
                return View(request);
            }

            var token = CreateToken(user);

            Response.Cookies.Append("JWTToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // DEV phải false
                SameSite = SameSiteMode.Strict,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return RedirectToAction("Index", "Home");
        }

        private string CreateToken(User user)
        {
            var keyStr = _configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key is missing");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JWTToken", new CookieOptions
            {
                Path = "/",
                SameSite = SameSiteMode.Strict,
                Secure = false
            });

            return RedirectToAction("Index", "Home");
        }
    }
}