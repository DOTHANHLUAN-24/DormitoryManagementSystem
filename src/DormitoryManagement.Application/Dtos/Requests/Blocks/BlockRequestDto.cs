using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests.Blocks
{
    public class BlockRequestDto
    {
        [Required(ErrorMessage = "Tên tòa nhà không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tòa nhà không quá 100 ký tự")]
        [Display(Name = "Tên tòa nhà")]
        public string BlockName { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "Số tầng phải từ 1 đến 50")]
        [Display(Name = "Tổng số tầng")]
        public int TotalFloors { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; } = string.Empty;
    }
}