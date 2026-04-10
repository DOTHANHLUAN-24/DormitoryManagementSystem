using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("Payments")]
    public class Payment
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        
        public Invoice Invoice { get; set; } = null!;

        public decimal Amount { get; set; }
        
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        
        public string PaymentMethod { get; set; } = "Cash";
    }
}
