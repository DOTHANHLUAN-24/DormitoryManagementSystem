using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        IQueryable<Room> GetPagingQuery(string searchString, int pageIndex, int pageSize);

        Task<IEnumerable<Room>> ListAllRoomAsync();

        Task<IEnumerable<RoomType>> ListAllRoomTypeAsync();
    }
}