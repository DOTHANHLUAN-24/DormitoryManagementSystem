using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho yêu cầu bảo trì, sửa chữa thiết bị hoặc phòng trong ký túc xá.
    /// Được tạo bởi sinh viên và xử lý bởi nhân viên kỹ thuật.
    /// </summary>
    [Table("MaintenanceRequests")]
    public class MaintenanceRequest : BaseEntity
    {
        /// <summary>Tiêu đề ngắn gọn mô tả sự cố cần sửa chữa (ví dụ: Sửa điều hòa, Thay bóng đèn). Tối đa 200 ký tự.</summary>
        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>Mô tả chi tiết về tình trạng sự cố, vị trí cụ thể và mức độ ảnh hưởng.</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Mức độ ưu tiên xử lý (Low = Thấp, Medium = Trung bình, High = Cao, Critical = Khẩn cấp).</summary>
        public MaintenancePriority Priority { get; set; }

        /// <summary>Trạng thái xử lý yêu cầu (Open = Mới, InProgress = Đang xử lý, Resolved = Đã xong, Closed = Đóng).</summary>
        public MaintenanceStatus Status { get; set; }

        /// <summary>Thời điểm yêu cầu được đánh dấu là đã xử lý xong. Null nếu chưa hoàn thành.</summary>
        public DateTime? ResolvedAt { get; set; }

        /// <summary>Khóa ngoại trỏ đến phòng phát sinh sự cố cần bảo trì.</summary>
        public Guid RoomId { get; set; }

        /// <summary>Khóa ngoại trỏ đến người dùng đã gửi yêu cầu bảo trì (thường là sinh viên).</summary>
        public Guid RequesterId { get; set; }

        /// <summary>Khóa ngoại trỏ đến nhân viên kỹ thuật được giao xử lý yêu cầu. Null nếu chưa phân công.</summary>
        public Guid? HandlerId { get; set; }

        /// <summary>Navigation property đến người gửi yêu cầu bảo trì.</summary>
        [ForeignKey("RequesterId")]
        public virtual User Requester { get; set; } = null!;

        /// <summary>Navigation property đến nhân viên kỹ thuật xử lý yêu cầu.</summary>
        [ForeignKey("HandlerId")]
        public virtual User? Handler { get; set; }

        /// <summary>Navigation property đến phòng phát sinh sự cố.</summary>
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }
}
