using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<(List<User> Users, int TotalCount)> GetActiveUsersPagedAsync(int page, int pageSize, string? search);

        IQueryable<User> GetQuery();
    }
}
