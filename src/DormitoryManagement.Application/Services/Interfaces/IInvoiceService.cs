using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý hóa đơn (Invoice).
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// Tìm hóa đơn theo mã hóa đơn (InvoiceCode).
        /// </summary>
        Task<Invoice?> GetByInvoiceCodeAsync(string invoiceCode);

        /// <summary>
        /// Lấy danh sách hóa đơn theo Id hợp đồng (ContractId).
        /// </summary>
        Task<IEnumerable<Invoice>> GetByContractIdAsync(Guid contractId);

        /// <summary>
        /// Lấy danh sách tất cả các hóa đơn.
        /// </summary>
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();

        /// <summary>
        /// Lấy danh sách hóa đơn phân trang, tìm kiếm và lọc theo trạng thái.
        /// </summary>
        Task<DormitoryManagement.Domain.Common.PagedResult<Invoice>> GetPagedInvoicesAsync(int pageIndex, int pageSize, string? searchString = null, DormitoryManagement.Domain.Enums.InvoiceStatus? status = null);

        /// <summary>
        /// Lấy hóa đơn theo Id.
        /// </summary>
        Task<Invoice?> GetByIdAsync(Guid id);

        /// <summary>
        /// Tạo mới một hóa đơn.
        /// </summary>
        Task<bool> CreateInvoiceAsync(Invoice invoice);

        /// <summary>
        /// Cập nhật thông tin hóa đơn.
        /// </summary>
        Task<bool> UpdateInvoiceAsync(Invoice invoice);

        /// <summary>
        /// Xóa hóa đơn theo Id (xóa mềm).
        /// </summary>
        Task<bool> DeleteInvoiceAsync(Guid id);

        /// <summary>
        /// Lấy danh sách hóa đơn đã bị xóa phân trang.
        /// </summary>
        Task<DormitoryManagement.Domain.Common.PagedResult<Invoice>> GetDeletedInvoicesAsync(int pageIndex, int pageSize, string? searchString = null);

        /// <summary>
        /// Khôi phục hóa đơn đã bị xóa mềm.
        /// </summary>
        Task<bool> RestoreInvoiceAsync(Guid id);

        /// <summary>
        /// Xóa vĩnh viễn hóa đơn khỏi database.
        /// </summary>
        Task<bool> DeletePermanentlyAsync(Guid id);
    }
}
