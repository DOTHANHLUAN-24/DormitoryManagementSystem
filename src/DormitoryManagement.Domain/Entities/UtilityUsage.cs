using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể lưu trữ số liệu tiêu thụ điện/nước hàng tháng của từng phòng,
    /// làm cơ sở tính toán chi phí trong hóa đơn.
    /// </summary>
    [Table("UtilityUsages")]
    public class UtilityUsage : BaseEntity
    {
        /// <summary>Tháng ghi nhận chỉ số tiêu thụ (giá trị từ 1 đến 12).</summary>
        public int Month { get; set; }

        /// <summary>Năm ghi nhận chỉ số tiêu thụ (ví dụ: 2024, 2025).</summary>
        public int Year { get; set; }

        /// <summary>Chỉ số công tơ/đồng hồ tại đầu kỳ (chỉ số tháng trước).</summary>
        public double PreviousIndex { get; set; }

        /// <summary>Chỉ số công tơ/đồng hồ tại cuối kỳ (chỉ số tháng này).</summary>
        public double CurrentIndex { get; set; }

        /// <summary>Lượng tiêu thụ trong kỳ = CurrentIndex - PreviousIndex (tính theo đơn vị của tiện ích tương ứng).</summary>
        public double UsageQuantity { get; set; }

        /// <summary>Tổng tiền phát sinh = UsageQuantity × UnitPrice của tiện ích (đơn vị: VNĐ).</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>Khóa ngoại trỏ đến phòng được ghi nhận lượng tiêu thụ trong tháng.</summary>
        public Guid RoomId { get; set; }

        /// <summary>Khóa ngoại trỏ đến loại tiện ích (Điện, Nước...) tương ứng với bản ghi này.</summary>
        public Guid UtilityId { get; set; }

        /// <summary>Khóa ngoại trỏ đến hóa đơn chứa chi tiết tiêu thụ này. Null nếu chưa được gắn vào hóa đơn.</summary>
        public Guid? InvoiceId { get; set; }

        /// <summary>Navigation property đến phòng tiêu thụ dịch vụ.</summary>
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        /// <summary>Navigation property đến loại tiện ích (Điện, Nước, Internet...).</summary>
        [ForeignKey("UtilityId")]
        public virtual Utility Utility { get; set; } = null!;

        /// <summary>Navigation property đến hóa đơn đính kèm bản ghi tiêu thụ này.</summary>
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; } = null!;
    }
}
