using DormitoryManagement.Data;
using DormitoryManagement.Data.Entities;
using DormitoryManagement.Repositories.Implementations.Base;
using DormitoryManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(u => u.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(u =>
                    u.UserName.Contains(searchTerm) ||
                    u.FirstName.Contains(searchTerm) ||
                    u.LastName.Contains(searchTerm) ||
                    u.Email.Contains(searchTerm)
                )
                .ToListAsync();
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            return await _dbSet.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }
    }
}
