using DormitoryManagement.Application.Dtos.Requests.RoomTypes;
using DormitoryManagement.Application.Dtos.Responses.RoomTypes;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý loại phòng (RoomType).
    /// </summary>
    public interface IRoomTypeService
    {
        /// <summary>
        /// Lấy toàn bộ danh sách các loại phòng hiện có trong hệ thống.
        /// </summary>
        /// <returns>Danh sách loại phòng</returns>
        Task<IEnumerable<RoomTypeResponseDto>> GetAllRoomTypesAsync();

        /// <summary>
        /// Lấy chi tiết thông tin loại phòng theo Id.
        /// </summary>
        /// <param name="id">Id của loại phòng cần lấy</param>
        /// <returns>Thông tin loại phòng hoặc null nếu không tìm thấy</returns>
        Task<RoomTypeResponseDto?> GetRoomTypeByIdAsync(Guid id);

        /// <summary>
        /// Tạo mới một loại phòng trong hệ thống.
        /// </summary>
        /// <param name="request">Thông tin yêu cầu tạo loại phòng</param>
        /// <returns>True nếu tạo thành công, ngược lại là False</returns>
        Task<bool> CreateRoomTypeAsync(RoomTypeRequestDto request);

        /// <summary>
        /// Cập nhật thông tin loại phòng hiện có.
        /// </summary>
        /// <param name="id">Id của loại phòng cần sửa</param>
        /// <param name="request">Thông tin cập nhật mới</param>
        /// <returns>True nếu cập nhật thành công, ngược lại là False</returns>
        Task<bool> UpdateRoomTypeAsync(Guid id, RoomTypeRequestDto request);

        /// <summary>
        /// Xóa mềm một loại phòng khỏi hệ thống.
        /// </summary>
        /// <param name="id">Id loại phòng cần xóa</param>
        /// <returns>True nếu xóa thành công, ngược lại là False</returns>
        Task<bool> DeleteRoomTypeAsync(Guid id);
    }
}