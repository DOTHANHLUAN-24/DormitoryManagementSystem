using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin người dùng (User).
    /// </summary>
    public class UserRepository(ApplicationDbContext db) : BaseRepository<User>(db), IUserRepository
    {

        /// <summary>
        /// Lấy thông tin người dùng theo tên tài khoản (Username).
        /// </summary>
        /// <param name="username">Tên tài khoản</param>
        /// <returns>Người dùng tương ứng nếu tìm thấy, ngược lại là null</returns>
        public async Task<User?> GetByUsernameAsync(string username)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);

        /// <summary>
        /// Kiểm tra xem địa chỉ email có phải là duy nhất (chưa được sử dụng) hay không.
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <returns>True nếu email chưa được sử dụng, ngược lại là False</returns>
        public async Task<bool> IsEmailUniqueAsync(string email)
            => !await _dbSet.AsNoTracking().AnyAsync(u => u.Email == email);
    }
}