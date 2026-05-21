using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests.Utilities
{
    public class UtilityRequestDto
    {
        [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
        [StringLength(100, ErrorMessage = "Tên dịch vụ không quá 100 ký tự")]
        [Display(Name = "Tên dịch vụ")]
        public string UtilityName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        [Display(Name = "Đơn giá")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Đơn vị tính không được để trống")]
        [StringLength(50, ErrorMessage = "Đơn vị tính không quá 50 ký tự")]
        [Display(Name = "Đơn vị tính")]
        public string Unit { get; set; } = string.Empty;

        [Display(Name = "Trạng thái hoạt động")]
        public bool IsActive { get; set; } = true;
    }
}
