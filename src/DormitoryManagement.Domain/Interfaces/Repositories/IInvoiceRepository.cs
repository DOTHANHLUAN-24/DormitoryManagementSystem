using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin hóa đơn (Invoice).
    /// </summary>
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        /// <summary>
        /// Tìm hóa đơn theo mã hóa đơn (InvoiceCode).
        /// </summary>
        /// <param name="invoiceCode">Mã hóa đơn cần tìm</param>
        /// <returns>Hóa đơn nếu tìm thấy, ngược lại là null</returns>
        Task<Invoice?> GetByInvoiceCodeAsync(string invoiceCode);

        /// <summary>
        /// Lấy đối tượng truy vấn phân trang và tìm kiếm hóa đơn theo từ khóa.
        /// </summary>
        /// <param name="searchString">Từ khóa tìm kiếm (mã hóa đơn, tiêu đề...)</param>
        /// <returns>Đối tượng IQueryable chứa danh sách hóa đơn tìm được</returns>
        IQueryable<Invoice> GetPagingQuery(string searchString);

        /// <summary>
        /// Lấy danh sách hóa đơn theo Id hợp đồng (ContractId).
        /// </summary>
        /// <param name="contractId">Id của hợp đồng thuê phòng</param>
        /// <returns>Danh sách hóa đơn liên kết với hợp đồng</returns>
        Task<IEnumerable<Invoice>> GetByContractIdAsync(Guid contractId);
    }
}
