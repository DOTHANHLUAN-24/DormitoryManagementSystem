using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    // Quản lý chi tiết từng giường - gần giống quân sự khu B (Giường có ghi tên)
    [Table("Beds")]
    public class Bed
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(20)]
        public string BedNumber { get; set; } = string.Empty; // Ví dụ: G01, G02 

        public BedStatus Status { get; set; } = BedStatus.Available;

        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
