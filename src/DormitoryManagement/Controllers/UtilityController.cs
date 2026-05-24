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
            return View(new UtilityRequestDto());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UtilityRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                var result = await _utilityService.CreateUtilityAsync(request);
                if (result)
                {
                    TempData["Success"] = "Thêm dịch vụ tiện ích mới thành công.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Không thể tạo dịch vụ. Vui lòng thử lại.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(request);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var utility = await _utilityService.GetUtilityByIdAsync(id);
            if (utility == null)
            {
                return NotFound();
            }

            var requestDto = _mapper.Map<UtilityRequestDto>(utility);
            return View(requestDto);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UtilityRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                var result = await _utilityService.UpdateUtilityAsync(id, request);
                if (result)
                {
                    TempData["Success"] = "Cập nhật thông tin dịch vụ thành công.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Không tìm thấy dịch vụ hoặc cập nhật thất bại.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(request);
        }

        // Xóa mềm: Đưa dịch vụ vào thùng rác (đặt IsActive = false)
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _utilityService.SoftDeleteUtilityAsync(id);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = result, message = result ? "Đã đưa dịch vụ vào thùng rác." : "Xóa thất bại. Không tìm thấy dịch vụ." });
            }
            if (result)
            {
                TempData["Success"] = "Đã đưa dịch vụ vào thùng rác.";
            }
            else
            {
                TempData["Error"] = "Xóa thất bại. Không tìm thấy dịch vụ.";
            }
            return RedirectToAction(nameof(Index));
        }

        // Khôi phục dịch vụ từ thùng rác
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            var result = await _utilityService.RestoreUtilityAsync(id);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = result, message = result ? "Khôi phục dịch vụ thành công." : "Khôi phục thất bại. Không tìm thấy dịch vụ." });
            }
            if (result)
            {
                TempData["Success"] = "Khôi phục dịch vụ thành công.";
            }
            else
            {
                TempData["Error"] = "Khôi phục thất bại. Không tìm thấy dịch vụ.";
            }
            return RedirectToAction(nameof(Trash));
        }

        // Xóa cứng: Xóa hoàn toàn khỏi database
        [HttpPost("HardDelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            var result = await _utilityService.HardDeleteUtilityAsync(id);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = result, message = result ? "Đã xóa vĩnh viễn dịch vụ khỏi hệ thống." : "Xóa vĩnh viễn thất bại. Không tìm thấy dịch vụ." });
            }
            if (result)
            {
                TempData["Success"] = "Đã xóa vĩnh viễn dịch vụ.";
            }
            else
            {
                TempData["Error"] = "Xóa vĩnh viễn thất bại. Không tìm thấy dịch vụ.";
            }
            return RedirectToAction(nameof(Trash));
        }
    }
}
