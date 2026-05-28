using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho hợp đồng thuê giường/phòng của sinh viên.
    /// Mỗi hợp đồng gắn với một sinh viên (User) và một giường (Bed) cụ thể.
    /// </summary>
    [Table("Contracts")]
    public class Contract : BaseEntity
    {
        /// <summary>Mã hợp đồng dùng để định danh và tham chiếu (ví dụ: HD001, HD2024-001). Tối đa 50 ký tự.</summary>
        [Required, StringLength(50)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>Ngày bắt đầu hiệu lực của hợp đồng thuê phòng.</summary>
        public DateTime StartDate { get; set; }

        /// <summary>Ngày kết thúc hợp đồng thuê phòng (ngày hết hạn).</summary>
        public DateTime EndDate { get; set; }

        /// <summary>Số tiền đặt cọc mà sinh viên đã nộp khi ký hợp đồng (đơn vị: VNĐ).</summary>
        public decimal DepositAmount { get; set; }

        /// <summary>Trạng thái hợp đồng (Pending = Chờ duyệt, Active = Đang hoạt động, Expired = Hết hạn, Terminated = Chấm dứt sớm).</summary>
        public ContractStatus Status { get; set; } = ContractStatus.Pending;

        /// <summary>Khóa ngoại trỏ đến sinh viên (User) là chủ hợp đồng. Nullable vì hợp đồng có thể chưa được gán người.</summary>
        public Guid? UserId { get; set; }

        /// <summary>Navigation property đến sinh viên ký hợp đồng.</summary>
        [ForeignKey("UserId")]
        public virtual User? User { get; set; } = null!;

        /// <summary>Khóa ngoại trỏ đến giường được thuê theo hợp đồng này.</summary>
        public Guid BedId { get; set; }

        /// <summary>Navigation property đến giường thuê trong hợp đồng.</summary>
        [ForeignKey("BedId")]
        public virtual Bed Bed { get; set; } = null!;

        /// <summary>Danh sách hóa đơn phát sinh trong kỳ hợp đồng này.</summary>
        public virtual ICollection<Invoice> Invoices { get; set; } = [];

        /// <summary>Danh sách biên bản vi phạm kỷ luật liên quan đến hợp đồng này.</summary>
        public virtual ICollection<Violation> Violations { get; set; } = [];
    }
}
