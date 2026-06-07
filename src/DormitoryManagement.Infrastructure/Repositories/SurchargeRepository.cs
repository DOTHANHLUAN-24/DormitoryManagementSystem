using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository triển khai quản lý thông tin phụ phí (Surcharge).
    /// </summary>
    public class SurchargeRepository(ApplicationDbContext db) : BaseRepository<Surcharge>(db), ISurchargeRepository
    {
        /// <summary>
        /// Lấy danh sách phụ phí theo Id hóa đơn (InvoiceId).
        /// </summary>
        public async Task<IEnumerable<Surcharge>> GetByInvoiceIdAsync(Guid invoiceId)
        {
            if (invoiceId == Guid.Empty)
            {
                return Enumerable.Empty<Surcharge>();
            }

            return await _dbSet
                .AsNoTracking()
                .Include(s => s.Invoice)
                .Where(s => s.InvoiceId == invoiceId && !s.IsDeleted)
                .OrderByDescending(s => s.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy danh sách phụ phí đang hoạt động (IsActive = true, IsDeleted = false).
        /// </summary>
        public async Task<IEnumerable<Surcharge>> GetActiveAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(s => s.IsActive && !s.IsDeleted)
                .OrderByDescending(s => s.CreatedDate)
                .ToListAsync();
        }
    }
}
