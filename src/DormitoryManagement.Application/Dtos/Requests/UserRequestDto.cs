using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests
{
    public class UserRequestDto
    {
        public string UserName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string IdentityCardNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Student;

        public bool IsActive { get; set; } = true;
    }
}
