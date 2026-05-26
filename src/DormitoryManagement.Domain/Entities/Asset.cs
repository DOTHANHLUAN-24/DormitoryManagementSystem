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
        [Required, StringLength(100)]
        public string AssetName { get; set; } = string.Empty;

        [StringLength(50)]
        public string AssetCode { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public AssetStatus Status { get; set; } = AssetStatus.Good;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ReplacementCost { get; set; } = 0;

        public Guid RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }
}
