using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Số lần sử dụng cụ thể của Dịch vụ / tiện ích
    [Table("UtilityUsages")]
    public class UtilityUsage : BaseEntity
    {
        public int Month { get; set; }
        
        public int Year { get; set; }
        
        public double PreviousIndex { get; set; }
        
        public double CurrentIndex { get; set; }
        
        public double UsageQuantity { get; set; }
        
        public decimal TotalAmount { get; set; }

        // Gắn với Room để ghi nhận hàng tháng, có thể map nullable với Invoice
        public Guid RoomId { get; set; }
        
        public Guid UtilityId { get; set; }

        public Guid? InvoiceId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        [ForeignKey("UtilityId")]
        public virtual Utility Utility { get; set; } = null!;

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
