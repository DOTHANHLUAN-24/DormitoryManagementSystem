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
        
        [Required, StringLength(12)]
        public string IdNumber { get; set; } = string.Empty; // Số CMND/CCCD
        
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty; // Số điện thoại
        
        [StringLength(100)]
        public string Relationship { get; set; } = string.Empty; // Mối quan hệ với sinh viên
        
        [StringLength(50)]
        public string Status { get; set; } = "Chờ duyệt"; // "Chờ duyệt", "Đang ở trong", "Đã rời đi", "Từ chối", "Quá giờ"
        
        public bool IsCheckedOut { get; set; } = false;

        public DateTime CheckInTime { get; set; }
        
        public DateTime? CheckOutTime { get; set; }
        
        public string Purpose { get; set; } = string.Empty; // Mục đích thăm

        public Guid HostId { get; set; } // Người được thăm
        
        [ForeignKey("HostId")]
        public virtual User Host { get; set; } = null!;
    }
}
