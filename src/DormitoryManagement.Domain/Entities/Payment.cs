using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Thanh toán
    [Table("Payments")]
    public class Payment : BaseEntity
    {
        public decimal AmountPaid { get; set; }
        
        public DateTime PaymentDate { get; set; }
        
        public string TransactionCode { get; set; } = string.Empty;
        
        public PaymentMethod Method { get; set; }
        
        public string Note { get; set; } = string.Empty;

        public Guid InvoiceId { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
