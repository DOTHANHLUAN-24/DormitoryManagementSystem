using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository triển khai quản lý thông tin thanh toán (Payment).
    /// </summary>
    public class PaymentRepository(ApplicationDbContext db) : BaseRepository<Payment>(db), IPaymentRepository
    {

        /// <summary>
        /// Tìm thanh toán theo mã giao dịch (TransactionCode).
        /// </summary>
        public async Task<Payment?> GetByTransactionCodeAsync(string transactionCode)
        {
            if (string.IsNullOrWhiteSpace(transactionCode))
            {
                return null;
            }

            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Invoice)
                .FirstOrDefaultAsync(p => p.TransactionCode == transactionCode);
        }

        /// <summary>
        /// Lấy danh sách thanh toán theo InvoiceId.
        /// </summary>
        public async Task<IEnumerable<Payment>> GetByInvoiceIdAsync(Guid invoiceId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Invoice)
                .Where(p => p.InvoiceId == invoiceId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }
    }
}
