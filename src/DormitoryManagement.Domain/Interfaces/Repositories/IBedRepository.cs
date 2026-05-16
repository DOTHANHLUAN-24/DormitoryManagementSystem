using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBedRepository : IBaseRepository<Bed>
    {
        Task<Bed?> GetByBedNumberAsync(string bedNumber);

<<<<<<< HEAD
        // Tìm giường theo từ tìm kiếm
        // IEnumerable<Bed> GetPagingQuery(string searchString);

=======
>>>>>>> a9a730fcf3697072403b64ac1c9df1a1e2abef15
        Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId);

        Task<bool> IsBedAvailableAsync(Guid bedId);
    }
}
