using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin hóa đơn (Invoice).
    /// </summary>
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        /// <summary>
        /// Khởi tạo InvoiceRepository.
        /// </summary>
        /// <param name="db">ApplicationDbContext kết nối database</param>
        public InvoiceRepository(ApplicationDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Tìm hóa đơn theo mã hóa đơn (InvoiceCode).
        /// </summary>
        /// <param name="invoiceCode">Mã hóa đơn cần tìm</param>
        /// <returns>Hóa đơn kèm thông tin Hợp đồng, Sinh viên, Giường và Lịch sử thanh toán</returns>
        public async Task<Invoice?> GetByInvoiceCodeAsync(string invoiceCode)
        {
            if (string.IsNullOrWhiteSpace(invoiceCode))
            {
                return null;
            }

            return await _dbSet
                .AsNoTracking()
                .Include(i => i.Contract)
                    .ThenInclude(c => c.User)
                .Include(i => i.Contract)
                    .ThenInclude(c => c.Bed)
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.InvoiceCode == invoiceCode);
        }

        /// <summary>
        /// Lấy đối tượng truy vấn phân trang và tìm kiếm hóa đơn theo từ khóa.
        /// </summary>
        /// <param name="searchString">Từ khóa tìm kiếm (mã hóa đơn hoặc tiêu đề)</param>
        /// <param name="status">Trạng thái hóa đơn cần lọc</param>
        /// <returns>Đối tượng IQueryable chứa danh sách hóa đơn</returns>
        public IQueryable<Invoice> GetPagingQuery(string searchString, DormitoryManagement.Domain.Enums.InvoiceStatus? status = null)
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
                    || i.Title.Contains(searchString)
                    || (i.Contract != null && i.Contract.User != null && (i.Contract.User.FullName.Contains(searchString) || i.Contract.User.Code.Contains(searchString))));
            }

            if (status.HasValue)
            {
                query = query.Where(i => i.Status == status.Value);
            }

            return query.OrderByDescending(i => i.CreatedDate);
        }

        /// <summary>
        /// Lấy danh sách hóa đơn theo Id hợp đồng (ContractId).
        /// </summary>
        /// <param name="contractId">Id của hợp đồng</param>
        /// <returns>Danh sách hóa đơn</returns>
        public async Task<IEnumerable<Invoice>> GetByContractIdAsync(Guid contractId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(i => i.Contract)
                    .ThenInclude(c => c.User)
                .Include(i => i.Contract)
                    .ThenInclude(c => c.Bed)
                        .ThenInclude(b => b.Room)
                            .ThenInclude(r => r.RoomType)
                .Include(i => i.Payments)
                .Include(i => i.UtilityUsages)
                    .ThenInclude(u => u.Utility)
                .Include(i => i.Surcharges)
                .Where(i => i.ContractId == contractId)
                .OrderByDescending(i => i.CreatedDate)
                .ToListAsync();
        }
    }
}
