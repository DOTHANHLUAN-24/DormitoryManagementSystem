using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAllAsync();
    }
}