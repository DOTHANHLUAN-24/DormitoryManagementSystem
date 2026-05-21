using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin người dùng (User / Sinh viên).
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Lấy thông tin người dùng theo tên tài khoản (Username).
        /// </summary>
        /// <param name="username">Tên tài khoản cần tìm</param>
        /// <returns>Người dùng nếu tìm thấy, ngược lại là null</returns>
        Task<User?> GetByUsernameAsync(string username);

        /// <summary>
        /// Kiểm tra xem địa chỉ email có phải là duy nhất (chưa được sử dụng) hay không.
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <returns>True nếu email chưa được sử dụng bởi người dùng khác, ngược lại là False</returns>
        Task<bool> IsEmailUniqueAsync(string email);
    }
}
