using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBedRepository : IBaseRepository<Bed>
    {
        Task<(List<Bed> Beds, int TotalCount)> GetActiveBedsPagedAsync(int page, int pageSize, string? search);

        Task<List<Bed>> GetAllBedByStatusAsync(BedStatus status);
    }
}
