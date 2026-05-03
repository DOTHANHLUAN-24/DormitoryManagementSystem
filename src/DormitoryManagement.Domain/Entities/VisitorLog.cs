using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Lưu dữ liệu khách đến thăm
    [Table("VisitorLogs")]
    public class VisitorLog : BaseEntity
    {
        [Required, StringLength(100)]
        public string VisitorName { get; set; } = string.Empty;
        
        public string IdNumber { get; set; } = string.Empty; // Số CMND/CCCD
        
        public DateTime CheckInTime { get; set; }
        
        public DateTime? CheckOutTime { get; set; }
        
        public string Purpose { get; set; } = string.Empty; // Mục đích thăm

        public Guid HostId { get; set; } // Người được thăm
        
        [ForeignKey("HostId")]
        public virtual User Host { get; set; } = null!;
    }
}
