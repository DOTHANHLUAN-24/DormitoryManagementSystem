using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBedRepository : IBaseRepository<Bed>
    {
        Task<Bed?> GetByBedNumberAsync(string bedNumber);

        // Phân trang + tìm kiếm (trả về IQueryable để layer service/controller tự áp dụng paging)
        IQueryable<Bed> GetPagingQuery(string searchString);
    }
}
