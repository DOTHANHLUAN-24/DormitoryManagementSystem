using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Interfaces.Services
{
    public interface IRoomService
    {
        // Lấy danh sách phân trang và tìm kiếm
        Task<PagedResult<RoomResponse>> GetPagedRoomsAsync(RoomFilterRequest filter);

        // Lấy chi tiết một phòng
        Task<RoomDetailResponse?> GetRoomByIdAsync(Guid id);

        // Tạo mới phòng
        Task<Guid> CreateRoomAsync(CreateRoomRequest request);

        // Cập nhật thông tin phòng
        Task<bool> UpdateRoomAsync(Guid id, UpdateRoomRequest request);

        // Xóa phòng (mặc định xóa mềm)
        Task<bool> DeleteRoomAsync(Guid id);

        // Khôi phục phòng đã xóa
        Task<bool> RestoreRoomAsync(Guid id);

        // Lấy danh sách phòng theo Block (cho dropdown)
        Task<IEnumerable<RoomResponse>> GetRoomsByBlockAsync(Guid blockId);
    }
}