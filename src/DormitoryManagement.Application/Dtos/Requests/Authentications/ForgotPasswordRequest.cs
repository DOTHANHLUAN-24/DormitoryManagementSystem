using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests.Authentications
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;
    }
}
