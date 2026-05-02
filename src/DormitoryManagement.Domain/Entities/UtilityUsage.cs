using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Domain.Entities
{
    // Số lần sử dụng cụ thể của **Dịch vụ / tiện ích**
    [Table("UtilityUsages")]
    public class UtilityUsage
    {
        [Key]
        public int Id { get; set; }
        
        public int Month { get; set; }
        
        public int Year { get; set; }
        
        public double PreviousIndex { get; set; }
        
        public double CurrentIndex { get; set; }
        
        public double UsageQuantity { get; set; }
        
        public decimal TotalAmount { get; set; }

        // Gắn với Room để ghi nhận hàng tháng, có thể map nullable với Invoice
        public int RoomId { get; set; }
        
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        public int UtilityId { get; set; }
        
        [ForeignKey("UtilityId")]
        public virtual Utility Utility { get; set; } = null!;

        public int? InvoiceId { get; set; }
        
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
