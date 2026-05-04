using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetActiveRoomsAsync();
        
        Task<Room?> GetRoomDetailAsync(Guid id);
        
        Task CreateNewRoomAsync(Room room);
        
        Task UpdateRoomInfoAsync(Room room);
        
        Task RemoveRoomAsync(Guid id);
    }
}
