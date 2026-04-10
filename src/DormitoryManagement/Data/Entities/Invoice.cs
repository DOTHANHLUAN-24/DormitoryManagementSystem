using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("Invoices")]
    public class Invoice
    {
        public int Id { get; set; }

        public int ContractId { get; set; }

        public Contract Contract { get; set; } = null!;

        public DateTime MonthYear { get; set; } 
        
        public decimal TotalAmount { get; set; }
        
        public bool IsPaid { get; set; } = false;
        
        public DateTime? PaidDate { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
