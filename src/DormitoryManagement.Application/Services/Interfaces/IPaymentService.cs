using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý thanh toán (Payment).
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Tìm thanh toán theo mã giao dịch.
        /// </summary>
        Task<Payment?> GetByTransactionCodeAsync(string transactionCode);

        /// <summary>
        /// Lấy danh sách thanh toán theo Id hóa đơn (InvoiceId).
        /// </summary>
        Task<IEnumerable<Payment>> GetByInvoiceIdAsync(Guid invoiceId);

        /// <summary>
        /// Tạo mới một thanh toán cho hóa đơn.
        /// </summary>
        Task<bool> CreatePaymentAsync(
            Guid invoiceId,
            decimal amountPaid,
            DateTime paymentDate,
            string transactionCode,
            PaymentMethod method,
            string note);

        /// <summary>
        /// Xóa (soft delete) thanh toán theo Id.
        /// </summary>
        Task<bool> DeletePaymentAsync(Guid id);
    }
}
