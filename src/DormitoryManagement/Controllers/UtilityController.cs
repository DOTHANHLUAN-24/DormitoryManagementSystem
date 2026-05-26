using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Utilities;
using DormitoryManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DormitoryManagement.Controllers
{
    /// <summary>
    /// Controller quản lý dịch vụ / tiện ích sử dụng IUtilityService.
    /// </summary>
    [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
    public class UtilityController
    (
        IUtilityService utilityService,
        IMapper mapper
    ) : BaseController
    {
        private readonly IUtilityService _utilityService = utilityService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("")]
        public async Task<IActionResult> Index(string search = "", int page = 1)
        {
            Logger.LogInformation("Đang tải danh sách dịch vụ hoạt động trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var pagedUtilities = await _utilityService.GetPagedUtilitiesAsync(page, pageSize, search, isActive: true, isDeleted: false);

            var utilities = await _utilityService.GetAllActiveUtilitiesAsync();
            var deleted = await _utilityService.GetAllDeletedUtilitiesAsync();
            ViewBag.ActiveCount = utilities.Count();
            ViewBag.DeletedCount = deleted.Count();
            ViewBag.TotalCount = utilities.Count() + deleted.Count();

            ViewBag.Search = search;

            return View(pagedUtilities);
        }

        [HttpGet("Trash")]
        public async Task<IActionResult> Trash(string search = "", int page = 1)
        {
            Logger.LogInformation("Đang tải danh sách dịch vụ bị ngưng hoạt động trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var pagedTrashed = await _utilityService.GetPagedUtilitiesAsync(page, pageSize, search, isActive: false, isDeleted: false);

            var active = await _utilityService.GetAllActiveUtilitiesAsync();
            var deleted = await _utilityService.GetAllDeletedUtilitiesAsync();
            ViewBag.ActiveCount = active.Count();
            ViewBag.DeletedCount = deleted.Count();
            ViewBag.TotalCount = active.Count() + deleted.Count();

            ViewBag.Search = search;

            return View(pagedTrashed);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang tạo mới dịch vụ.");
            return View(new UtilityRequestDto());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UtilityRequestDto request)
        {
            Logger.LogInformation("Đang thực hiện tạo mới dịch vụ: '{UtilityName}' (Giá: {Price})", request.UtilityName, request.UnitPrice);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu tạo dịch vụ không hợp lệ.");
                return View(request);
            }

            try
            {
                var result = await _utilityService.CreateUtilityAsync(request);
                if (result)
                {
                    Logger.LogInformation("Tạo dịch vụ '{UtilityName}' thành công.", request.UtilityName);
                    TempData["Success"] = "Thêm dịch vụ tiện ích mới thành công.";
                    return RedirectToAction(nameof(Index));
                }

                Logger.LogWarning("Tạo dịch vụ '{UtilityName}' thất bại tại Service.", request.UtilityName);
                ModelState.AddModelError(string.Empty, "Không thể tạo dịch vụ. Vui lòng thử lại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi tạo dịch vụ '{UtilityName}'.", request.UtilityName);
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(request);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang tải trang chỉnh sửa dịch vụ ID: {Id}", id);
            var utility = await _utilityService.GetUtilityByIdAsync(id);
            if (utility == null)
            {
                Logger.LogWarning("Không tìm thấy dịch vụ ID: {Id} để chỉnh sửa.", id);
                return NotFound();
            }

            var requestDto = _mapper.Map<UtilityRequestDto>(utility);
            return View(requestDto);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UtilityRequestDto request)
        {
            Logger.LogInformation("Đang xử lý cập nhật thông tin dịch vụ ID: {Id}", id);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu cập nhật dịch vụ ID: {Id} không hợp lệ.", id);
                return View(request);
            }

            try
            {
                var result = await _utilityService.UpdateUtilityAsync(id, request);
                if (result)
                {
                    Logger.LogInformation("Cập nhật thông tin dịch vụ ID: {Id} thành công.", id);
                    TempData["Success"] = "Cập nhật thông tin dịch vụ thành công.";
                    return RedirectToAction(nameof(Index));
                }

                Logger.LogWarning("Cập nhật dịch vụ ID: {Id} thất bại tại Service (Không tìm thấy).", id);
                ModelState.AddModelError(string.Empty, "Không tìm thấy dịch vụ hoặc cập nhật thất bại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi cập nhật dịch vụ ID: {Id}.", id);
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(request);
        }

        // Xóa mềm: Đưa dịch vụ vào thùng rác (đặt IsActive = false)
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa mềm (ngừng hoạt động) dịch vụ ID: {Id}", id);
            var result = await _utilityService.SoftDeleteUtilityAsync(id);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Logger.LogInformation("Đã trả về kết quả xóa mềm AJAX cho dịch vụ ID {Id}: {Result}", id, result);
                return Json(new { success = result, message = result ? "Đã đưa dịch vụ vào thùng rác." : "Xóa thất bại. Không tìm thấy dịch vụ." });
            }
            if (result)
            {
                Logger.LogInformation("Xóa mềm dịch vụ ID: {Id} thành công.", id);
                TempData["Success"] = "Đã đưa dịch vụ vào thùng rác.";
            }
            else
            {
                Logger.LogWarning("Xóa mềm dịch vụ ID: {Id} thất bại (Không tìm thấy).", id);
                TempData["Error"] = "Xóa thất bại. Không tìm thấy dịch vụ.";
            }
            return RedirectToAction(nameof(Index));
        }

        // Khôi phục dịch vụ từ thùng rác
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục hoạt động dịch vụ ID: {Id}", id);
            var result = await _utilityService.RestoreUtilityAsync(id);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Logger.LogInformation("Đã trả về kết quả khôi phục AJAX cho dịch vụ ID {Id}: {Result}", id, result);
                return Json(new { success = result, message = result ? "Khôi phục dịch vụ thành công." : "Khôi phục thất bại. Không tìm thấy dịch vụ." });
            }
            if (result)
            {
                Logger.LogInformation("Khôi phục dịch vụ ID: {Id} thành công.", id);
                TempData["Success"] = "Khôi phục dịch vụ thành công.";
            }
            else
            {
                Logger.LogWarning("Khôi phục dịch vụ ID: {Id} thất bại (Không tìm thấy).", id);
                TempData["Error"] = "Khôi phục thất bại. Không tìm thấy dịch vụ.";
            }
            return RedirectToAction(nameof(Trash));
        }

        // Xóa cứng: Xóa hoàn toàn khỏi database
        [HttpPost("HardDelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn dịch vụ ID: {Id}", id);
            var result = await _utilityService.HardDeleteUtilityAsync(id);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Logger.LogInformation("Đã trả về kết quả xóa vĩnh viễn AJAX cho dịch vụ ID {Id}: {Result}", id, result);
                return Json(new { success = result, message = result ? "Đã xóa vĩnh viễn dịch vụ khỏi hệ thống." : "Xóa vĩnh viễn thất bại. Không tìm thấy dịch vụ." });
            }
            if (result)
            {
                Logger.LogInformation("Xóa vĩnh viễn dịch vụ ID: {Id} thành công.", id);
                TempData["Success"] = "Đã xóa vĩnh viễn dịch vụ.";
            }
            else
            {
                Logger.LogWarning("Xóa vĩnh viễn dịch vụ ID: {Id} thất bại.", id);
                TempData["Error"] = "Xóa vĩnh viễn thất bại. Không tìm thấy dịch vụ.";
            }
            return RedirectToAction(nameof(Trash));
        }
    }
}
