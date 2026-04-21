using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    // Phụ phí kèm theo
    [Table("Surcharges")]
    public class Surcharge
    {
        [Key]
        public int Id { get; set; }
        
        public string SurchargeName { get; set; } = string.Empty; // Gửi xe, dọn vệ sinh...
        
        public decimal Amount { get; set; }

        public int InvoiceId { get; set; }
        
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
