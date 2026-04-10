using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("RoomTypes")]
    public class RoomType
    {
        public int Id { get; set; }
        
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
        
        public int Capacity { get; set; }
    }
}
