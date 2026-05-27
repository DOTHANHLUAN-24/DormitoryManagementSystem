using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể tài sản/trang thiết bị trong phòng của ký túc xá.
    /// </summary>
    [Table("Assets")]
    public class Asset : BaseEntity
    {
        /// <summary>Tên tài sản (ví dụ: Quạt điện, Đèn LED, Tủ đầu giường).</summary>
        [Required, StringLength(100)]
        public string AssetName { get; set; } = string.Empty;

        /// <summary>Mã tài sản dùng để định danh nội bộ (ví dụ: TS-001, FAN-002).</summary>
        [StringLength(50)]
        public string AssetCode { get; set; } = string.Empty;

        /// <summary>Mô tả chi tiết về tài sản, xuất xứ, tình trạng hoặc ghi chú đặc biệt.</summary>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>Tình trạng hiện tại của tài sản (Good = Tốt, Damaged = Hỏng, UnderRepair = Đang sửa, Disposed = Thanh lý).</summary>
        public AssetStatus Status { get; set; } = AssetStatus.Good;

        /// <summary>Chi phí thay thế ước tính khi tài sản bị hỏng hoặc mất (đơn vị: VNĐ).</summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal ReplacementCost { get; set; } = 0;

        /// <summary>Khóa ngoại trỏ đến phòng chứa tài sản này.</summary>
        public Guid RoomId { get; set; }

        /// <summary>Navigation property đến phòng chứa tài sản.</summary>
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }
}
