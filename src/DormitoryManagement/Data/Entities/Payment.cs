using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Data.Enums;

namespace DormitoryManagement.Data.Entities
{
    // Thanh toán
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        
        public decimal AmountPaid { get; set; }
        
        public DateTime PaymentDate { get; set; }
        
        public string TransactionCode { get; set; } = string.Empty;
        
        public PaymentMethod Method { get; set; }
        
        public string Note { get; set; } = string.Empty;

        public int InvoiceId { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
