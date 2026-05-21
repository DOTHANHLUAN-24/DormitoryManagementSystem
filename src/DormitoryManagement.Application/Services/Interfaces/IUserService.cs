using DormitoryManagement.Application.Dtos.Requests.Users;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý nghiệp vụ người dùng (User / Sinh viên / Quản lý).
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Lấy danh sách người dùng đang hoạt động phân trang và tìm kiếm.
        /// </summary>
        /// <param name="page">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="search">Từ khóa tìm kiếm theo tên hoặc email</param>
        /// <returns>Kết quả phân trang danh sách người dùng</returns>
        Task<PagedResult<UserResponseDto>> GetActiveUsersPagedAsync(int page, int pageSize, string? search);

        /// <summary>
        /// Lấy danh sách người dùng bị khóa (Ban) phân trang và tìm kiếm.
        /// </summary>
        /// <param name="page">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns>Kết quả phân trang danh sách người dùng bị khóa</returns>
        Task<PagedResult<UserResponseDto>> GetBannedUsersPagedAsync(int page, int pageSize, string? search);

        /// <summary>
        /// Lấy danh sách người dùng đã bị xóa mềm (thùng rác) phân trang và tìm kiếm.
        /// </summary>
        /// <param name="page">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns>Kết quả phân trang danh sách người dùng đã xóa mềm</returns>
        Task<PagedResult<UserResponseDto>> GetDeletedUsersPagedAsync(int page, int pageSize, string? search);

        /// <summary>
        /// Lấy thông tin chi tiết người dùng theo Id.
        /// </summary>
        /// <param name="id">Id của người dùng</param>
        /// <returns>Thông tin người dùng hoặc null nếu không tìm thấy</returns>
        Task<UserResponseDto?> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Lấy thông tin người dùng theo tên tài khoản (Username).
        /// </summary>
        /// <param name="username">Tên tài khoản</param>
        /// <returns>Thông tin người dùng hoặc null</returns>
        Task<UserResponseDto?> GetByUsernameAsync(string username);

        /// <summary>
        /// Tạo mới một người dùng.
        /// </summary>
        /// <param name="userDto">Thông tin yêu cầu tạo người dùng</param>
        /// <returns>True nếu tạo thành công, ngược lại là False</returns>
        Task<bool> CreateUserAsync(UserRequestDto userDto);

        /// <summary>
        /// Tạo hàng loạt nhiều người dùng từ danh sách (import dữ liệu).
        /// </summary>
        /// <param name="userDtos">Danh sách người dùng cần tạo</param>
        /// <returns>True nếu tất cả tạo thành công, ngược lại là False</returns>
        Task<bool> CreateUsersAsync(IEnumerable<UserRequestDto> userDtos);

        /// <summary>
        /// Cập nhật thông tin hồ sơ của người dùng.
        /// </summary>
        /// <param name="id">Id của người dùng cần sửa</param>
        /// <param name="userDto">Thông tin cập nhật mới</param>
        /// <returns>True nếu cập nhật thành công, ngược lại là False</returns>
        Task<bool> UpdateUserProfileAsync(Guid id, UserUpdateDto userDto);

        /// <summary>
        /// Đổi trạng thái hoạt động của người dùng (IsActive true/false).
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        Task<bool> ToggleUserStatusAsync(Guid id);

        /// <summary>
        /// Khóa (Ban) tài khoản người dùng.
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        Task<bool> BanUserAsync(Guid id);

        /// <summary>
        /// Mở khóa (Unban) tài khoản người dùng.
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        Task<bool> UnbanUserAsync(Guid id);

        /// <summary>
        /// Xóa mềm tài sản người dùng (IsDeleted = true).
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        Task<bool> DeactivateUserAsync(Guid id);

        /// <summary>
        /// Xóa mềm nhiều người dùng cùng lúc.
        /// </summary>
        /// <param name="ids">Danh sách Id người dùng cần xóa mềm</param>
        /// <returns>True nếu tất cả thành công, ngược lại là False</returns>
        Task<bool> DeactivateUsersAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Khôi phục tài khoản người dùng đã bị xóa mềm.
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>True nếu khôi phục thành công, ngược lại là False</returns>
        Task<bool> RestoreUserAsync(Guid id);

        /// <summary>
        /// Khôi phục hàng loạt nhiều người dùng cùng lúc.
        /// </summary>
        /// <param name="ids">Danh sách Id người dùng cần khôi phục</param>
        /// <returns>True nếu khôi phục thành công, ngược lại là False</returns>
        Task<bool> RestoreUsersAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Xóa vĩnh viễn người dùng khỏi cơ sở dữ liệu.
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>True nếu xóa vĩnh viễn thành công, ngược lại là False</returns>
        Task<bool> DeletePermanentlyAsync(Guid id);

        /// <summary>
        /// Kiểm tra tên tài khoản đã tồn tại hay chưa.
        /// </summary>
        /// <param name="username">Tên tài khoản cần kiểm tra</param>
        /// <returns>True nếu đã tồn tại, ngược lại là False</returns>
        Task<bool> IsUsernameExistAsync(string username);

        /// <summary>
        /// Kiểm tra địa chỉ email đã tồn tại hay chưa.
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <returns>True nếu đã tồn tại, ngược lại là False</returns>
        Task<bool> IsEmailExistAsync(string email);
    }
}