using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Domain.Entities
{
    // Loại phòng
    [Table("RoomTypes")]
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]

        public string TypeName { get; set; } = string.Empty; // Ví dụ: Phòng đơn, Phòng đôi, Phòng tập thể

        public decimal BasePrice { get; set; }

        public int MaxOccupants { get; set; } // Bằng số lượng Bed trong phòng

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
