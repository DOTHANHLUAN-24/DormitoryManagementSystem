using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetAllRoomTypesAsync();
    }
}