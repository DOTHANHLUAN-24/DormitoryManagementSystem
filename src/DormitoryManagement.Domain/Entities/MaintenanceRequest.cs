using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Yêu cầu bảo trì
    [Table("MaintenanceRequests")]
    public class MaintenanceRequest : BaseEntity
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty; // Ví dụ: Sửa điều hòa, Thay bóng đèn

        public string Description { get; set; } = string.Empty;
        
        public MaintenancePriority Priority { get; set; }
        
        public MaintenanceStatus Status { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public Guid RoomId { get; set; }
        
        public Guid RequesterId { get; set; }

        public Guid? HandlerId { get; set; }

        [ForeignKey("RequesterId")]
        public virtual User Requester { get; set; } = null!;

        [ForeignKey("HandlerId")]
        public virtual User? Handler { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }
}
