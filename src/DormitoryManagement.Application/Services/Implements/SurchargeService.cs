using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Application.Services.Interfaces;

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
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSurchargeAsync(Guid id, string surchargeName, decimal amount, bool isActive)
        {
            if (id == Guid.Empty) return false;
            if (string.IsNullOrWhiteSpace(surchargeName)) return false;
            if (amount <= 0) return false;

            var surcharge = await _surchargeRepository.GetByIdAsync(id);
            if (surcharge == null || surcharge.IsDeleted) return false;

            surcharge.SurchargeName = surchargeName.Trim();
            surcharge.Amount = amount;
            surcharge.IsActive = isActive;
            surcharge.LastModified = DateTime.Now;

            await _surchargeRepository.UpdateAsync(surcharge);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteSurchargeAsync(Guid id)
        {
            if (id == Guid.Empty) return false;

            var surcharge = await _surchargeRepository.GetByIdAsync(id);
            if (surcharge == null || surcharge.IsDeleted) return false;

            await _surchargeRepository.DeleteAsync(surcharge, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RestoreSurchargeAsync(Guid id)
        {
            if (id == Guid.Empty) return false;

            var surcharge = await _surchargeRepository.GetByIdAsync(id);
            if (surcharge == null || !surcharge.IsDeleted) return false;

            await _surchargeRepository.RestoreAsync(surcharge);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
