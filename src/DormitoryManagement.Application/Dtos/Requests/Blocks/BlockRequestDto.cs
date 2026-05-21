using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests.Blocks
{
    public class BlockRequestDto
    {
        [Required(ErrorMessage = "Tên tòa nhà không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tòa nhà không được vượt quá 100 ký tự")]
        public string BlockName { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Số tầng phải từ 1 đến 100")]
        public int TotalFloors { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}