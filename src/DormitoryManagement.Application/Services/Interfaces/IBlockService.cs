using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IBlockService
    {
        Task<IEnumerable<Block>> GetAllBlocksAsync();
    }
}