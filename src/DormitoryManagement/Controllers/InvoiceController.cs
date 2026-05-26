using Microsoft.AspNetCore.Mvc;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using System.Threading.Tasks;

namespace DormitoryManagement.Controllers
{
    /// <summary>
    /// Controller xử lý các logic liên quan đến Hóa đơn (Invoice)
    /// </summary>
    public class InvoiceController(IInvoiceService invoiceService) : BaseController
    {
        /// <summary>
        /// Hiển thị danh sách hóa đơn (Trang Index)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(string search = "", string status = "", int page = 1)
        {
            Logger.LogInformation("Đang tải danh sách hóa đơn trang {Page}, tìm kiếm: '{Search}', trạng thái: '{Status}'", page, search, status);
            int pageSize = PageSize;

            DormitoryManagement.Domain.Enums.InvoiceStatus? invoiceStatus = null;
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<DormitoryManagement.Domain.Enums.InvoiceStatus>(status, true, out var parsedStatus))
            {
                invoiceStatus = parsedStatus;
            }

            var pagedResult = await invoiceService.GetPagedInvoicesAsync(page, pageSize, search, invoiceStatus);

            // Thống kê nhanh
            var allInvoices = await invoiceService.GetAllInvoicesAsync();
            ViewBag.TotalCount = allInvoices.Count();
            ViewBag.UnpaidCount = allInvoices.Count(x => x.Status != DormitoryManagement.Domain.Enums.InvoiceStatus.Paid);
            ViewBag.PaidCount = allInvoices.Count(x => x.Status == DormitoryManagement.Domain.Enums.InvoiceStatus.Paid);

            ViewBag.Search = search;
            ViewBag.Status = status;

            return View(pagedResult);
        }

        /// <summary>
        /// GET: Hiển thị form tạo hóa đơn mới
        /// </summary>
        [HttpGet("Create")]
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang tạo mới hóa đơn.");
            return View();
        }

        /// <summary>
        /// POST: Xử lý lưu hóa đơn mới
        /// </summary>
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            Logger.LogInformation("Đang xử lý tạo hóa đơn mới cho hợp đồng ID: {ContractId}", invoice.ContractId);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu tạo hóa đơn không hợp lệ.");
                return View(invoice);
            }

            await invoiceService.CreateInvoiceAsync(invoice);
            Logger.LogInformation("Tạo hóa đơn cho hợp đồng ID: {ContractId} thành công.", invoice.ContractId);
            TempData["Success"] = "Tạo hóa đơn thành công!";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: Hiển thị form chỉnh sửa hóa đơn
        /// </summary>
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang truy cập trang chỉnh sửa hóa đơn ID: {Id}", id);
            var invoice = await invoiceService.GetByIdAsync(id);
            if (invoice == null)
            {
                Logger.LogWarning("Không tìm thấy hóa đơn ID {Id} để chỉnh sửa.", id);
                return NotFound();
            }
            return View(invoice);
        }

        /// <summary>
        /// POST: Cập nhật thông tin hóa đơn
        /// </summary>
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Invoice invoice)
        {
            Logger.LogInformation("Đang xử lý cập nhật hóa đơn ID: {Id}", id);
            if (id != invoice.Id)
            {
                Logger.LogWarning("Yêu cầu cập nhật hóa đơn không khớp ID: {Id} vs {InvoiceId}", id, invoice.Id);
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu cập nhật hóa đơn ID: {Id} không hợp lệ.", id);
                return View(invoice);
            }

            await invoiceService.UpdateInvoiceAsync(invoice);
            Logger.LogInformation("Cập nhật hóa đơn ID: {Id} thành công.", id);
            TempData["Success"] = "Cập nhật hóa đơn thành công!";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: Xóa hóa đơn (xóa mềm)
        /// </summary>
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa hóa đơn ID: {Id}", id);
            var result = await invoiceService.DeleteInvoiceAsync(id);
            if (result)
            {
                Logger.LogInformation("Xóa hóa đơn ID: {Id} thành công.", id);
                TempData["Success"] = "Xóa hóa đơn thành công!";
            }
            else
            {
                Logger.LogWarning("Xóa hóa đơn ID: {Id} thất bại (Không tìm thấy hoặc lỗi xảy ra).", id);
                TempData["Error"] = "Xóa hóa đơn thất bại hoặc không tìm thấy hóa đơn.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
