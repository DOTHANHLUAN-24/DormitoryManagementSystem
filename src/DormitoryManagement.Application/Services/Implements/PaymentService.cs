using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Triển khai dịch vụ quản lý thanh toán (Payment) theo hướng nghiệp vụ:
    /// - Validate đầu vào
    /// - Chống trùng TransactionCode (payment còn hiệu lực)
    /// - Tạo/xóa (soft delete) payment
    /// - Recalculate trạng thái Invoice dựa trên tổng AmountPaid còn hiệu lực
    /// </summary>
    public class PaymentService(
        IPaymentRepository paymentRepository,
        IInvoiceRepository invoiceRepository,
        IUnitOfWork unitOfWork) : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository = paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Task<Payment?> GetByTransactionCodeAsync(string transactionCode)
        {
            return _paymentRepository.GetByTransactionCodeAsync(transactionCode);
        }

        public Task<IEnumerable<Payment>> GetByInvoiceIdAsync(Guid invoiceId)
        {
            return _paymentRepository.GetByInvoiceIdAsync(invoiceId);
        }

        public async Task<PagedResult<Payment>> GetPagedPaymentsAsync(
            int pageIndex,
            int pageSize,
            string? searchString = null,
            Guid? userId = null)
        {
            var query = _paymentRepository.GetQuery()
                .Include(p => p.Invoice)
                    .ThenInclude(i => i.Contract)
                        .ThenInclude(c => c.User)
                .Include(p => p.Invoice)
                    .ThenInclude(i => i.Contract)
                        .ThenInclude(c => c.Bed)
                            .ThenInclude(b => b.Room)
                                .ThenInclude(r => r.Block)
                .Where(p => p.IsActive && !p.IsDeleted)
                .AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(p => p.Invoice.Contract.UserId == userId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var search = searchString.Trim().ToLower();
                query = query.Where(p => p.TransactionCode.ToLower().Contains(search)
                    || p.Invoice.InvoiceCode.ToLower().Contains(search)
                    || p.Invoice.Contract.User.FullName.ToLower().Contains(search)
                    || p.Invoice.Contract.User.Code.ToLower().Contains(search)
                    || p.Invoice.Contract.Bed.Room.RoomNumber.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.PaymentDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Payment>(items, totalCount, pageIndex, pageSize);
        }

        public async Task<bool> CreatePaymentAsync(
            Guid invoiceId,
            decimal amountPaid,
            DateTime paymentDate,
            string transactionCode,
            PaymentMethod method,
            string note)
        {
            // Validate đầu vào: đảm bảo input hợp lệ trước khi tạo payment
            if (invoiceId == Guid.Empty) return false;
            if (amountPaid <= 0) return false;
            if (paymentDate == default) return false;
            if (string.IsNullOrWhiteSpace(transactionCode)) return false;

            var code = transactionCode.Trim();

            // Kiểm tra invoice tồn tại & đang active
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
            if (invoice == null || invoice.IsDeleted || !invoice.IsActive) return false;

            // Chống trùng TransactionCode (chỉ tính payment còn hiệu lực)
            var existingPayment = await _paymentRepository.GetByTransactionCodeAsync(code);
            if (existingPayment != null && existingPayment.IsActive && !existingPayment.IsDeleted)
            {
                return false;
            }

            var now = DateTime.Now;

            // Tạo payment mới
            var payment = new Payment
            {
                InvoiceId = invoiceId,
                AmountPaid = amountPaid,
                PaymentDate = paymentDate,
                TransactionCode = code,
                Method = method,
                Note = note ?? string.Empty,

                // Audit/flags
                Id = Guid.NewGuid(),
                CreatedDate = now,
                LastModified = null,
                IsActive = true,
                IsDeleted = false
            };

            await _paymentRepository.AddAsync(payment);

            // Recalculate Invoice.Status sau khi tạo payment
            var totalPaid = await CalculatePaidAmountAsync(invoiceId);
            invoice.Status = GetInvoiceStatusAfterPayments(invoice, totalPaid);

            await _invoiceRepository.UpdateAsync(invoice);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePaymentAsync(Guid id)
        {
            if (id == Guid.Empty) return false;

            // Lấy payment theo id
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null || payment.IsDeleted || !payment.IsActive) return false;

            // Soft delete payment
            await _paymentRepository.DeleteAsync(payment, isSoftDelete: true);

            // Recalculate Invoice.Status sau khi xóa payment
            var invoice = await _invoiceRepository.GetByIdAsync(payment.InvoiceId);
            if (invoice == null || invoice.IsDeleted || !invoice.IsActive)
            {
                return await _unitOfWork.SaveChangesAsync() > 0;
            }

            var totalPaid = await CalculatePaidAmountAsync(invoice.Id);
            invoice.Status = GetInvoiceStatusAfterPayments(invoice, totalPaid);

            await _invoiceRepository.UpdateAsync(invoice);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        private async Task<decimal> CalculatePaidAmountAsync(Guid invoiceId)
        {
            // Repository GetByInvoiceIdAsync hiện tại không lọc IsDeleted/IsActive,
            // nên Service cần lọc để soft delete không ảnh hưởng tổng thanh toán.
            var payments = await _paymentRepository.GetByInvoiceIdAsync(invoiceId);

            return payments
                .Where(p => p.IsActive && !p.IsDeleted)
                .Sum(p => p.AmountPaid);
        }

        private static InvoiceStatus GetInvoiceStatusAfterPayments(Invoice invoice, decimal totalPaid)
        {
            // Trường hợp invoice không hợp lệ
            if (invoice.TotalAmount <= 0)
            {
                return InvoiceStatus.Unpaid;
            }

            // Đủ tiền hoặc vượt tiền => Paid
            if (totalPaid >= invoice.TotalAmount)
            {
                return InvoiceStatus.Paid;
            }

            // Trả một phần => PartiallyPaid
            if (totalPaid > 0 && totalPaid < invoice.TotalAmount)
            {
                return InvoiceStatus.PartiallyPaid;
            }

            // Chưa trả gì => xét overdue/unpaid
            var now = DateTime.Now;
            return now > invoice.DueDate ? InvoiceStatus.Overdue : InvoiceStatus.Unpaid;
        }
    }
}
