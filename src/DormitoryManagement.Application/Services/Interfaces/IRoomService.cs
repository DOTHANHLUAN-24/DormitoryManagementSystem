using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Interfaces.Services
{
    /// <summary>
    /// Giao diện dịch vụ quản lý nghiệp vụ phòng (Room).
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// Lấy danh sách phòng phân trang kèm theo bộ lọc nâng cao.
        /// </summary>
        /// <param name="filter">Đối tượng chứa các điều kiện lọc và phân trang</param>
        /// <returns>Kết quả phân trang danh sách phòng</returns>
        Task<PagedResult<RoomResponse>> GetPagedRoomsAsync(RoomFilterRequest filter);

        /// <summary>
        /// Lấy danh sách phòng đã xóa mềm (thùng rác) có phân trang.
        /// </summary>
        /// <param name="filter">Bộ lọc và phân trang</param>
        /// <returns>Kết quả phân trang danh sách phòng đã xóa</returns>
        Task<PagedResult<RoomResponse>> GetDeletedRoomsPagedAsync(RoomFilterRequest filter);

        /// <summary>
        /// Lấy chi tiết phòng theo Id bao gồm thông tin tòa nhà và loại phòng.
        /// </summary>
        /// <param name="id">Id của phòng</param>
        /// <returns>Chi tiết phòng DTO hoặc null</returns>
        Task<RoomDetailResponse?> GetRoomByIdAsync(Guid id);

        /// <summary>
        /// Lấy danh sách các phòng thuộc một tòa nhà cụ thể.
        /// </summary>
        /// <param name="blockId">Id của tòa nhà</param>
        /// <returns>Danh sách phòng</returns>
        Task<IEnumerable<RoomResponse>> GetRoomsByBlockAsync(Guid blockId);

        /// <summary>
        /// Tạo mới một phòng.
        /// </summary>
        /// <param name="request">Yêu cầu tạo phòng</param>
        /// <returns>True nếu tạo thành công, ngược lại là False</returns>
        Task<bool> CreateRoomAsync(CreateRoomRequest request);

        /// <summary>
        /// Cập nhật thông tin phòng.
        /// </summary>
        /// <param name="id">Id của phòng cần sửa</param>
        /// <param name="request">Yêu cầu cập nhật mới</param>
        /// <returns>True nếu cập nhật thành công, ngược lại là False</returns>
        Task<bool> UpdateRoomAsync(Guid id, UpdateRoomRequest request);

        /// <summary>
        /// Xóa mềm một phòng.
        /// </summary>
        /// <param name="id">Id phòng cần xóa mềm</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        Task<bool> DeleteRoomAsync(Guid id);

        /// <summary>
        /// Khôi phục phòng đã bị xóa mềm.
        /// </summary>
        /// <param name="id">Id phòng cần khôi phục</param>
        /// <returns>True nếu khôi phục thành công, ngược lại là False</returns>
        Task<bool> RestoreRoomAsync(Guid id);

        /// <summary>
        /// Xóa vĩnh viễn phòng khỏi cơ sở dữ liệu.
        /// </summary>
        /// <param name="id">Id phòng cần xóa vĩnh viễn</param>
        /// <returns>True nếu xóa thành công, ngược lại là False</returns>
        Task<bool> DeletePermanentlyAsync(Guid id);

        /// <summary>
        /// Thống kê tình trạng phòng phục vụ cho dashboard (tổng số phòng, phòng trống, đầy...).
        /// </summary>
        /// <returns>Đối tượng DTO thống kê phòng</returns>
        Task<RoomStatisticsDto> GetRoomStatisticsAsync();
    }
}