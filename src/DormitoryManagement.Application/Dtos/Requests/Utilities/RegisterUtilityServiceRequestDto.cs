using System;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests.Utilities
{
    public class RegisterUtilityServiceRequestDto
    {
        [Required(ErrorMessage = "Vui lòng chọn dịch vụ tiện ích.")]
        public Guid UtilityId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 đến 100.")]
        public int Quantity { get; set; } = 1;

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự.")]
        public string? Notes { get; set; }
    }
}
