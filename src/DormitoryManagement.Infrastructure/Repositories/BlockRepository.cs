using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class BlockRepository : BaseRepository<Block>, IBlockRepository
    {
        public BlockRepository(ApplicationDbContext db) : base(db) { }

        public async Task<Block?> GetBlockWithRoomsAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Rooms.Where(r => !r.IsDeleted)) // Chỉ lấy các phòng chưa bị xóa
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<bool> IsBlockNameExistsAsync(string blockName, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(b =>
                b.BlockName.ToLower() == blockName.ToLower() &&
                b.Id != excludeId &&
                !b.IsDeleted);
        }

        public async Task<PagedResult<Block>> SearchBlocksAsync(string searchTerm, int pageIndex, int pageSize)
        {
            // Tận dụng hàm GetByStatusPagedAsync đã có ở BaseRepository của bạn
            return await GetByStatusPagedAsync(
                pageIndex,
                pageSize,
                isActive: null, // Lấy cả active và inactive
                isDeleted: false,
                predicate: b => string.IsNullOrEmpty(searchTerm) ||
                                b.BlockName.Contains(searchTerm) ||
                                b.Description.Contains(searchTerm)
            );
        }
    }
}
