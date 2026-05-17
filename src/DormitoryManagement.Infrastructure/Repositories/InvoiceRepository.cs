using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext db) : base(db)
        {

        }
        public async Task<Invoice?> GetByInvoiceCodeAsync(string invoiceCode)
        {
            if (string.IsNullOrWhiteSpace(invoiceCode))
            {
                return null;
            }

            return await _dbSet
            .Include(i => i.Contract)
                .ThenInclude(c => c.User)
            .Include(i => i.Contract)
                .ThenInclude(c => c.Bed)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.InvoiceCode == invoiceCode);

        }
        public IQueryable<Invoice> GetPagingQuery(string searchString)
        {
            var query = _dbSet
            .Include(i => i.Contract)
                .ThenInclude(c => c.User)
            .Include(i => i.Contract)
                .ThenInclude(c => c.Bed)
            .Include(i => i.Payments)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(i => i.InvoiceCode.Contains(searchString)
                || i.Title.Contains(searchString));
            }
            else
            {
                query = query.Where(i => false);
            }

            return query.OrderByDescending(i => i.CreatedDate);
        }
        public async Task<IEnumerable<Invoice>> GetByContractIdAsync(Guid contractId)
        {
            return await _dbSet
            .Include(i => i.Contract)
                .ThenInclude(c => c.User)
            .Include(i => i.Contract)
                .ThenInclude(c => c.Bed)
            .Include(i => i.Payments)
            .Where(i => i.ContractId == contractId)
            .OrderByDescending(i => i.CreatedDate)
            .ToListAsync();
        }
    }
}
