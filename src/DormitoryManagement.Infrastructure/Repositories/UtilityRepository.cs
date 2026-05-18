using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class UtilityRepository : BaseRepository<Utility>, IUtilityRepository
    {
        public UtilityRepository(ApplicationDbContext db) : base(db)
        {
        }

        public override async Task<Utility?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<Utility?> GetByUtilityNameAsync(string utilityName)
        {
            if (string.IsNullOrWhiteSpace(utilityName))
            {
                return null;
            }

            return await _dbSet.FirstOrDefaultAsync(u => !u.IsDeleted && u.UtilityName == utilityName);
        }

        public async Task<IEnumerable<Utility>> GetActiveUtilitiesAsync()
        {
            return await _dbSet
                .Where(u => u.IsActive && !u.IsDeleted)
                .OrderByDescending(u => u.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> IsUtilityActiveAsync(Guid utilityId)
        {
            var utility = await _dbSet.FindAsync(utilityId);
            return utility != null && utility.IsActive && !utility.IsDeleted;
        }
    }
}
