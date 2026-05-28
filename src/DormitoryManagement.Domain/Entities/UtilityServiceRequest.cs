using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho yêu cầu đăng ký sử dụng dịch vụ/tiện ích của sinh viên cho phòng.
    /// </summary>
    [Table("UtilityServiceRequests")]
    public class UtilityServiceRequest : BaseEntity
    {
        /// <summary>Khóa ngoại trỏ đến phòng đăng ký dịch vụ.</summary>
        public Guid RoomId { get; set; }

        /// <summary>Khóa ngoại trỏ đến sinh viên gửi yêu cầu đăng ký.</summary>
        public Guid? RequesterId { get; set; }

        /// <summary>Khóa ngoại trỏ đến loại dịch vụ/tiện ích được đăng ký.</summary>
        public Guid UtilityId { get; set; }

        /// <summary>Trạng thái yêu cầu: Pending (Chờ duyệt), Approved (Đã duyệt), Rejected (Từ chối).</summary>
        [Required, StringLength(50)]
        public string Status { get; set; } = "Pending";

        /// <summary>Ghi chú hoặc phản hồi từ quản lý/sinh viên.</summary>
        public string? Notes { get; set; }

        /// <summary>Số lượng đăng ký sử dụng (mặc định là 1).</summary>
        public int Quantity { get; set; } = 1;

        /// <summary>Navigation property đến phòng.</summary>
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        /// <summary>Navigation property đến sinh viên gửi yêu cầu.</summary>
        [ForeignKey("RequesterId")]
        public virtual User Requester { get; set; } = null!;

        /// <summary>Navigation property đến dịch vụ tiện ích.</summary>
        [ForeignKey("UtilityId")]
        public virtual Utility Utility { get; set; } = null!;
    }
}
