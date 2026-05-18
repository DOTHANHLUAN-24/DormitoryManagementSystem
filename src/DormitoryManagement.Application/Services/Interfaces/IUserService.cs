using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Requests.Users;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IUserService
    {
        // ================= QUERY (Đọc dữ liệu) =================

        // Lấy danh sách đang hoạt động (Thường dùng cho màn hình chính)
        Task<PagedResult<UserResponseDto>> GetActiveUsersPagedAsync(int page, int pageSize, string? search);

        // Lấy danh sách bị chặn (Ban)
        Task<PagedResult<UserResponseDto>> GetBannedUsersPagedAsync(int page, int pageSize, string? search);

        // Lấy danh sách đã bị xóa mềm (Thùng rác)
        Task<PagedResult<UserResponseDto>> GetDeletedUsersPagedAsync(int page, int pageSize, string? search);

        Task<UserResponseDto?> GetUserByIdAsync(Guid id);
        Task<UserResponseDto?> GetByUsernameAsync(string username);


        // ================= COMMAND (Thay đổi dữ liệu) =================

        Task<bool> CreateUserAsync(UserRequestDto userDto);
        Task<bool> CreateUsersAsync(IEnumerable<UserRequestDto> userDtos);

        Task<bool> UpdateUserProfileAsync(Guid id, UserUpdateDto userDto);

        // Thay đổi trạng thái Hoạt động/Bị chặn (IsActive true/false)
        Task<bool> ToggleUserStatusAsync(Guid id); // Đảo ngược trạng thái hoặc dùng hàm Ban/Unban bên dưới
        Task<bool> BanUserAsync(Guid id);
        Task<bool> UnbanUserAsync(Guid id);


        // ================= DELETE & RESTORE (Xóa và Khôi phục) =================

        // Xóa mềm (IsDeleted = true)
        Task<bool> DeactivateUserAsync(Guid id);
        Task<bool> DeactivateUsersAsync(IEnumerable<Guid> ids);

        // Khôi phục (IsDeleted = false)
        Task<bool> RestoreUserAsync(Guid id);
        Task<bool> RestoreUsersAsync(IEnumerable<Guid> ids);

        // Xóa vĩnh viễn (Hard Delete)
        Task<bool> DeletePermanentlyAsync(Guid id);


        // ================= VALIDATION (Kiểm tra) =================

        Task<bool> IsUsernameExistAsync(string username);
        Task<bool> IsEmailExistAsync(string email);
    }
}