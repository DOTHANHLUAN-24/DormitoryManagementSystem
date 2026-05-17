using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Interfaces.Services
{
    public interface IRoomService
    {
        // --- TRUY VẤN (QUERY) ---
        Task<PagedResult<RoomResponse>> GetPagedRoomsAsync(RoomFilterRequest filter);
        Task<PagedResult<RoomResponse>> GetDeletedRoomsPagedAsync(RoomFilterRequest filter);
        Task<RoomDetailResponse?> GetRoomByIdAsync(Guid id);
        Task<IEnumerable<RoomResponse>> GetRoomsByBlockAsync(Guid blockId);

        // --- THAO TÁC (COMMAND) ---
        Task<bool> CreateRoomAsync(CreateRoomRequest request);
        Task<bool> UpdateRoomAsync(Guid id, UpdateRoomRequest request);
        Task<bool> DeleteRoomAsync(Guid id);
        Task<bool> RestoreRoomAsync(Guid id);
        Task<bool> DeletePermanentlyAsync(Guid id);
    }
}