using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin thanh toán (Payment).
    /// </summary>
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        /// <summary>
        /// Tìm thanh toán theo mã giao dịch.
        /// </summary>
        Task<Payment?> GetByTransactionCodeAsync(string transactionCode);

        /// <summary>
        /// Lấy danh sách thanh toán theo Id hóa đơn (InvoiceId).
        /// </summary>
        Task<IEnumerable<Payment>> GetByInvoiceIdAsync(Guid invoiceId);
    }
}
