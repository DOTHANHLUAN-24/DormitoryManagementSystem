namespace DormitoryManagement.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
