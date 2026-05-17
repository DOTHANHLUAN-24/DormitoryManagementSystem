using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests.RoomTypes
{
    public class RoomTypeRequestDto
    {
        [Required(ErrorMessage = "Tên loại phòng không được để trống")]
        [StringLength(50, ErrorMessage = "Tên loại phòng không quá 50 ký tự")]
        [Display(Name = "Tên loại phòng")]
        public string TypeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Đơn giá cơ bản là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải là số dương")]
        [Display(Name = "Giá thuê cơ bản")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "Số người tối đa là bắt buộc")]
        [Range(1, 20, ErrorMessage = "Số người ở phải từ 1 đến 20")]
        [Display(Name = "Số người ở tối đa")]
        public int MaxOccupants { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; } = string.Empty;
    }
}