namespace DormitoryManagement.Repositories.Interfaces.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);  
    }
}
