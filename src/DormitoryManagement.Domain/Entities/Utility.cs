using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho một loại dịch vụ hoặc tiện ích tính phí theo chỉ số (Điện, Nước, Internet...).
    /// Định nghĩa đơn giá và đơn vị tính để làm cơ sở lập hóa đơn.
    /// </summary>
    [Table("Utilities")]
    public class Utility : BaseEntity
    {
        /// <summary>Tên dịch vụ hoặc tiện ích (ví dụ: Điện, Nước, Internet, Gas). Tối đa 100 ký tự.</summary>
        [Required, StringLength(100)]
        public string UtilityName { get; set; } = string.Empty;

        /// <summary>Đơn giá tính phí trên mỗi đơn vị tiêu thụ (ví dụ: 3.500đ/kWh điện, 15.000đ/m³ nước).</summary>
        public decimal UnitPrice { get; set; }

        /// <summary>Đơn vị đo lường tiêu thụ (ví dụ: kWh cho điện, m³ cho nước, Mbps cho internet).</summary>
        public string Unit { get; set; } = string.Empty;
    }
}
