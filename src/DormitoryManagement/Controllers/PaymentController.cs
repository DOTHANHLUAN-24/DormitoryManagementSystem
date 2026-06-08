using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PaymentController(
        IPaymentService paymentService,
        IInvoiceService invoiceService
    ) : BaseController
    {
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IInvoiceService _invoiceService = invoiceService;

        /// <summary>
        /// GET: Payment/Index
        /// Lịch sử giao dịch thanh toán.
        /// </summary>
        [HttpGet("")]
        [HttpGet("Index")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff,Student")]
        public async Task<IActionResult> Index(string search = "", int page = 1)
        {
            Logger.LogInformation("Truy cập lịch sử thanh toán trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageIndex = page > 0 ? page : 1;

            Guid? studentId = null;
            if (User.IsInRole("Student"))
            {
                studentId = CurrentUserId;
                if (!studentId.HasValue)
                {
                    return Unauthorized();
                }
            }

            var result = await _paymentService.GetPagedPaymentsAsync(pageIndex, PageSize, search, studentId);

            ViewBag.Search = search;
            return View(result);
        }

        /// <summary>
        /// GET: Payment/Create
        /// Giao diện ghi nhận thanh toán mới (Admin/Quản lý).
        /// </summary>
        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Create()
        {
            Logger.LogInformation("Admin truy cập trang thêm mới giao dịch thanh toán.");
            await PopulateInvoicesDropdownAsync();
            return View();
        }

        /// <summary>
        /// POST: Payment/Create
        /// Xử lý ghi nhận thanh toán mới.
        /// </summary>
        [HttpPost("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid invoiceId, decimal amountPaid, string transactionCode, PaymentMethod method, string note = "")
        {
            Logger.LogInformation("Xử lý ghi nhận đóng tiền cho hóa đơn ID {InvoiceId}, Số tiền: {Amount}", invoiceId, amountPaid);

            if (invoiceId == Guid.Empty)
            {
                ModelState.AddModelError(nameof(invoiceId), "Vui lòng chọn hóa đơn cần thanh toán.");
            }
            if (amountPaid <= 0)
            {
                ModelState.AddModelError(nameof(amountPaid), "Số tiền thanh toán phải lớn hơn 0.");
            }
            if (method == PaymentMethod.Cash && string.IsNullOrWhiteSpace(transactionCode))
            {
                transactionCode = $"CASH-{DateTime.Now:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
            }
            else if (string.IsNullOrWhiteSpace(transactionCode))
            {
                ModelState.AddModelError(nameof(transactionCode), "Mã giao dịch/đối soát không được để trống.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateInvoicesDropdownAsync();
                return View();
            }

            try
            {
                // Kiểm tra xem hóa đơn tồn tại và lấy chi tiết số dư
                var invoice = await _invoiceService.GetByIdAsync(invoiceId);
                if (invoice == null)
                {
                    ModelState.AddModelError(string.Empty, "Hóa đơn không tồn tại.");
                    await PopulateInvoicesDropdownAsync();
                    return View();
                }

                // Tính số tiền còn nợ
                var payments = await _paymentService.GetByInvoiceIdAsync(invoiceId);
                var totalPaid = payments.Where(p => p.IsActive && !p.IsDeleted).Sum(p => p.AmountPaid);
                var remainingAmount = invoice.TotalAmount - totalPaid;

                if (amountPaid > remainingAmount)
                {
                    ModelState.AddModelError(nameof(amountPaid), $"Số tiền đóng ({amountPaid:N0} đ) không được lớn hơn số tiền còn lại phải thanh toán ({remainingAmount:N0} đ).");
                    await PopulateInvoicesDropdownAsync();
                    return View();
                }

                var success = await _paymentService.CreatePaymentAsync(
                    invoiceId,
                    amountPaid,
                    DateTime.Now,
                    transactionCode,
                    method,
                    note);

                if (success)
                {
                    TempData["Success"] = "Ghi nhận giao dịch thanh toán thành công.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Lưu giao dịch thanh toán thất bại. Mã giao dịch có thể đã trùng.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi ghi nhận thanh toán.");
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            await PopulateInvoicesDropdownAsync();
            return View();
        }

        /// <summary>
        /// POST: Payment/Delete/{id}
        /// Xóa mềm giao dịch thanh toán (Admin/Quản lý).
        /// </summary>
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Xóa mềm giao dịch thanh toán ID: {Id}", id);
            var success = await _paymentService.DeletePaymentAsync(id);
            return Json(new { success = success, message = success ? "Đã xóa giao dịch thanh toán." : "Xóa thất bại." });
        }

        /// <summary>
        /// GET: Payment/GetInvoiceDetails
        /// API lấy chi tiết số dư nợ của hóa đơn (AJAX).
        /// </summary>
        [HttpGet("GetInvoiceDetails")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> GetInvoiceDetails(Guid invoiceId)
        {
            if (invoiceId == Guid.Empty)
            {
                return Json(new { success = false, message = "Hóa đơn không hợp lệ." });
            }

            var invoice = await _invoiceService.GetByIdAsync(invoiceId);
            if (invoice == null)
            {
                return Json(new { success = false, message = "Hóa đơn không tồn tại." });
            }

            var payments = await _paymentService.GetByInvoiceIdAsync(invoiceId);
            var totalPaid = payments.Where(p => p.IsActive && !p.IsDeleted).Sum(p => p.AmountPaid);
            var remainingAmount = invoice.TotalAmount - totalPaid;

            return Json(new
            {
                success = true,
                totalAmount = invoice.TotalAmount,
                paidAmount = totalPaid,
                remainingAmount = Math.Max(0, remainingAmount)
            });
        }

        private async Task PopulateInvoicesDropdownAsync()
        {
            // Lấy toàn bộ danh sách hóa đơn và lọc ra các hóa đơn chưa thanh toán hoặc thanh toán một phần
            var result = await _invoiceService.GetPagedInvoicesAsync(1, 9999, null, null);
            var unpaidInvoices = result.Items
                .Where(i => !i.IsDeleted && i.IsActive && i.Status != InvoiceStatus.Paid)
                .OrderByDescending(i => i.CreatedDate)
                .ToList();

            ViewBag.Invoices = unpaidInvoices;
        }
    }
}
