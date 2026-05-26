using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho hóa đơn thanh toán hàng tháng của sinh viên.
    /// </summary>
    [Table("Invoices")]
    public class Invoice : BaseEntity
    {
        [Required, StringLength(50)]
        public string InvoiceCode { get; set; } = string.Empty; // Ví dụ: HD001, HD002
        
        public string Title { get; set; } = string.Empty; // Ví dụ: Hóa đơn tháng 1/2024
        
        public int BillingMonth { get; set; }
        
        public int BillingYear { get; set; }
        
        public decimal TotalAmount { get; set; } // Tiền phòng + Điện nước + Phụ phí - Đã trả
        
        public DateTime DueDate { get; set; }
        
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;
        
        public Guid ContractId { get; set; }
        
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; } = null!;

        public virtual ICollection<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();
        
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        
        public virtual ICollection<Surcharge> Surcharges { get; set; } = new List<Surcharge>();
    }
}
