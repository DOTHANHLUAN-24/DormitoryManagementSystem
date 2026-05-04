using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class ContractRepository : IContractRepository{
        public async Task<Contract> GetByContractCodeAsync (string contractCode)
        {
            return await _dbSet
                .Include(c => c.User)
                .Include(c => c.Bed)
                .FirstOrDefaultAsync(c => c.ContractCode == ContractCode);
        }

        public IQueryable<Contract> GetPagingQuery (string searchString)
        {
            var query = _dbSet
            .Include(c => c.User)
            .Include(c => c.Bed)
            .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(
                    c => ContractCode.Contains(searchString) ||
                    c.User.UserName!.Contains(searchString)

                );
            }

            return query.OrderByDescending(c => c.CreatedDate);
        }
        
        public Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
            .Include(c => c.User)
            .Include(c => c.Bed)
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedDate)
            .ToListAsync();
        }

        public Task<Contract> GetByBedIdAsync(Guid bedId)
        {
            return await _dbSet
            .Include(c => c.User)
            .Include(c => c.Bed)
            .FirstOrDefaultAsync(c => c.BedId == bedId);
        }
    
    }

    
}