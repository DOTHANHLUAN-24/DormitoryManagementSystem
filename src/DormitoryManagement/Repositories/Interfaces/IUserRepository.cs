using DormitoryManagement.Data.Entities;
using DormitoryManagement.Repositories.Interfaces.Base;

namespace DormitoryManagement.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUserNameAsync(string userName);

        Task<User?> GetByEmailAsync(string email);

        Task<IEnumerable<User>> GetActiveUsersAsync();

        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);

        Task<bool> UserExistsAsync(string userName);

        Task<bool> EmailExistsAsync(string email);
    }
}
