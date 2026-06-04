using Microsoft.AspNetCore.Mvc;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Controllers
{
    /// <summary>
    /// Controller xử lý các logic liên quan đến Hóa đơn (Invoice)
    /// </summary>
    public class InvoiceController(IInvoiceService _invoiceService, IContractService _contractService) : BaseController
    {
        /// <summary>
        /// Hiển thị danh sách hóa đơn (Trang Index)
        /// </summary>
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(string search = "", string status = "", int page = 1)
        {
            Logger.LogInformation("Đang tải danh sách hóa đơn trang {Page}, tìm kiếm: '{Search}', trạng thái: '{Status}'", page, search, status);
            int pageSize = PageSize;

            DormitoryManagement.Domain.Enums.InvoiceStatus? invoiceStatus = null;
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<DormitoryManagement.Domain.Enums.InvoiceStatus>(status, true, out var parsedStatus))
            {
                invoiceStatus = parsedStatus;
            }

            var pagedResult = await _invoiceService.GetPagedInvoicesAsync(page, pageSize, search, invoiceStatus);

            // Thống kê nhanh
            var allInvoices = await _invoiceService.GetAllInvoicesAsync();
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
        public async Task<IActionResult> Create()
        {
            Logger.LogInformation("Đang truy cập trang tạo mới hóa đơn.");
            var contracts = await _contractService.GetPagedContractsAsync(1, 9999, status: DormitoryManagement.Domain.Enums.ContractStatus.Active);
            ViewBag.Contracts = contracts.Items;
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

            ModelState.Remove(nameof(invoice.Contract));

            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                Logger.LogWarning("Dữ liệu tạo hóa đơn không hợp lệ. Lỗi: {Errors}", errors);
                var contracts = await _contractService.GetPagedContractsAsync(1, 9999, status: DormitoryManagement.Domain.Enums.ContractStatus.Active);
                ViewBag.Contracts = contracts.Items;
                return View(invoice);
            }

            await _invoiceService.CreateInvoiceAsync(invoice);
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
            var invoice = await _invoiceService.GetByIdAsync(id);
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

            ModelState.Remove(nameof(invoice.Contract));

            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                Logger.LogWarning("Dữ liệu cập nhật hóa đơn ID: {Id} không hợp lệ. Lỗi: {Errors}", id, errors);
                var dbInvoice = await _invoiceService.GetByIdAsync(id);
                if (dbInvoice != null)
                {
                    invoice.Contract = dbInvoice.Contract;
                }
                return View(invoice);
            }

            await _invoiceService.UpdateInvoiceAsync(invoice);
            Logger.LogInformation("Cập nhật hóa đơn ID: {Id} thành công.", id);
            TempData["Success"] = "Cập nhật hóa đơn thành công!";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// AJAX API: Lấy chi tiết thông tin hợp đồng (tên sinh viên, tiền phòng, mã giường phòng)
        /// </summary>
        [HttpGet("GetContractDetails/{id}")]
        public async Task<IActionResult> GetContractDetails(Guid id)
        {
            Logger.LogInformation("AJAX lấy chi tiết hợp đồng ID: {Id}", id);
            var contract = await _contractService.GetByIdAsync(id);
            if (contract == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hợp đồng hoặc sinh viên." });
            }

            return Json(new
            {
                success = true,
                studentName = contract.User?.FullName ?? "N/A",
                roomPrice = contract.Bed?.Room?.RoomType?.BasePrice ?? 0,
                roomNumber = contract.Bed?.Room?.RoomNumber ?? "N/A",
                bedNumber = contract.Bed?.BedNumber ?? "N/A"
            });
        }

        /// <summary>
        /// POST: Xóa hóa đơn (xóa mềm)
        /// </summary>
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa hóa đơn ID: {Id}", id);
            var result = await _invoiceService.DeleteInvoiceAsync(id);
            if (result)
            {
                Logger.LogInformation("Xóa hóa đơn ID: {Id} thành công.", id);
                return Json(new { success = true, message = "Xóa hóa đơn thành công!" });
            }
            else
            {
                Logger.LogWarning("Xóa hóa đơn ID: {Id} thất bại (Không tìm thấy hoặc lỗi xảy ra).", id);
                return Json(new { success = false, message = "Xóa hóa đơn thất bại hoặc không tìm thấy hóa đơn." });
            }
        }

        [HttpGet("RecycleBin")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang truy cập thùng rác hóa đơn trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var result = await _invoiceService.GetDeletedInvoicesAsync(page, pageSize, search);
            ViewBag.Search = search;
            return View(result);
        }

        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục hóa đơn ID: {Id}", id);
            var success = await _invoiceService.RestoreInvoiceAsync(id);
            if (success)
            {
                return Json(new { success = true, message = "Khôi phục hóa đơn thành công!" });
            }
            return Json(new { success = false, message = "Khôi phục hóa đơn thất bại." });
        }

        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn hóa đơn ID: {Id}", id);
            var success = await _invoiceService.DeletePermanentlyAsync(id);
            if (success)
            {
                return Json(new { success = true, message = "Đã xóa vĩnh viễn hóa đơn khỏi cơ sở dữ liệu." });
            }
            return Json(new { success = false, message = "Xóa vĩnh viễn hóa đơn thất bại." });
        }
    }
}
