using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Tài sản
    [Table("Assets")]
    public class Asset : BaseEntity
    {
        [Required, StringLength(100)]
        public string AssetName { get; set; } =  string.Empty;

        public string AssetCode { get; set; } = string.Empty;

        public AssetStatus Status { get; set; } = AssetStatus.Good;

        public Guid RoomId { get; set; }
        
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }
}
