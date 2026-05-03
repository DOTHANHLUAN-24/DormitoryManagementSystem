using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        IQueryable<Room> GetPagingQuery(string searchString);

        Task<IEnumerable<Room>> ListAllRoomAsync();

        Task<IEnumerable<RoomType>> ListAllRoomTypeAsync();
    }
}