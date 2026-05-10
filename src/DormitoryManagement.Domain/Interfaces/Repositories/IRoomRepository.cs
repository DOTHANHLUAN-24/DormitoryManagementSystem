using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<Room?> GetRoomWithDetailsAsync(Guid id);

        Task<PagedResult<Room>> SearchRoomsAsync(
            string? searchTerm,
            Guid? blockId,
            Guid? roomTypeId,
            RoomStatus? status,
            int pageIndex,
            int pageSize);

        Task<bool> IsRoomNumberDuplicateAsync(string roomNumber, Guid blockId, Guid? excludeId = null);

        Task<IEnumerable<Room>> GetRoomsByBlockAsync(Guid blockId);
    }
}