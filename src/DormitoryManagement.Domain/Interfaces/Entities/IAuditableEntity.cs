namespace DormitoryManagement.Domain.Interfaces.Entities
{
    /// <summary>
    /// Giao diện đại diện cho một thực thể (Entity) có khả năng theo dõi vết (Audit).
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Định danh duy nhất (Id) của thực thể.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Thời điểm thực thể được tạo ra.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Thời điểm thực thể được cập nhật lần cuối (nếu có).
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Cờ đánh dấu thực thể đang hoạt động (kích hoạt).
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Cờ đánh dấu thực thể đã bị xóa mềm (Soft Delete).
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
