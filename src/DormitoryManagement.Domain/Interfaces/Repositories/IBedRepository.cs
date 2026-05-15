using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBedRepository : IBaseRepository<Bed>
    {
        Task<Bed?> GetByBedNumberAsync(string bedNumber);

        Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId);

        Task<bool> IsBedAvailableAsync(Guid bedId);
    }
}
