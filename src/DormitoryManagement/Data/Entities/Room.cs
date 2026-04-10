using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("Rooms")]
    public class Room
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string RoomNumber { get; set; } = string .Empty;

        public int BlockId { get; set; }

        public Block Block { get; set; } = null!;

        public int RoomTypeId { get; set; }
        
        public RoomType RoomType { get; set; } = null!;

        public bool IsAvailable { get; set; } = true;
    }
}
