using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho các phụ phí kèm theo trong hóa đơn.
    /// </summary>
    [Table("Surcharges")]
    public class Surcharge : BaseEntity
    {
        public string SurchargeName { get; set; } = string.Empty; // Gửi xe, dọn vệ sinh...
        
        public decimal Amount { get; set; }

        public Guid InvoiceId { get; set; }
        
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
