using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Users
{
    public class UserRequestDto
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")] // Quan trọng
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mã số (MSSV/NV) là bắt buộc")]
        public string Code { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "CCCD là bắt buộc")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "CCCD không hợp lệ")]
        public string IdentityCardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn vai trò")]
        public UserRole Role { get; set; }

    }
}
