using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DormitoryManagement.Application.Dtos.Requests; // Đảm bảo folder Dtos hay DTOs viết đúng chính tả nhé
using DormitoryManagement.Application.Dtos.Requests.Authentications;
using DormitoryManagement.Application.Services.Interfaces;
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
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
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

        // --- QUÊN MẬT KHẨU ---

        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // MÔI TRƯỜNG DEV: Báo lỗi để biết email không tồn tại trong DB.
                // LƯU Ý: Khi lên thực tế (Production), bạn nên ẩn thông báo này để bảo mật thông tin người dùng.
                ModelState.AddModelError(string.Empty, "Email này không tồn tại trong hệ thống.");
                return View(request);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { token, email = request.Email }, Request.Scheme);

            // Nội dung Email HTML custom
            string content = $@"
            <div style='font-family: Arial; padding: 20px; border: 1px solid #eee;'>
                <h2 style='color: #006b73;'>Yêu cầu cấp lại mật khẩu</h2>
                <p>Chào {user.FullName},</p>
                <p>Bạn vừa yêu cầu đặt lại mật khẩu cho tài khoản tại DMS.</p>
                <a href='{callbackUrl}' style='display:inline-block; background:#006b73; color:#fff; padding:10px 20px; text-decoration:none; border-radius:5px;'>Đặt lại mật khẩu</a>
                <p>Nếu không phải bạn yêu cầu, hãy bỏ qua mail này.</p>
            </div>";

            try
            {
                await _emailService.SendEmailAsync(request.Email, "Khôi phục mật khẩu DMS", content);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi gửi Email (Kiểm tra lại cấu hình SMTP): {ex.Message}");
                return View(request);
            }

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [HttpGet]
        [Route("ForgotPasswordConfirmation")]
        public IActionResult ForgotPasswordConfirmation() => View();


        // --- ĐẶT LẠI MẬT KHẨU ---

        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string token = null!, string email = null!)
        {
            if (token == null || email == null) return BadRequest("Token hoặc Email không hợp lệ");

            var model = new ResetPasswordRequest { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return RedirectToAction("ResetPasswordConfirmation");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(request);
        }

        [HttpGet]
        [Route("ResetPasswordConfirmation")]
        public IActionResult ResetPasswordConfirmation() => View();
    }
}