using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class RoomTypeRepository : BaseRepository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(ApplicationDbContext db) : base(db) { }

        public async Task<bool> IsTypeNameDuplicateAsync(string typeName, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(rt =>
                rt.TypeName.ToLower() == typeName.ToLower() &&
                rt.Id != excludeId &&
                !rt.IsDeleted);
        }

        public async Task<RoomType?> GetRoomTypeWithRoomsAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(rt => rt.Rooms.Where(r => !r.IsDeleted))
                .FirstOrDefaultAsync(rt => rt.Id == id && !rt.IsDeleted);
        }

        public async Task<Dictionary<string, int>> GetRoomCountByTypeAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(rt => !rt.IsDeleted)
                .Select(rt => new
                {
                    rt.TypeName,
                    Count = rt.Rooms.Count(r => !r.IsDeleted)
                })
                .ToDictionaryAsync(x => x.TypeName, x => x.Count);
        }
    }
}