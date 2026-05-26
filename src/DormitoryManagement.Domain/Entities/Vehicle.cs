using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể quản lý thông tin phương tiện cá nhân (xe máy, xe đạp...) của sinh viên hoặc nhân viên
    /// đã đăng ký gửi xe tại khuôn viên ký túc xá.
    /// </summary>
    [Table("Vehicles")]
    public class Vehicle : BaseEntity
    {
        /// <summary>Loại phương tiện (ví dụ: Xe máy, Xe đạp, Xe điện). Xác định loại bãi đỗ và phí gửi xe.</summary>
        public string VehicleType { get; set; } = string.Empty;

        /// <summary>Biển số xe (ví dụ: 51F1-12345). Dùng để xác minh và kiểm soát ra vào bãi xe.</summary>
        public string LicensePlate { get; set; } = string.Empty;

        /// <summary>Khóa ngoại trỏ đến chủ sở hữu phương tiện (sinh viên hoặc nhân viên).</summary>
        public Guid OwnerId { get; set; }

        /// <summary>Navigation property đến người dùng là chủ sở hữu phương tiện.</summary>
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; } = null!;
    }
}
