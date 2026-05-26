using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho một giao dịch thanh toán hóa đơn của sinh viên.
    /// Mỗi hóa đơn có thể được thanh toán nhiều lần (trả từng phần).
    /// </summary>
    [Table("Payments")]
    public class Payment : BaseEntity
    {
        /// <summary>Số tiền đã thanh toán trong giao dịch này (đơn vị: VNĐ).</summary>
        public decimal AmountPaid { get; set; }

        /// <summary>Thời điểm thực hiện giao dịch thanh toán.</summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>Mã giao dịch từ hệ thống thanh toán hoặc ngân hàng (ví dụ: TXN20240115001). Dùng để đối soát.</summary>
        public string TransactionCode { get; set; } = string.Empty;

        /// <summary>Phương thức thanh toán (Cash = Tiền mặt, BankTransfer = Chuyển khoản, Momo, VnPay...).</summary>
        public PaymentMethod Method { get; set; }

        /// <summary>Ghi chú bổ sung về giao dịch (ví dụ: Thanh toán qua quầy, đã xác nhận).</summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>Khóa ngoại trỏ đến hóa đơn được thanh toán trong giao dịch này.</summary>
        public Guid InvoiceId { get; set; }

        /// <summary>Navigation property đến hóa đơn liên kết với giao dịch thanh toán.</summary>
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
