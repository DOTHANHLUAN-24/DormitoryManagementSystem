using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Entities
{
    // Yêu cầu bảo trì
    [Table("MaintenanceRequests")]
    public class MaintenanceRequest
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty; // Ví dụ: Sửa điều hòa, Thay bóng đèn

        public string Description { get; set; } = string.Empty;
        
        public MaintenancePriority Priority { get; set; }
        
        public MaintenanceStatus Status { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? ResolvedAt { get; set; }

        public int RoomId { get; set; }
        
        [ForeignKey("RoomId")]
        
        public virtual Room Room { get; set; } = null!;

        public string RequesterId { get; set; } = string.Empty;
        
        [ForeignKey("RequesterId")]
        public virtual User Requester { get; set; } = null!;
    }
}
