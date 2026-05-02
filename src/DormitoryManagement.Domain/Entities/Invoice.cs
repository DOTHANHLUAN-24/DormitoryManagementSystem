using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Entities
{
    // Hóa đơn
    [Table("Invoices")]
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string InvoiceCode { get; set; } = string.Empty; // Ví dụ: HD001, HD002
        
        public string Title { get; set; } = string.Empty; // Ví dụ: Hóa đơn tháng 1/2024
        
        public int BillingMonth { get; set; }
        
        public int BillingYear { get; set; }
        
        public decimal TotalAmount { get; set; } // Tiền phòng + Điện nước + Phụ phí - Đã trả
        
        public DateTime DueDate { get; set; }
        
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int ContractId { get; set; }
        
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; } = null!;

        public virtual ICollection<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();
        
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        
        public virtual ICollection<Surcharge> Surcharges { get; set; } = new List<Surcharge>();
    }
}
