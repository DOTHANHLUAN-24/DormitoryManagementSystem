using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository; // Repository để truy vấn DB
        private readonly IJwtTokenGenerator _jwtTokenGenerator; // Interface tạo token (nằm ở Domain)

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);

            if (user == null || user.PasswordHash != loginRequest.Password)
            {
                return null;
            }

            if (user != null)
            {
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
            return new LoginResponse();
        }

    }
}
