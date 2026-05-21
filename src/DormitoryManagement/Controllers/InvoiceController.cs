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
            return View();
        }

        /// <summary>
        /// POST: Xử lý lưu hóa đơn mới
        /// </summary>
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            if (!ModelState.IsValid) return View(invoice);

            await invoiceService.CreateInvoiceAsync(invoice);
            TempData["Success"] = "Tạo hóa đơn thành công!";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: Hiển thị form chỉnh sửa hóa đơn
        /// </summary>
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var invoice = await invoiceService.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return View(invoice);
        }

        /// <summary>
        /// POST: Cập nhật thông tin hóa đơn
        /// </summary>
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Invoice invoice)
        {
            if (id != invoice.Id) return BadRequest();
            if (!ModelState.IsValid) return View(invoice);

            await invoiceService.UpdateInvoiceAsync(invoice);
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
            var result = await invoiceService.DeleteInvoiceAsync(id);
            if (result)
            {
                TempData["Success"] = "Xóa hóa đơn thành công!";
            }
            else
            {
                TempData["Error"] = "Xóa hóa đơn thất bại hoặc không tìm thấy hóa đơn.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
