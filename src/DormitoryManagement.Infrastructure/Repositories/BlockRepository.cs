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

        public async Task<IEnumerable<Block>> GetAllWithRoomCountAsync() 
            => await _dbSet.Include(b => b.Rooms).Where(b => !b.IsDeleted).ToListAsync();


        public async Task<Block?> GetBlockWithRoomsAsync(Guid id)
            => await _dbSet.Include(b => b.Rooms).FirstOrDefaultAsync(b => b.Id == id);

        public async Task<bool> IsBlockNameExistsAsync(string blockName, Guid? excludeId = null)
        {
            var query = _dbSet.AsQueryable();

            if (excludeId.HasValue)
            {
                query = query.Where(b => b.Id != excludeId.Value);
            }

            return await query.AnyAsync(b => b.BlockName.ToLower() == blockName.ToLower() && !b.IsDeleted);
        }

        public async Task<PagedResult<Block>> SearchBlocksAsync(string searchTerm, int pageIndex, int pageSize)
        {
            var query = _dbSet.AsNoTracking().Where(b => !b.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearch = searchTerm.ToLower();
                query = query.Where(b => b.BlockName.ToLower().Contains(lowerSearch)
                                      || b.Description.ToLower().Contains(lowerSearch));
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(b => b.BlockName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Block>(items, totalCount, pageIndex, pageSize);
        }
    }
}
