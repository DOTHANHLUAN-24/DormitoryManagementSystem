using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho người dùng hệ thống: Sinh viên, Nhân viên quản lý, Nhân viên kỹ thuật hoặc Quản trị viên.
    /// Kế thừa từ <see cref="IdentityUser{Guid}"/> để tích hợp ASP.NET Core Identity.
    /// </summary>
    [Table("Users")]
    public class User : IdentityUser<Guid>, IAuditableEntity
    {
        /// <summary>Họ và tên đầy đủ của người dùng. Tối đa 100 ký tự.</summary>
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>Mã định danh nội bộ: MSSV đối với sinh viên, mã nhân viên đối với nhân viên (ví dụ: SV2024001, NV-IT-01). Tối đa 50 ký tự.</summary>
        [Required, StringLength(50)]
        public string Code { get; set; } = string.Empty;

        /// <summary>Cờ đánh dấu tài khoản còn hoạt động hay đã bị khóa. True = đang hoạt động, False = đã khóa.</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Số Căn cước công dân hoặc Chứng minh nhân dân 12 chữ số, dùng để xác minh danh tính.</summary>
        public string IdentityCardNumber { get; set; } = string.Empty;

        /// <summary>Vai trò của người dùng trong hệ thống (Admin, ManagementStaff, TechnicalStaff, Student).</summary>
        public UserRole Role { get; set; }

        /// <summary>Thời điểm tài khoản được tạo trong hệ thống.</summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>Thời điểm thông tin tài khoản được cập nhật lần cuối. Null nếu chưa từng chỉnh sửa.</summary>
        public DateTime? LastModified { get; set; }

        /// <summary>Cờ xóa mềm: True = đã xóa (ẩn khỏi hệ thống), False = đang tồn tại.</summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>Danh sách hợp đồng thuê phòng mà người dùng này là chủ hợp đồng.</summary>
        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

        /// <summary>Danh sách phương tiện cá nhân (xe máy, xe đạp...) đã đăng ký gửi tại ký túc xá.</summary>
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
