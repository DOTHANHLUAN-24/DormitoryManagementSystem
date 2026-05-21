using DormitoryManagement.Application.Dtos.Requests.Utilities;
using DormitoryManagement.Application.Dtos.Responses.Utilities;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý tiện ích / dịch vụ (Utility).
    /// </summary>
    public interface IUtilityService
    {
        /// <summary>
        /// Lấy toàn bộ danh sách dịch vụ đang hoạt động.
        /// </summary>
        /// <returns>Danh sách dịch vụ</returns>
        Task<IEnumerable<UtilityResponseDto>> GetAllActiveUtilitiesAsync();

        /// <summary>
        /// Lấy toàn bộ danh sách dịch vụ đã bị tạm ngưng / đưa vào thùng rác.
        /// </summary>
        /// <returns>Danh sách dịch vụ trong thùng rác</returns>
        Task<IEnumerable<UtilityResponseDto>> GetAllDeletedUtilitiesAsync();

        /// <summary>
        /// Lấy danh sách dịch vụ tiện ích phân trang kèm theo bộ lọc tìm kiếm.
        /// </summary>
        Task<PagedResult<UtilityResponseDto>> GetPagedUtilitiesAsync(int pageIndex, int pageSize, string? searchTerm, bool? isActive = null, bool? isDeleted = false);

        /// <summary>
        /// Lấy chi tiết thông tin dịch vụ theo Id.
        /// </summary>
        /// <param name="id">Id của dịch vụ</param>
        /// <returns>Thông tin dịch vụ hoặc null</returns>
        Task<UtilityResponseDto?> GetUtilityByIdAsync(Guid id);

        /// <summary>
        /// Tạo mới một dịch vụ tiện ích.
        /// </summary>
        /// <param name="request">Thông tin dịch vụ mới</param>
        /// <returns>True nếu tạo thành công, ngược lại là False</returns>
        Task<bool> CreateUtilityAsync(UtilityRequestDto request);

        /// <summary>
        /// Cập nhật thông tin dịch vụ hiện có.
        /// </summary>
        /// <param name="id">Id của dịch vụ cần sửa</param>
        /// <param name="request">Thông tin cập nhật mới</param>
        /// <returns>True nếu cập nhật thành công, ngược lại là False</returns>
        Task<bool> UpdateUtilityAsync(Guid id, UtilityRequestDto request);

        /// <summary>
        /// Đưa dịch vụ vào thùng rác (xóa mềm bằng cách đặt IsActive = false).
        /// </summary>
        /// <param name="id">Id dịch vụ cần xóa</param>
        /// <returns>True nếu xóa thành công, ngược lại là False</returns>
        Task<bool> SoftDeleteUtilityAsync(Guid id);

        /// <summary>
        /// Khôi phục dịch vụ từ thùng rác (đặt IsActive = true).
        /// </summary>
        /// <param name="id">Id dịch vụ cần khôi phục</param>
        /// <returns>True nếu khôi phục thành công, ngược lại là False</returns>
        Task<bool> RestoreUtilityAsync(Guid id);

        /// <summary>
        /// Xóa vĩnh viễn dịch vụ tiện ích ra khỏi database.
        /// </summary>
        /// <param name="id">Id dịch vụ cần xóa vĩnh viễn</param>
        /// <returns>True nếu xóa thành công, ngược lại là False</returns>
        Task<bool> HardDeleteUtilityAsync(Guid id);
    }
}
