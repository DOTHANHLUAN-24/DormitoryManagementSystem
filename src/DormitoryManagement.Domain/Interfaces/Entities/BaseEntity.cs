namespace DormitoryManagement.Domain.Interfaces.Entities
{
    /// <summary>
    /// Lớp cơ sở (Base class) cho hầu hết các thực thể trong hệ thống, cung cấp các thuộc tính Audit mặc định.
    /// </summary>
    public abstract class BaseEntity : IAuditableEntity
    {
        /// <summary>
        /// Định danh duy nhất (Id) của thực thể, tự động tạo mới khi khởi tạo.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Thời điểm thực thể được tạo ra, mặc định là thời gian hiện tại.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Thời điểm thực thể được cập nhật lần cuối (nếu có).
        /// </summary>
        public DateTime? LastModified { get; set; }
        
        /// <summary>
        /// Cờ đánh dấu thực thể đang hoạt động (kích hoạt). Mặc định là true.
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Cờ đánh dấu thực thể đã bị xóa mềm (Soft Delete). Mặc định là false.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
