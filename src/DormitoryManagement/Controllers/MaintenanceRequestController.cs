using DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    public class MaintenanceRequestController(
        IMaintenanceRequestService service,
        IContractRepository contractRepository,
        ILogger<MaintenanceRequestController> logger) : BaseController
    {
        private readonly IMaintenanceRequestService _service = service;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly ILogger<MaintenanceRequestController> _logger = logger;

        /// <summary>
        /// Admin/Technical Staff view all requests
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, ManagementStaff, TechnicalStaff")]
        public async Task<IActionResult> Index(MaintenanceRequestFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = PageSize;

            var result = await _service.GetAllPagedAsync(filter.PageNumber, filter.PageSize, filter.SearchTerm, filter.Status, filter.Priority);

            ViewBag.Filter = filter;
            return View(result);
        }

        /// <summary>
        /// Student view their own requests
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyRequests()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized();

            var result = await _service.GetByRequesterIdAsync(userId);
            return View(result);
        }

        /// <summary>
        /// Form for Student to create a request
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized();

            // Find student's current room
            var contracts = await _contractRepository.GetQuery()
                .Include(c => c.Bed)
                .ThenInclude(b => b.Room)
                .Where(c => c.UserId == userId && c.Status == ContractStatus.Active)
                .ToListAsync();

            var activeContract = contracts.FirstOrDefault();
            if (activeContract == null || activeContract.Bed?.Room == null)
            {
                TempData["Error"] = "Bạn chưa được xếp phòng hoặc không có hợp đồng hợp lệ để báo sửa chữa.";
                return RedirectToAction(nameof(MyRequests));
            }

            var model = new CreateMaintenanceRequestDto
            {
                RoomId = activeContract.Bed.Room.Id
            };
            
            ViewBag.RoomNumber = activeContract.Bed.Room.RoomNumber;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMaintenanceRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                // Re-fetch room name just in case
                var userIdClaim2 = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim2 != null && Guid.TryParse(userIdClaim2.Value, out Guid userId2))
                {
                    var contracts = await _contractRepository.GetQuery()
                        .Include(c => c.Bed).ThenInclude(b => b.Room)
                        .Where(c => c.UserId == userId2 && c.Status == ContractStatus.Active)
                        .ToListAsync();
                    var activeContract = contracts.FirstOrDefault();
                    if (activeContract != null && activeContract.Bed?.Room != null)
                        ViewBag.RoomNumber = activeContract.Bed.Room.RoomNumber;
                }
                return View(dto);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized();

            try
            {
                var result = await _service.CreateAsync(dto, userId);
                TempData["Success"] = "Gửi yêu cầu bảo trì thành công!";
                return RedirectToAction(nameof(MyRequests));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo yêu cầu bảo trì");
                TempData["Error"] = "Đã xảy ra lỗi hệ thống.";
                return View(dto);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, TechnicalStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(Guid id, MaintenanceStatus status)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            Guid? handlerId = null;
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid parsedId))
            {
                handlerId = parsedId;
            }

            var dto = new UpdateMaintenanceStatusDto { Status = status, HandlerId = handlerId };
            var success = await _service.UpdateStatusAsync(id, dto, handlerId);

            if (success)
            {
                return Json(new { success = true, message = "Cập nhật trạng thái thành công!" });
            }

            return Json(new { success = false, message = "Không tìm thấy yêu cầu hoặc cập nhật thất bại." });
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (success)
            {
                return Json(new { success = true, message = "Đã xóa yêu cầu bảo trì." });
            }
            return Json(new { success = false, message = "Xóa thất bại." });
        }
    }
}
