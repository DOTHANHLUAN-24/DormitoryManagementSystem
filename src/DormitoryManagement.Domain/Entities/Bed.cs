using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Quản lý chi tiết từng giường - gần giống quân sự khu B (Giường có ghi tên)
    [Table("Beds")]
    public class Bed : BaseEntity
    {
        [Required, StringLength(20)]
        public string BedNumber { get; set; } = string.Empty; // Ví dụ: G01, G02 

        public BedStatus Status { get; set; } = BedStatus.Available;

        public Guid RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
