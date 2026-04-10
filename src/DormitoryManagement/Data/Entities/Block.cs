using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("Blocks")]
    public class Block
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string BlockName { get; set; } = string.Empty;

        public string? Description { get; set; }
        
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
