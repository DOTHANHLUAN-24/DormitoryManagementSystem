using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    // Vị trí tòa nhà
    [Table("Blocks")]
    public class Block
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string BlockName { get; set; } = string.Empty; // Ví dụ: Khu A, Khu B
        public int TotalFloors { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
