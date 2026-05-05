using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IContractRepository : IBaseRepository<Contract>
    {
        // tìm hợp đồng theo mã hợp đồng
        Task<Contract?> GetByContractCodeAsync(string contractCode);
        // Phân trang và tìm kiếm
        IQueryable<Contract> GetPagingQuery(string searchString);
        // Lấy ra danh sách hợp đồng của 1 user
        Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId);
        // Lấy ra hợp đồng theo giường 
        Task<Contract> GetByBedIdAsync(Guid bedId);
    }
}
