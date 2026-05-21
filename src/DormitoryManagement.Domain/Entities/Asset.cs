using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể tài sản/trang thiết bị trong ký túc xá.
    /// </summary>
    [Table("Assets")]
    public class Asset : BaseEntity
    {
        /// <summary>Tên tài sản</summary>
        [Required, StringLength(100)]
        public string AssetName { get; set; } = string.Empty;

        /// <summary>Mã định danh tài sản (duy nhất)</summary>
        [StringLength(50)]
        public string AssetCode { get; set; } = string.Empty;

        /// <summary>Mô tả chi tiết tài sản</summary>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>Trạng thái hiện tại của tài sản</summary>
        public AssetStatus Status { get; set; } = AssetStatus.Good;

        /// <summary>Giá trị đền bù khi tài sản bị hỏng hoặc mất (VNĐ)</summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal ReplacementCost { get; set; } = 0;

        /// <summary>Phòng đang chứa tài sản này</summary>
        public Guid RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }
}
