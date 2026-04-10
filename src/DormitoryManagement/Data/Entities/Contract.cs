using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("Contracts")]
    public class Contract
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = null!;

        public int RoomId { get; set; }
        
        public Room Room { get; set; } = null!;

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public decimal Deposit { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
