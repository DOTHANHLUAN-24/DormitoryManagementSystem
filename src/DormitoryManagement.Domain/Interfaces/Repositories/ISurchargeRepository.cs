using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin phụ phí (Surcharge).
    /// Lưu ý: Trong schema hiện tại, Surcharge đang được gắn với InvoiceId.
    /// </summary>
    public interface ISurchargeRepository : IBaseRepository<Surcharge>
    {
        /// <summary>
        /// Lấy danh sách phụ phí theo Id hóa đơn (InvoiceId).
        /// </summary>
        Task<IEnumerable<Surcharge>> GetByInvoiceIdAsync(Guid invoiceId);

        /// <summary>
        /// Lấy danh sách phụ phí đang hoạt động (IsActive = true, IsDeleted = false).
        /// </summary>
        Task<IEnumerable<Surcharge>> GetActiveAsync();
    }
}
