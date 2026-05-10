using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBedRepository : IBaseRepository<Bed>
    {
        // Lấy danh sách giường
        Task<Bed?> GetByBedNumberAsync(string bedNumber);
        // Tìm giường theo từ tìm kiếm
        IEnumerable<Bed> GetPagingQuery(string searchString);
    }
}
