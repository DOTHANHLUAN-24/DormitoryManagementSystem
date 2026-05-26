using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể lưu trữ thông tin khách vãng lai đến thăm sinh viên tại ký túc xá.
    /// Theo dõi check-in/check-out để đảm bảo an ninh nội khu.
    /// </summary>
    [Table("VisitorLogs")]
    public class VisitorLog : BaseEntity
    {
        /// <summary>Họ và tên đầy đủ của khách đến thăm. Tối đa 100 ký tự.</summary>
        [Required, StringLength(100)]
        public string VisitorName { get; set; } = string.Empty;

        /// <summary>Số CMND/CCCD của khách (tối đa 12 ký tự), dùng để xác minh danh tính khi vào cổng.</summary>
        [Required, StringLength(12)]
        public string IdNumber { get; set; } = string.Empty;

        /// <summary>Số điện thoại liên hệ của khách để xác nhận hoặc thông báo khi cần thiết. Tối đa 20 ký tự.</summary>
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>Mối quan hệ của khách với sinh viên được thăm (ví dụ: Cha/mẹ, Anh/chị/em, Bạn bè). Tối đa 100 ký tự.</summary>
        [StringLength(100)]
        public string Relationship { get; set; } = string.Empty;

        /// <summary>Trạng thái hiện tại của lượt thăm: "Chờ duyệt", "Đang ở trong", "Đã rời đi", "Từ chối", "Quá giờ".</summary>
        [StringLength(50)]
        public string Status { get; set; } = "Chờ duyệt";

        /// <summary>Cờ đánh dấu khách đã hoàn tất check-out (ra khỏi khu vực ký túc xá). True = đã ra về.</summary>
        public bool IsCheckedOut { get; set; } = false;

        /// <summary>Thời điểm khách đăng ký vào hoặc thực tế được cho vào khu ký túc xá.</summary>
        public DateTime CheckInTime { get; set; }

        /// <summary>Thời điểm khách ra về (check-out). Null nếu khách chưa rời đi hoặc chưa được ghi nhận.</summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>Mục đích chuyến thăm (ví dụ: Thăm hỏi, Giúp chuyển đồ, Tham dự sự kiện). Dùng để ghi vào sổ an ninh.</summary>
        public string Purpose { get; set; } = string.Empty;

        /// <summary>Khóa ngoại trỏ đến sinh viên (User) là người được thăm trong ký túc xá.</summary>
        public Guid HostId { get; set; }

        /// <summary>Navigation property đến sinh viên (chủ nhà) được khách đến thăm.</summary>
        [ForeignKey("HostId")]
        public virtual User Host { get; set; } = null!;
    }
}
