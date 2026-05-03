namespace DormitoryManagement.Domain.Interfaces.Entities
{
    public abstract class BaseEntity : IAuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Tự tạo Guid mới

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? LastModified { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public bool IsDeleted { get; set; } = false;
    }
}
