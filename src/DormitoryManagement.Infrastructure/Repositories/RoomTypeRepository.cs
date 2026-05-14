using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class RoomTypeRepository : BaseRepository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<bool> IsTypeNameDuplicateAsync(string typeName, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(x => x.TypeName.ToLower() == typeName.ToLower()
                                             && x.Id != excludeId
                                             && !x.IsDeleted);
        }

        public async Task<RoomType?> GetRoomTypeWithRoomsAsync(Guid id)
        {
            return await _dbSet
                .Include(rt => rt.Rooms)
                .FirstOrDefaultAsync(rt => rt.Id == id && !rt.IsDeleted);
        }

        public async Task<Dictionary<string, int>> GetRoomCountByTypeAsync()
        {
            return await _dbSet
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