using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý nghiệp vụ giường (Bed).
    /// </summary>
    public interface IBedService
    {
        /// <summary>
        /// Lấy thông tin giường theo Id.
        /// </summary>
        /// <param name="id">Id của giường</param>
        /// <returns>Thông tin giường hoặc null nếu không tìm thấy</returns>
        Task<Bed?> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy toàn bộ danh sách giường.
        /// </summary>
        /// <param name="includeDeleted">Có bao gồm giường đã bị xóa mềm không</param>
        /// <returns>Danh sách giường</returns>
        Task<IEnumerable<Bed>> GetAllAsync(bool includeDeleted = false);

        /// <summary>
        /// Lấy thông tin giường theo số giường (BedNumber).
        /// </summary>
        /// <param name="bedNumber">Số giường cần tìm</param>
        /// <returns>Thông tin giường hoặc null</returns>
        Task<Bed?> GetByBedNumberAsync(string bedNumber);

        /// <summary>
        /// Lấy danh sách giường phân trang kèm tìm kiếm.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số lượng phần tử trên trang</param>
        /// <param name="searchString">Từ khóa tìm kiếm (tùy chọn)</param>
        /// <returns>Kết quả phân trang của giường</returns>
        Task<PagedResult<Bed>> GetPagedAsync(int pageIndex, int pageSize, string? searchString = null);

        /// <summary>
        /// Lấy danh sách các giường trống thuộc một phòng cụ thể.
        /// </summary>
        /// <param name="roomId">Id của phòng</param>
        /// <returns>Danh sách các giường trống</returns>
        Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId);

        /// <summary>
        /// Kiểm tra xem giường có trống để sử dụng không.
        /// </summary>
        /// <param name="bedId">Id của giường</param>
        /// <returns>True nếu trống và hợp lệ, ngược lại là False</returns>
        Task<bool> IsBedAvailableAsync(Guid bedId);

        /// <summary>
        /// Tạo mới một giường vào hệ thống.
        /// </summary>
        /// <param name="bed">Thông tin thực thể giường cần tạo</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        Task<bool> CreateBedAsync(Bed bed);

        /// <summary>
        /// Cập nhật thông tin giường.
        /// </summary>
        /// <param name="bed">Thông tin giường cần sửa</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        Task<bool> UpdateBedAsync(Bed bed);

        /// <summary>
        /// Xóa một giường ra khỏi hệ thống (hỗ trợ xóa mềm/xóa cứng).
        /// </summary>
        /// <param name="id">Mã định danh của giường cần xóa</param>
        /// <param name="isSoftDelete">True để xóa mềm (mặc định), False để xóa cứng</param>
        /// <returns>True nếu xóa thành công, ngược lại là False</returns>
        Task<bool> DeleteBedAsync(Guid id, bool isSoftDelete = true);
    }
}