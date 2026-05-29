using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Triển khai dịch vụ quản lý phụ phí (Surcharge).
    /// </summary>
    public class SurchargeService(
        ISurchargeRepository surchargeRepository,
        IInvoiceRepository invoiceRepository,
        IUnitOfWork unitOfWork) : ISurchargeService
    {
        private readonly ISurchargeRepository _surchargeRepository = surchargeRepository;
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Task<IEnumerable<Surcharge>> GetByInvoiceIdAsync(Guid invoiceId)
        {
            return _surchargeRepository.GetByInvoiceIdAsync(invoiceId);
        }

        public Task<IEnumerable<Surcharge>> GetActiveAsync()
        {
            return _surchargeRepository.GetActiveAsync();
        }

        public async Task<bool> CreateSurchargeAsync(Guid invoiceId, string surchargeName, decimal amount, bool isActive = true)
        {
            if (invoiceId == Guid.Empty) return false;
            if (string.IsNullOrWhiteSpace(surchargeName)) return false;
            if (amount <= 0) return false;

            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
            if (invoice == null || invoice.IsDeleted || !invoice.IsActive) return false;

            var surcharge = new Surcharge
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                LastModified = null,

                IsActive = isActive,
                IsDeleted = false,

                SurchargeName = surchargeName.Trim(),
                Amount = amount,

                InvoiceId = invoiceId
            };

            await _surchargeRepository.AddAsync(surcharge);

            // Cập nhật TotalAmount của hóa đơn tương ứng
            invoice.TotalAmount += amount;
            await _invoiceRepository.UpdateAsync(invoice);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSurchargeAsync(Guid id, string surchargeName, decimal amount, bool isActive)
        {
            if (id == Guid.Empty) return false;
            if (string.IsNullOrWhiteSpace(surchargeName)) return false;
            if (amount <= 0) return false;

            var surcharge = await _surchargeRepository.GetByIdAsync(id);
            if (surcharge == null || surcharge.IsDeleted) return false;

            var oldAmount = surcharge.Amount;

            surcharge.SurchargeName = surchargeName.Trim();
            surcharge.Amount = amount;
            surcharge.IsActive = isActive;
            surcharge.LastModified = DateTime.Now;

            await _surchargeRepository.UpdateAsync(surcharge);

            // Cập nhật TotalAmount của hóa đơn tương ứng
            var invoice = await _invoiceRepository.GetByIdAsync(surcharge.InvoiceId);
            if (invoice != null)
            {
                invoice.TotalAmount = invoice.TotalAmount - oldAmount + amount;
                await _invoiceRepository.UpdateAsync(invoice);
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteSurchargeAsync(Guid id)
        {
            if (id == Guid.Empty) return false;

            var surcharge = await _surchargeRepository.GetByIdAsync(id);
            if (surcharge == null || surcharge.IsDeleted) return false;

            await _surchargeRepository.DeleteAsync(surcharge, isSoftDelete: true);

            // Cập nhật TotalAmount của hóa đơn tương ứng
            var invoice = await _invoiceRepository.GetByIdAsync(surcharge.InvoiceId);
            if (invoice != null)
            {
                invoice.TotalAmount -= surcharge.Amount;
                await _invoiceRepository.UpdateAsync(invoice);
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RestoreSurchargeAsync(Guid id)
        {
            if (id == Guid.Empty) return false;

            var surcharge = await _surchargeRepository.GetQuery().FirstOrDefaultAsync(s => s.Id == id);
            if (surcharge == null || !surcharge.IsDeleted) return false;

            await _surchargeRepository.RestoreAsync(surcharge);

            // Cập nhật TotalAmount của hóa đơn tương ứng
            var invoice = await _invoiceRepository.GetByIdAsync(surcharge.InvoiceId);
            if (invoice != null)
            {
                invoice.TotalAmount += surcharge.Amount;
                await _invoiceRepository.UpdateAsync(invoice);
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<Surcharge>> GetPagedSurchargesAsync(int pageIndex, int pageSize, string? searchString, bool? isActive = null, bool? isDeleted = false)
        {
            var query = _surchargeRepository.GetQuery()
                .Include(s => s.Invoice)
                    .ThenInclude(i => i.Contract)
                        .ThenInclude(c => c.User)
                .Include(s => s.Invoice)
                    .ThenInclude(i => i.Contract)
                        .ThenInclude(c => c.Bed)
                            .ThenInclude(b => b.Room)
                .Where(s => s.IsDeleted == (isDeleted ?? false));

            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.SurchargeName.Contains(searchString) || 
                                         (s.Invoice != null && s.Invoice.InvoiceCode.Contains(searchString)) || 
                                         (s.Invoice != null && s.Invoice.Contract != null && s.Invoice.Contract.User != null && s.Invoice.Contract.User.FullName.Contains(searchString)));
            }

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(s => s.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Surcharge>(items, totalCount, pageIndex, pageSize);
        }

        public async Task<PagedResult<Surcharge>> GetPagedSurchargesByUserIdAsync(Guid userId, int pageIndex, int pageSize, string? searchString, bool? isActive = null)
        {
            var query = _surchargeRepository.GetQuery()
                .Include(s => s.Invoice)
                    .ThenInclude(i => i.Contract)
                        .ThenInclude(c => c.User)
                .Include(s => s.Invoice)
                    .ThenInclude(i => i.Contract)
                        .ThenInclude(c => c.Bed)
                            .ThenInclude(b => b.Room)
                .Where(s => !s.IsDeleted && s.Invoice.Contract.UserId == userId);

            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.SurchargeName.Contains(searchString) || 
                                         (s.Invoice != null && s.Invoice.InvoiceCode.Contains(searchString)));
            }

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(s => s.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Surcharge>(items, totalCount, pageIndex, pageSize);
        }
    }
}
