using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Users
{
    public class UserUpdateDto
    {
        // Giữ lại ID để biết đang cập nhật ai (không bắt buộc nếu dùng id trên URL, nhưng nên có)
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Số CCCD không được để trống")]
        public string IdentityCardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mã số không được để trống")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn vai trò")]
        public UserRole Role { get; set; }

        public string? UserName { get; set; }

        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; } // Cho phép Null (Trống = Không đổi)

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string? ConfirmPassword { get; set; }
    }
}