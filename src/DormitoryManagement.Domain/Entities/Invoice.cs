using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho hóa đơn thanh toán hàng tháng của sinh viên,
    /// bao gồm tiền phòng, điện nước và các phụ phí đính kèm.
    /// </summary>
    [Table("Invoices")]
    public class Invoice : BaseEntity
    {
        /// <summary>Mã hóa đơn dùng để định danh (ví dụ: INV-2024-001). Tối đa 50 ký tự.</summary>
        [Required, StringLength(50)]
        public string InvoiceCode { get; set; } = string.Empty;

        /// <summary>Tiêu đề mô tả kỳ hóa đơn (ví dụ: Hóa đơn tháng 1/2024).</summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>Tháng thanh toán của hóa đơn (giá trị từ 1 đến 12).</summary>
        public int BillingMonth { get; set; }

        /// <summary>Năm thanh toán của hóa đơn (ví dụ: 2024, 2025).</summary>
        public int BillingYear { get; set; }

        /// <summary>Tổng số tiền cần thanh toán: tiền phòng + điện + nước + phụ phí (đơn vị: VNĐ).</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>Ngày hết hạn thanh toán hóa đơn. Quá ngày này sẽ chuyển sang trạng thái Overdue.</summary>
        public DateTime DueDate { get; set; }

        /// <summary>Trạng thái thanh toán (Unpaid = Chưa thanh toán, Paid = Đã thanh toán, Overdue = Quá hạn, Cancelled = Hủy).</summary>
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;

        /// <summary>Khóa ngoại trỏ đến hợp đồng sinh ra hóa đơn này.</summary>
        public Guid ContractId { get; set; }

        /// <summary>Navigation property đến hợp đồng liên kết với hóa đơn.</summary>
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; } = null!;

        /// <summary>Danh sách chi tiết sử dụng điện/nước trong kỳ hóa đơn này.</summary>
        public virtual ICollection<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();

        /// <summary>Danh sách các giao dịch thanh toán thực hiện cho hóa đơn này.</summary>
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        /// <summary>Danh sách phụ phí đính kèm trong hóa đơn (giữ xe, vệ sinh...).</summary>
        public virtual ICollection<Surcharge> Surcharges { get; set; } = new List<Surcharge>();
    }
}
