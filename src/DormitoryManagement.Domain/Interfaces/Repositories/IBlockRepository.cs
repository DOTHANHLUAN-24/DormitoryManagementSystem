using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBlockRepository
    {
        Task<IEnumerable<Block>> GetAllAsync();
    }
}