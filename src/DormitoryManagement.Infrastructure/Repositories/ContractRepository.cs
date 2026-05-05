using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
        public ContractRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Contract?> GetByContractCodeAsync(string contractCode)
        {
            return await _dbSet
                .Include(c => c.User)
                .Include(c => c.Bed)
                .FirstOrDefaultAsync(c => c.ContractCode == contractCode);
        }

        public IQueryable<Contract> GetPagingQuery(string searchString)
        {
            var query = _dbSet
                .Include(c => c.User)
                .Include(c => c.Bed)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c =>
                    c.ContractCode.Contains(searchString) ||
                    (c.User != null && c.User.UserName != null && c.User.UserName.Contains(searchString)) ||
                    (c.User != null && c.User.FullName != null && c.User.FullName.Contains(searchString))
                );
            }
            else
            {
                query = query.Where(c => false);
            }

            return query.OrderByDescending(c => c.CreatedDate);
        }

        public async Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Include(c => c.User)
                .Include(c => c.Bed)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<Contract> GetByBedIdAsync(Guid bedId)
        {
            var contract = await _dbSet
                .Include(c => c.User)
                .Include(c => c.Bed)
                .FirstOrDefaultAsync(c => c.BedId == bedId);

            // Interface yêu cầu trả về Task<Contract> (không nullable)
            return contract ?? new Contract();
        }
}
}
