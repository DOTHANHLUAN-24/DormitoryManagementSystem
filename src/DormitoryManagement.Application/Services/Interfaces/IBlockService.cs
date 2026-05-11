using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IBlockService
    {
        // Truy vấn dữ liệu
        Task<IEnumerable<Block>> GetAllBlocksAsync(bool includeDeleted = false);
        Task<PagedResult<Block>> GetPagedBlocksAsync(int pageIndex, int pageSize, string? searchTerm = null);
        Task<Block?> GetBlockByIdAsync(Guid id);
        Task<Block?> GetBlockWithRoomsAsync(Guid id);

        // Thao tác nghiệp vụ
        Task CreateBlockAsync(Block block);
        Task UpdateBlockAsync(Block block);
        Task DeleteBlockAsync(Guid id, bool isSoftDelete = true);
        Task RestoreBlockAsync(Guid id);

        // Kiểm tra
        Task<bool> IsNameDuplicateAsync(string name, Guid? excludeId = null);
    }
}
