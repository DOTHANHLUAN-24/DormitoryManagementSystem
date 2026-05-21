using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ xác thực người dùng (AuthService).
    /// </summary>
    public class AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        /// <summary>
        /// Thực hiện xác thực đăng nhập tài khoản và sinh JWT Token.
        /// </summary>
        /// <param name="loginRequest">Yêu cầu đăng nhập</param>
        /// <returns>Thông tin LoginResponse hoặc null nếu thông tin đăng nhập sai</returns>
        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);

            if (user == null || user.PasswordHash != loginRequest.Password)
            {
                return null;
            }

            var userRole = user.Role switch
            {
                Domain.Enums.UserRole.Admin => "Admin",
                Domain.Enums.UserRole.ManagementStaff => "ManagementStaff",
                Domain.Enums.UserRole.TechnicalStaff => "TechnicalStaff",
                Domain.Enums.UserRole.Student => "Student",
                _ => "Student"
            };
            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.UserName!, userRole);

            return new LoginResponse
            {
                Token = token,
                Username = user.UserName!,
                Role = userRole,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60) // Khớp với cấu hình Jwt
            };
        }
    }
}
