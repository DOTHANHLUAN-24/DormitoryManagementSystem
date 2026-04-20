using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Data.Enums;

namespace DormitoryManagement.Data.Entities
{
    // Phòng
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty; // Ví dụ: P101, P102

        public int Floor { get; set; }
        
        public RoomStatus Status { get; set; } = RoomStatus.Available;

        public int BlockId { get; set; }
        
        [ForeignKey("BlockId")]
        public virtual Block Block { get; set; } = null!;

        public int RoomTypeId { get; set; }
        
        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomType { get; set; } = null!;

        // Liên kết
        public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();
        
        public virtual ICollection<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();
        
        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}
