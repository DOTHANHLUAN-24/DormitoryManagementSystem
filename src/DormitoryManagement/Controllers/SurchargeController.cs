using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SurchargeController(
        ISurchargeService service,
        IInvoiceService invoiceService
    ) : BaseController
    {
        private readonly ISurchargeService _service = service;
        private readonly IInvoiceService _invoiceService = invoiceService;

        /// <summary>
        /// GET: Surcharge/Index
        /// Hiển thị danh sách phụ phí từ database.
        /// </summary>
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(string search = "", string? isActive = null, int page = 1)
        {
            Logger.LogInformation("Đang tải danh sách phụ phí trang {Page}, tìm kiếm: '{Search}', trạng thái: '{IsActive}'", page, search, isActive);
            int pageIndex = page > 0 ? page : 1;

            bool? activeFilter = null;
            if (isActive == "true") activeFilter = true;
            else if (isActive == "false") activeFilter = false;

            DormitoryManagement.Domain.Common.PagedResult<Surcharge> result;

            if (User.IsInRole("Student"))
            {
                var userId = CurrentUserId;
                if (userId == null)
                {
                    return Unauthorized();
                }
                result = await _service.GetPagedSurchargesByUserIdAsync(userId.Value, pageIndex, PageSize, search, activeFilter);
            }
            else
            {
                result = await _service.GetPagedSurchargesAsync(pageIndex, PageSize, search, activeFilter, isDeleted: false);
            }

            ViewBag.Search = search;
            ViewBag.IsActive = isActive;

            return View(result);
        }

        /// <summary>
        /// GET: Surcharge/Create
        /// Giao diện thêm mới phụ phí (Chỉ Admin và các cấp Quản lý).
        /// </summary>
        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        public async Task<IActionResult> Create()
        {
            Logger.LogInformation("Đang truy cập trang thêm mới phụ phí.");
            await PopulateInvoicesViewBagAsync();
            return View();
        }

        /// <summary>
        /// POST: Surcharge/Create
        /// Xử lý lưu phụ phí mới vào database.
        /// </summary>
        [HttpPost("Create")]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid invoiceId, string surchargeName, decimal amount, bool isActive = true)
        {
            Logger.LogInformation("Đang xử lý thêm mới phụ phí cho hóa đơn ID {InvoiceId}: '{SurchargeName}'", invoiceId, surchargeName);

            if (invoiceId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng chọn hóa đơn để áp dụng phụ phí.");
            }
            if (string.IsNullOrWhiteSpace(surchargeName))
            {
                ModelState.AddModelError(string.Empty, "Tên khoản phụ thu không được để trống.");
            }
            if (amount <= 0)
            {
                ModelState.AddModelError(string.Empty, "Mức giá thu phải lớn hơn 0.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateInvoicesViewBagAsync();
                return View();
            }

            try
            {
                var success = await _service.CreateSurchargeAsync(invoiceId, surchargeName, amount, isActive);
                if (success)
                {
                    TempData["Success"] = "Thêm phụ phí mới thành công.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Tạo phụ phí thất bại. Hóa đơn không tồn tại hoặc đã bị khóa.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thêm mới phụ phí.");
                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi hệ thống: " + ex.Message);
            }

            await PopulateInvoicesViewBagAsync();
            return View();
        }

        /// <summary>
        /// GET: Surcharge/Edit/{id}
        /// Giao diện chỉnh sửa phụ phí (Chỉ Admin/Manager).
        /// </summary>
        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang truy cập giao diện chỉnh sửa phụ phí ID: {Id}", id);
            var surcharge = await _service.GetPagedSurchargesAsync(1, 1000, null, null, false);

            var item = surcharge.Items.FirstOrDefault(s => s.Id == id);
            if (item == null)
            {
                Logger.LogWarning("Không tìm thấy phụ phí ID: {Id} để sửa.", id);
                return NotFound();
            }

            await PopulateInvoicesViewBagAsync();
            return View(item);
        }

        /// <summary>
        /// POST: Surcharge/Edit/{id}
        /// Xử lý cập nhật thông tin phụ phí.
        /// </summary>
        [HttpPost("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string surchargeName, decimal amount, bool isActive)
        {
            Logger.LogInformation("Đang cập nhật phụ phí ID {Id}", id);

            if (string.IsNullOrWhiteSpace(surchargeName))
            {
                ModelState.AddModelError(string.Empty, "Tên khoản phụ thu không được để trống.");
            }
            if (amount <= 0)
            {
                ModelState.AddModelError(string.Empty, "Mức giá thu phải lớn hơn 0.");
            }

            if (!ModelState.IsValid)
            {
                var surcharge = (await _service.GetPagedSurchargesAsync(1, 1000, null, null, false)).Items.FirstOrDefault(s => s.Id == id);
                await PopulateInvoicesViewBagAsync();
                return View(surcharge);
            }

            try
            {
                var success = await _service.UpdateSurchargeAsync(id, surchargeName, amount, isActive);
                if (success)
                {
                    TempData["Success"] = "Cập nhật thông tin phụ phí thành công.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Không tìm thấy phụ phí hoặc cập nhật thất bại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi khi cập nhật phụ phí ID: {Id}", id);
                ModelState.AddModelError(string.Empty, "Lỗi hệ thống: " + ex.Message);
            }

            var item = (await _service.GetPagedSurchargesAsync(1, 1000, null, null, false)).Items.FirstOrDefault(s => s.Id == id);
            await PopulateInvoicesViewBagAsync();
            return View(item);
        }

        /// <summary>
        /// POST: Surcharge/Delete/{id}
        /// Xóa mềm phụ phí (Chỉ Admin/Manager).
        /// </summary>
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang xóa mềm phụ phí ID {Id}", id);
            var success = await _service.SoftDeleteSurchargeAsync(id);
            return Json(new { success = success, message = success ? "Đã đưa phụ phí vào thùng rác." : "Xóa phụ phí thất bại." });
        }

        /// <summary>
        /// GET: Surcharge/RecycleBin
        /// Hiển thị danh sách phụ phí đã bị xóa mềm.
        /// </summary>
        [HttpGet("RecycleBin")]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        public async Task<IActionResult> RecycleBin(string search = "", int page = 1)
        {
            Logger.LogInformation("Truy cập thùng rác phụ phí trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageIndex = page > 0 ? page : 1;
            var result = await _service.GetPagedSurchargesAsync(pageIndex, PageSize, search, null, isDeleted: true);
            ViewBag.Search = search;
            return View(result);
        }

        /// <summary>
        /// POST: Surcharge/Restore/{id}
        /// Khôi phục phụ phí đã xóa mềm.
        /// </summary>
        [HttpPost("Restore/{id}")]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang khôi phục phụ phí ID {Id}", id);
            var success = await _service.RestoreSurchargeAsync(id);
            return Json(new { success = success, message = success ? "Khôi phục phụ phí thành công." : "Khôi phục thất bại." });
        }

        private async Task PopulateInvoicesViewBagAsync()
        {
            // Lấy danh sách hóa đơn chưa thanh toán hoặc quá hạn
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            var unpaidInvoices = invoices
                .Where(i => i.Status != InvoiceStatus.Paid && !i.IsDeleted && i.IsActive)
                .OrderByDescending(i => i.CreatedDate)
                .ToList();
            ViewBag.Invoices = unpaidInvoices;
        }
    }
}