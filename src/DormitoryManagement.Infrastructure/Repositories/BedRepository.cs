using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class BedRepository : BaseRepository<Bed>, IBedRepository
    {
        public BedRepository(ApplicationDbContext db) : base(db)
        {
        }

        public override async Task<Bed?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(b => b.Room)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public override async Task<IEnumerable<Bed>> GetAllAsync(bool includeDeleted = false)
        {
            var query = _dbSet
                .Include(b => b.Room)
                .AsNoTracking();

            if (!includeDeleted)
            {
                query = query.Where(b => !b.IsDeleted);
            }

            return await query
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();
        }

        public async Task<Bed?> GetByBedNumberAsync(string bedNumber)
        {
            if (string.IsNullOrEmpty(bedNumber))
            {
                return null;
            }

            return await _dbSet
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.BedNumber == bedNumber);
        }

        public IEnumerable<Bed> GetPagingQuery(string searchString)
        {
            var query = _dbSet
                .Include(b => b.Room)
                .Where(b => !b.IsDeleted)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(searchString))
            {
                return query
                    .Where(_ => false)
                    .OrderByDescending(b => b.CreatedDate)
                    .ToList();
            }

            query = query.Where(b => b.BedNumber.Contains(searchString));

            return query
                .OrderByDescending(b => b.CreatedDate)
                .ToList();
        }
    }
}
