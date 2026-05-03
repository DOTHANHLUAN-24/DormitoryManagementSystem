using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Phòng
    [Table("Rooms")]
    public class Room : BaseEntity
    {
        [Required, StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty; // Ví dụ: P101, P102

        public int Floor { get; set; }

        public RoomStatus Status { get; set; } = RoomStatus.Available;

        public Guid BlockId { get; set; }

        public Guid RoomTypeId { get; set; }

        // Liên kết
        public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();

        public virtual ICollection<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();

        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
        
        [ForeignKey("BlockId")]
        public virtual Block Block { get; set; } = null!;

        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomType { get; set; } = null!;
    }
}
