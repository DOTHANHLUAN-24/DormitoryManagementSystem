using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DormitoryManagement.Application.Dtos.Requests; // Đảm bảo folder Dtos hay DTOs viết đúng chính tả nhé
using DormitoryManagement.Application.Dtos.Requests.Authentications;
using DormitoryManagement.Application.Dtos.Requests.Users;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DormitoryManagement.Controllers
{
    [Route("Account")]
    [AllowAnonymous]
    public class AccountController
    (
        UserManager<User> userManager,
        IUserService userService,
        IConfiguration configuration,
        IEmailService emailService
    ) : BaseController
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IUserService _userService = userService;
        private readonly IEmailService _emailService = emailService;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet("Register")]
        public IActionResult Register()
        {
            Logger.LogInformation("Đang truy cập trang đăng ký tài khoản.");
            // Đã đăng nhập rồi thì không cho vào trang đăng ký nữa, chuyển hướng về Home
            if (User.Identity!.IsAuthenticated)
            {
                Logger.LogInformation("Người dùng đã đăng nhập, chuyển hướng từ trang đăng ký về Home.");
                return RedirectToAction("Index", "Home");
            }
            return View(new UserRequestDto());
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRequestDto request)
        {
            Logger.LogInformation("Đang thực hiện đăng ký tài khoản cho email: {Email}.", request.Email);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu đăng ký tài khoản không hợp lệ.");
                return View(request);
            }

            try
            {
                // Mặc định Role là sinh viên khi đăng ký ngoài
                request.Role = UserRole.Student;

                var result = await _userService.CreateUserAsync(request);
                if (result)
                {
                    Logger.LogInformation("Đăng ký tài khoản thành công cho email: {Email}.", request.Email);
                    TempData["Success"] = "Đăng ký thành công! Mời bạn đăng nhập.";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi đăng ký tài khoản cho email: {Email}.", request.Email);
                ModelState.AddModelError("", ex.Message);
            }
            return View(request);
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            Logger.LogInformation("Đang truy cập trang đăng nhập.");
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            Logger.LogInformation("Đang thực hiện đăng nhập cho tài khoản: {Username}.", request.Username);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu đăng nhập không hợp lệ.");
                return View(request);
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null && request.Username.Contains('@'))
            {
                user = await _userManager.FindByEmailAsync(request.Username);
            }

            if (user == null)
            {
                Logger.LogWarning("Đăng nhập thất bại: Tài khoản {Username} không tồn tại.", request.Username);
                ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại!");
                return View(request);
            }

            if (!user.IsActive)
            {
                Logger.LogWarning("Đăng nhập thất bại: Tài khoản {Username} đã bị khóa hoặc chưa kích hoạt.", request.Username);
                ModelState.AddModelError(string.Empty, "Tài khoản của bạn đã bị khóa hoặc chưa kích hoạt!");
                return View(request);
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                Logger.LogWarning("Đăng nhập thất bại: Mật khẩu sai cho tài khoản {Username}.", request.Username);
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

            Logger.LogInformation("Đăng nhập thành công cho tài khoản {Username}. Token đã được ghi vào Cookie.", request.Username);
            return RedirectToAction("Index", "Home");
        }

        private string CreateToken(User user)
        {
            var keyStr = _configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key is missing");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new (JwtRegisteredClaimNames.UniqueName, user.UserName ?? "Unknown"),
                new (ClaimTypes.Name, user.FullName), // Để hiển thị tên thật lên giao diện
                new (ClaimTypes.Role, user.Role.ToString()),
                new ("UserId", user.Id.ToString())
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

        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Logger.LogInformation("Người dùng {Username} yêu cầu đăng xuất.", CurrentUserName);
            Response.Cookies.Delete("JWTToken", new CookieOptions
            {
                Path = "/",
                SameSite = SameSiteMode.Strict,
                Secure = false
            });

            Logger.LogInformation("Đã xóa cookie JWTToken. Đăng xuất thành công.");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            Logger.LogInformation("Đang truy cập trang quên mật khẩu.");
            return View();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            Logger.LogInformation("Đang xử lý yêu cầu quên mật khẩu cho email: {Email}.", request.Email);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Yêu cầu quên mật khẩu có dữ liệu không hợp lệ.");
                return View(request);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // MÔI TRƯỜNG DEV: Báo lỗi để biết email không tồn tại trong DB.
                // LƯU Ý: Khi lên thực tế (Production), bạn nên ẩn thông báo này để bảo mật thông tin người dùng.
                Logger.LogWarning("Yêu cầu quên mật khẩu thất bại: Không tìm thấy người dùng có email {Email}.", request.Email);
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

            // Ghi nhận link khôi phục mật khẩu ra log hệ thống (Hỗ trợ test nhanh trên Render Free Tier khi cổng SMTP bị chặn)
            Logger.LogWarning(">>> LINK KHÔI PHỤC MẬT KHẨU (Email: {Email}): {Url} <<<", request.Email, callbackUrl);

            try
            {
                await _emailService.SendEmailAsync(request.Email, "Khôi phục mật khẩu DMS", content);
                Logger.LogInformation("Đã gửi email hướng dẫn khôi phục mật khẩu đến {Email}.", request.Email);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi gửi email khôi phục mật khẩu đến {Email}.", request.Email);
                ModelState.AddModelError(string.Empty, $"Lỗi gửi Email (Kiểm tra lại cấu hình SMTP hoặc xem link reset trong Render Log): {ex.Message}");
                return View(request);
            }

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [HttpGet("ForgotPasswordConfirmation")]
        public IActionResult ForgotPasswordConfirmation()
        {
            Logger.LogInformation("Đang truy cập trang xác nhận yêu cầu quên mật khẩu.");
            return View();
        }


        // --- ĐẶT LẠI MẬT KHẨU ---
        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token = null!, string email = null!)
        {
            Logger.LogInformation("Đang truy cập trang đặt lại mật khẩu với email {Email}.", email);
            if (token == null || email == null)
            {
                Logger.LogWarning("Token hoặc Email đặt lại mật khẩu bị thiếu.");
                return BadRequest("Token hoặc Email không hợp lệ");
            }

            var model = new ResetPasswordRequest { Token = token, Email = email };
            return View(model);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            Logger.LogInformation("Đang xử lý yêu cầu đặt lại mật khẩu cho email {Email}.", request.Email);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu đặt lại mật khẩu không hợp lệ.");
                return View(request);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                Logger.LogWarning("Không tìm thấy người dùng có email {Email} để đặt lại mật khẩu.", request.Email);
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (result.Succeeded)
            {
                Logger.LogInformation("Đặt lại mật khẩu thành công cho email {Email}.", request.Email);
                return RedirectToAction("ResetPasswordConfirmation");
            }

            Logger.LogWarning("Đặt lại mật khẩu thất bại cho email {Email} do lỗi từ Identity.", request.Email);
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(request);
        }

        [HttpGet("ResetPasswordConfirmation")]
        public IActionResult ResetPasswordConfirmation()
        {
            Logger.LogInformation("Đang truy cập trang xác nhận đặt lại mật khẩu thành công.");
            return View();
        }

        [HttpGet("Terms")]
        public IActionResult Terms()
        {
            Logger.LogInformation("Đang truy cập trang điều khoản điều kiện.");
            return View();
        }
    }
}