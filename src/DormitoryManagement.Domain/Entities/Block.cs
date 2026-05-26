using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho một tòa nhà (khu/block) trong ký túc xá.
    /// </summary>
    [Table("Blocks")]
    public class Block : BaseEntity
    {
        [Required, StringLength(100)]
        public string BlockName { get; set; } = string.Empty; // Ví dụ: Khu A, Khu B
        
        public int TotalFloors { get; set; }
        
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
