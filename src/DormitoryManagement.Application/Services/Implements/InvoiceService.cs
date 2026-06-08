using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý hóa đơn (InvoiceService).
    /// </summary>
    public class InvoiceService(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork) : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <summary>
        /// Tìm hóa đơn theo mã hóa đơn (InvoiceCode).
        /// </summary>
        public Task<Invoice?> GetByInvoiceCodeAsync(string invoiceCode)
        {
            return _invoiceRepository.GetByInvoiceCodeAsync(invoiceCode);
        }

        /// <summary>
        /// Lấy danh sách hóa đơn theo Id hợp đồng (ContractId).
        /// </summary>
        public Task<IEnumerable<Invoice>> GetByContractIdAsync(Guid contractId)
        {
            return _invoiceRepository.GetByContractIdAsync(contractId);
        }

        /// <summary>
        /// Lấy danh sách tất cả các hóa đơn.
        /// </summary>
        public Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return _invoiceRepository.GetAllAsync(includeDeleted: false);
        }

        /// <summary>
        /// Lấy danh sách hóa đơn phân trang, tìm kiếm và lọc theo trạng thái.
        /// </summary>
        public async Task<DormitoryManagement.Domain.Common.PagedResult<Invoice>> GetPagedInvoicesAsync(int pageIndex, int pageSize, string? searchString = null, DormitoryManagement.Domain.Enums.InvoiceStatus? status = null)
        {
            var query = _invoiceRepository.GetPagingQuery(searchString ?? "", status);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new DormitoryManagement.Domain.Common.PagedResult<Invoice>(items, totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// Lấy hóa đơn theo Id.
        /// </summary>
        public Task<Invoice?> GetByIdAsync(Guid id)
        {
            return _invoiceRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Tạo mới một hóa đơn.
        /// </summary>
        public async Task<bool> CreateInvoiceAsync(Invoice invoice)
        {
            await _invoiceRepository.AddAsync(invoice);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Cập nhật thông tin hóa đơn.
        /// </summary>
        public async Task<bool> UpdateInvoiceAsync(Invoice invoice)
        {
            var existingInvoice = await _invoiceRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == invoice.Id);
            if (existingInvoice == null) return false;

            existingInvoice.Title = invoice.Title;
            existingInvoice.BillingMonth = invoice.BillingMonth;
            existingInvoice.BillingYear = invoice.BillingYear;
            existingInvoice.DueDate = invoice.DueDate;
            existingInvoice.TotalAmount = invoice.TotalAmount;
            existingInvoice.Status = invoice.Status;
            
            await _invoiceRepository.UpdateAsync(existingInvoice);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa hóa đơn theo Id (xóa mềm).
        /// </summary>
        public async Task<bool> DeleteInvoiceAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null) return false;

            await _invoiceRepository.DeleteAsync(invoice, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<Invoice>> GetDeletedInvoicesAsync(int pageIndex, int pageSize, string? searchString = null)
        {
            System.Linq.Expressions.Expression<Func<Invoice, bool>>? predicate = null;
            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearch = searchString.ToLower().Trim();
                predicate = x => x.InvoiceCode.ToLower().Contains(lowerSearch) || x.Title.ToLower().Contains(lowerSearch);
            }

            var pagedData = await _invoiceRepository.GetByStatusPagedAsync(
                pageIndex, pageSize, null, true, predicate,
                x => x.Contract!, x => x.Contract!.User!);

            return pagedData;
        }

        public async Task<bool> RestoreInvoiceAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (invoice == null || !invoice.IsDeleted) return false;

            await _invoiceRepository.RestoreAsync(invoice);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (invoice == null) return false;

            await _invoiceRepository.DeleteAsync(invoice, false);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
