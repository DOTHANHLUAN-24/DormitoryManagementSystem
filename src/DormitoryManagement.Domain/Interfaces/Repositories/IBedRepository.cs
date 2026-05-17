using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBedRepository : IBaseRepository<Bed>
    {
        Task<Bed?> GetByBedNumberAsync(string bedNumber);

        // Tìm giường theo từ tìm kiếm
        IEnumerable<Bed> GetPagingQuery(string searchString);

        Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId);

        Task<bool> IsBedAvailableAsync(Guid bedId);
    }
}