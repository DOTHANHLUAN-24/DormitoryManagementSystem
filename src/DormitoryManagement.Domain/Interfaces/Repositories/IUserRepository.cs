using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);

        IQueryable<User> GetPagingQuery(string searchString, int pageIndex, int pageSize);
    }
}
