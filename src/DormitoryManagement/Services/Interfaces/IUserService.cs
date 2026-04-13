using DormitoryManagement.Models.DTOs;

namespace DormitoryManagement.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Tạo người dùng mới
        /// </summary>
        Task<(bool Success, string Message)> CreateUserAsync(CreateUserDTO userDTO);

        /// <summary>
        /// Lấy danh sách tất cả người dùng
        /// </summary>
        Task<IEnumerable<UserListDTO>> GetAllUsersAsync();

        /// <summary>
        /// Lấy danh sách người dùng hoạt động
        /// </summary>
        Task<IEnumerable<UserListDTO>> GetActiveUsersAsync();

        /// <summary>
        /// Lấy chi tiết người dùng
        /// </summary>
        Task<UserDetailsDTO?> GetUserDetailsAsync(string userId);

        /// <summary>
        /// Tìm kiếm người dùng
        /// </summary>
        Task<IEnumerable<UserListDTO>> SearchUsersAsync(string searchTerm);

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        Task<(bool Success, string Message)> UpdateUserAsync(UpdateUserDTO userDTO);

        /// <summary>
        /// Xóa người dùng (xóa mềm)
        /// </summary>
        Task<(bool Success, string Message)> DeleteUserAsync(string userId);

        /// <summary>
        /// Khôi phục người dùng bị xóa
        /// </summary>
        Task<(bool Success, string Message)> RestoreUserAsync(string userId);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        Task<(bool Success, string Message)> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);

        /// <summary>
        /// Đặt lại mật khẩu
        /// </summary>
        Task<(bool Success, string Message, string? NewPassword)> ResetPasswordAsync(string userId);

        /// <summary>
        /// Khóa tài khoản người dùng
        /// </summary>
        Task<(bool Success, string Message)> LockUserAsync(string userId, TimeSpan lockoutDuration);

        /// <summary>
        /// Mở khóa tài khoản người dùng
        /// </summary>
        Task<(bool Success, string Message)> UnlockUserAsync(string userId);

        /// <summary>
        /// Gán role cho người dùng
        /// </summary>
        Task<(bool Success, string Message)> AssignRolesToUserAsync(string userId, IEnumerable<string> roles);

        /// <summary>
        /// Lấy roles của người dùng
        /// </summary>
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);

        /// <summary>
        /// Kiểm tra tên người dùng đã tồn tại
        /// </summary>
        Task<bool> UserNameExistsAsync(string userName);

        /// <summary>
        /// Kiểm tra email đã tồn tại
        /// </summary>
        Task<bool> EmailExistsAsync(string email);
    }
}
using DormitoryManagement.Models.DTOs;

namespace DormitoryManagement.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Tạo người dùng mới
        /// </summary>
        Task<(bool Success, string Message)> CreateUserAsync(CreateUserDTO userDTO);

        /// <summary>
        /// Lấy danh sách tất cả người dùng
        /// </summary>
        Task<IEnumerable<UserListDTO>> GetAllUsersAsync();

        /// <summary>
        /// Lấy danh sách người dùng hoạt động
        /// </summary>
        Task<IEnumerable<UserListDTO>> GetActiveUsersAsync();

        /// <summary>
        /// Lấy chi tiết người dùng
        /// </summary>
        Task<UserDetailsDTO?> GetUserDetailsAsync(string userId);

        /// <summary>
        /// Tìm kiếm người dùng
        /// </summary>
        Task<IEnumerable<UserListDTO>> SearchUsersAsync(string searchTerm);

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        Task<(bool Success, string Message)> UpdateUserAsync(UpdateUserDTO userDTO);

        /// <summary>
        /// Xóa người dùng (xóa mềm)
        /// </summary>
        Task<(bool Success, string Message)> DeleteUserAsync(string userId);

        /// <summary>
        /// Khôi phục người dùng bị xóa
        /// </summary>
        Task<(bool Success, string Message)> RestoreUserAsync(string userId);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        Task<(bool Success, string Message)> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);

        /// <summary>
        /// Đặt lại mật khẩu
        /// </summary>
        Task<(bool Success, string Message, string? NewPassword)> ResetPasswordAsync(string userId);

        /// <summary>
        /// Khóa tài khoản người dùng
        /// </summary>
        Task<(bool Success, string Message)> LockUserAsync(string userId, TimeSpan lockoutDuration);

        /// <summary>
        /// Mở khóa tài khoản người dùng
        /// </summary>
        Task<(bool Success, string Message)> UnlockUserAsync(string userId);

        /// <summary>
        /// Gán role cho người dùng
        /// </summary>
        Task<(bool Success, string Message)> AssignRolesToUserAsync(string userId, IEnumerable<string> roles);

        /// <summary>
        /// Lấy roles của người dùng
        /// </summary>
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);

        /// <summary>
        /// Kiểm tra tên người dùng đã tồn tại
        /// </summary>
        Task<bool> UserNameExistsAsync(string userName);

        /// <summary>
        /// Kiểm tra email đã tồn tại
        /// </summary>
        Task<bool> EmailExistsAsync(string email);
    }
}
