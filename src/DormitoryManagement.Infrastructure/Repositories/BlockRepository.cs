using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class BlockRepository : BaseRepository<Block>, IBlockRepository
    {
        public BlockRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Block?> GetBlockWithRoomsAsync(Guid id)
        {
            return await _dbSet
                .Include(b => b.Rooms.Where(r => !r.IsDeleted)) // Chỉ lấy các phòng chưa xóa
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<bool> IsBlockNameExistsAsync(string blockName, Guid? excludeId = null)
        {
            var query = _dbSet.Where(x => !x.IsDeleted && x.BlockName.ToLower() == blockName.ToLower());

            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
