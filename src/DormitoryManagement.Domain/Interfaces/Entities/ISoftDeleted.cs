namespace DormitoryManagement.Domain.Interfaces.Entities
{
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
    }
}
