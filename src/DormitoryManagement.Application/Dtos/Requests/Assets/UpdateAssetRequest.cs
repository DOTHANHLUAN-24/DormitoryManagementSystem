using System;
using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Assets
{
    public class UpdateAssetRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên tài sản không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tài sản không được quá 100 ký tự")]
        public string AssetName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mã tài sản không được để trống")]
        [StringLength(50, ErrorMessage = "Mã tài sản không được quá 50 ký tự")]
        public string AssetCode { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Trạng thái tài sản không được để trống")]
        public AssetStatus Status { get; set; } = AssetStatus.Good;

        [Required(ErrorMessage = "Phòng không được để trống")]
        public Guid RoomId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá trị đền bù không hợp lệ")]
        public decimal ReplacementCost { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }
}