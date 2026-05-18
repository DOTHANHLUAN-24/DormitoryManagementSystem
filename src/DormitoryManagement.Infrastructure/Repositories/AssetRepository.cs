using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(ApplicationDbContext db) : base(db)
        {
        }

        public override async Task<Asset?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Room)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<Asset?> GetByAssetCodeAsync(string assetCode)
        {
            if (string.IsNullOrWhiteSpace(assetCode))
            {
                return null;
            }

            return await _dbSet
                .Include(a => a.Room)
                .FirstOrDefaultAsync(a => !a.IsDeleted && a.AssetCode == assetCode);
        }

        public async Task<IEnumerable<Asset>> GetActiveAssetsByRoomIdAsync(Guid roomId)
        {
            return await _dbSet
                .Where(a => a.RoomId == roomId && a.IsActive && !a.IsDeleted)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> IsAssetActiveAsync(Guid assetId)
        {
            var asset = await _dbSet.FindAsync(assetId);
            return asset != null && asset.IsActive && !asset.IsDeleted;
        }

        public async Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status)
        {
            return await _dbSet
                .Where(a => !a.IsDeleted && a.Status == status)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }
    }
}
