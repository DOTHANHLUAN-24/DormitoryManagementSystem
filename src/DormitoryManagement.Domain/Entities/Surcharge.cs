using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho các khoản phụ phí đính kèm trong hóa đơn.
    /// Ví dụ: phí gửi xe, phí dọn vệ sinh, phí internet thêm...
    /// </summary>
    [Table("Surcharges")]
    public class Surcharge : BaseEntity
    {
        /// <summary>Tên khoản phụ phí (ví dụ: Phí gửi xe máy, Phí vệ sinh tháng, Phí internet bổ sung).</summary>
        public string SurchargeName { get; set; } = string.Empty;

        /// <summary>Số tiền của khoản phụ phí (đơn vị: VNĐ).</summary>
        public decimal Amount { get; set; }

        /// <summary>Khóa ngoại trỏ đến hóa đơn chứa khoản phụ phí này.</summary>
        public Guid InvoiceId { get; set; }

        /// <summary>Navigation property đến hóa đơn liên kết với phụ phí.</summary>
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
