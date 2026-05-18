using DormitoryManagement.Application.Dtos.Requests.Blocks;
using DormitoryManagement.Application.Dtos.Responses.Blocks;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IBlockService
    {
        // Truy vấn
        Task<PagedResult<BlockResponseDto>> GetActiveBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm);
        Task<PagedResult<BlockResponseDto>> GetDeletedBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm);
        Task<PagedResult<BlockResponseDto>> GetSuspendedBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm);

        Task<BlockResponseDto?> GetBlockByIdAsync(Guid id);
        Task<IEnumerable<BlockResponseDto>> GetAllBlocksAsync(); // Dùng cho Dropdown

        // Thao tác
        Task<bool> CreateBlockAsync(BlockRequestDto request);
        Task<bool> UpdateBlockAsync(Guid id, BlockRequestDto request);
        Task<bool> SoftDeleteBlockAsync(Guid id);
        Task<bool> RestoreBlockAsync(Guid id);
        Task<bool> DeletePermanentlyAsync(Guid id);
        Task<bool> ToggleBlockStatusAsync(Guid id);
    }
}
