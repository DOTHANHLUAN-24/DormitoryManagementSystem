using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext db) : base(db) { }

        public override async Task<Room?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(r => r.Block)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<Room?> GetRoomWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(r => r.Block)
                .Include(r => r.RoomType)
                .Include(r => r.Beds)
                .Include(r => r.Assets)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<PagedResult<Room>> SearchRoomsAsync(
            string? searchTerm,
            Guid? blockId,
            Guid? roomTypeId,
            RoomStatus? status,
            int pageIndex,
            int pageSize)
        {
            var query = _dbSet
                .Include(r => r.Block)
                .Include(r => r.RoomType)
                .AsNoTracking()
                .Where(x => !x.IsDeleted);

            // Filter logic
            if (blockId.HasValue && blockId != Guid.Empty)
                query = query.Where(r => r.BlockId == blockId.Value);

            if (roomTypeId.HasValue && roomTypeId != Guid.Empty)
                query = query.Where(r => r.RoomTypeId == roomTypeId.Value);

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(r => r.RoomNumber.Contains(searchTerm));

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(r => r.Block.BlockName)
                .ThenBy(r => r.RoomNumber)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Room>(items, totalCount, pageIndex, pageSize);
        }

        public async Task<bool> IsRoomNumberDuplicateAsync(string roomNumber, Guid blockId, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(r =>
                r.RoomNumber.ToLower() == roomNumber.ToLower() &&
                r.BlockId == blockId &&
                r.Id != (excludeId ?? Guid.Empty) &&
                !r.IsDeleted);
        }

        public async Task<IEnumerable<Room>> GetRoomsByBlockAsync(Guid blockId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(r => r.BlockId == blockId && !r.IsDeleted)
                .ToListAsync();
        }
    }
}