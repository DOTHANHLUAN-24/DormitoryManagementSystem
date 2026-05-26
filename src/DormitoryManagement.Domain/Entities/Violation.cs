using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể ghi nhận các vi phạm nội quy kỷ luật của sinh viên trong ký túc xá.
    /// Liên kết với hợp đồng thuê phòng để xác định sinh viên và phòng vi phạm.
    /// </summary>
    [Table("Violations")]
    public class Violation : BaseEntity
    {
        /// <summary>Mô tả chi tiết nội dung hành vi vi phạm nội quy đã xảy ra.</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Số tiền phạt áp dụng theo mức độ vi phạm (đơn vị: VNĐ). Nhẹ: 50k, Trung bình: 100k, Nghiêm trọng: 200k, Cảnh cáo: 300k.</summary>
        public decimal FineAmount { get; set; }

        /// <summary>Ngày và giờ xảy ra hoặc phát hiện hành vi vi phạm.</summary>
        public DateTime ViolationDate { get; set; }

        /// <summary>Trạng thái xử lý biên bản (Pending = Chưa xử lý, Resolved = Đã xử lý, Cancelled = Hủy).</summary>
        public ViolationStatus Status { get; set; }

        /// <summary>Đường dẫn đến ảnh minh chứng vi phạm (ảnh chụp hiện trường, camera...). Có thể để mặc định nếu không có ảnh.</summary>
        public string EvidenceImage { get; set; } = string.Empty;

        /// <summary>Ghi chú biện pháp và kết quả xử lý do nhân viên quản lý nhập vào khi đánh dấu đã giải quyết.</summary>
        public string? ResolveNote { get; set; }

        /// <summary>Thời điểm biên bản vi phạm được xử lý xong và chuyển sang trạng thái Resolved. Null nếu chưa xử lý.</summary>
        public DateTime? ResolvedAt { get; set; }

        /// <summary>Khóa ngoại trỏ đến hợp đồng thuê phòng của sinh viên vi phạm.</summary>
        public Guid ContractId { get; set; }

        /// <summary>Navigation property đến hợp đồng thuê phòng liên quan đến vi phạm này.</summary>
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; } = null!;
    }
}
