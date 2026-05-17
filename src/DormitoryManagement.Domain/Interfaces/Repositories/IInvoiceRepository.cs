using System.Linq;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        // Tìm hóa đơn theo mã hóa đơn
        Task<Invoice?> GetByInvoiceCodeAsync(string invoiceCode);

        // Phân trang và tìm kiếm (tìm theo InvoiceCode/Title theo searchString tùy implement)
        IQueryable<Invoice> GetPagingQuery(string searchString);

        // Lấy danh sách hóa đơn theo ContractId
        Task<IEnumerable<Invoice>> GetByContractIdAsync(Guid contractId);
    }
}
