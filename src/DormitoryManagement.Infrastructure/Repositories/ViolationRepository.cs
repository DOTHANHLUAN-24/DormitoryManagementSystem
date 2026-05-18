using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class ViolationRepository : BaseRepository<Violation>, IViolationRepository
    {
        public ViolationRepository(ApplicationDbContext db) : base(db) { }

        public override async Task<Violation?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
        public async Task<Violation?> GetByEvidenceImageAsync(string evidenceImage)
        {
            if (string.IsNullOrEmpty(evidenceImage))
            {
                return null;
            }
            return await _dbSet
            .Include(v => v.Contract)
            .FirstOrDefaultAsync(v => !v.IsDeleted && v.EvidenceImage == evidenceImage);
        }
        public async Task<IEnumerable<Violation>> GetViolationsByContractIdAsync(Guid contractId)
        {
            return await _dbSet
            .Include(v => v.Contract)
            .Where(v => !v.IsDeleted && v.ContractId == contractId)
            .OrderByDescending(v => v.CreatedDate)
            .ToListAsync();
        }

        public async Task<bool> IsViolationResolvedAsync(Guid violationId)
        {
            var violation = await _dbSet.FindAsync(violationId);
            return violation != null && !violation.IsDeleted && violation.Status == ViolationStatus.Resolved;

        }

        public async Task<IEnumerable<Violation>> GetViolationsByStatusAsync(ViolationStatus status)
        {
            return await _dbSet
                .Where(v => !v.IsDeleted && v.Status == status)
                .OrderByDescending(v => v.CreatedDate)
                .ToListAsync();

        }
    }
}
