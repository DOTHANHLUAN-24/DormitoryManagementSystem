using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("MaintenanceRequests")]
    public class MaintenanceRequest
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        
        public Room Room { get; set; } = null!;

        [Required]
        public string Description { get; set; } = string.Empty;
        
        public string Status { get; set; } = "Pending";
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
