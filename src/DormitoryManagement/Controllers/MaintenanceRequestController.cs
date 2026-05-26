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
        IContractRepository contractRepository) : BaseController
    {
        private readonly IMaintenanceRequestService _service = service;
        private readonly IContractRepository _contractRepository = contractRepository;

        /// <summary>
        /// Admin/Technical Staff view all requests
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, ManagementStaff, TechnicalStaff")]
        public async Task<IActionResult> Index(MaintenanceRequestFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = PageSize;

            Logger.LogInformation("Đang tải danh sách yêu cầu bảo trì trang {Page}, tìm kiếm: '{Search}', trạng thái: '{Status}', độ ưu tiên: '{Priority}'", filter.PageNumber, filter.SearchTerm, filter.Status, filter.Priority);
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
            {
                Logger.LogWarning("Truy cập MyRequests bị từ chối: Sinh viên chưa đăng nhập hoặc Token không hợp lệ.");
                return Unauthorized();
            }

            Logger.LogInformation("Sinh viên ID {UserId} đang tải danh sách yêu cầu sửa chữa cá nhân.", userId);
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
            {
                Logger.LogWarning("Truy cập trang tạo yêu cầu sửa chữa bị từ chối: Chưa đăng nhập.");
                return Unauthorized();
            }

            Logger.LogInformation("Sinh viên ID {UserId} đang tải form tạo yêu cầu sửa chữa.", userId);
            // Find student's current room
            var contracts = await _contractRepository.GetQuery()
                .Include(c => c.Bed)
                .ThenInclude(b => b.Room)
                .Where(c => c.UserId == userId && c.Status == ContractStatus.Active)
                .ToListAsync();

            var activeContract = contracts.FirstOrDefault();
            if (activeContract == null || activeContract.Bed?.Room == null)
            {
                Logger.LogWarning("Sinh viên ID {UserId} yêu cầu báo sửa chữa nhưng không tìm thấy phòng hoạt động.", userId);
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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                Logger.LogWarning("Gửi yêu cầu sửa chữa thất bại: Người dùng chưa đăng nhập.");
                return Unauthorized();
            }

            Logger.LogInformation("Đang xử lý gửi yêu cầu sửa chữa từ sinh viên ID: {UserId} cho phòng ID: {RoomId}", userId, dto.RoomId);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu yêu cầu sửa chữa không hợp lệ.");
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

            try
            {
                var result = await _service.CreateAsync(dto, userId);
                Logger.LogInformation("Sinh viên ID {UserId} gửi yêu cầu sửa chữa thành công.", userId);
                TempData["Success"] = "Gửi yêu cầu bảo trì thành công!";
                return RedirectToAction(nameof(MyRequests));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi sinh viên ID {UserId} tạo yêu cầu sửa chữa.", userId);
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

            Logger.LogInformation("Đang xử lý cập nhật trạng thái yêu cầu bảo trì ID: {Id} sang {Status} bởi tài khoản: {HandlerId}", id, status, handlerId);
            var dto = new UpdateMaintenanceStatusDto { Status = status, HandlerId = handlerId };
            var success = await _service.UpdateStatusAsync(id, dto, handlerId);

            if (success)
            {
                Logger.LogInformation("Cập nhật trạng thái yêu cầu bảo trì ID: {Id} thành công.", id);
                return Json(new { success = true, message = "Cập nhật trạng thái thành công!" });
            }

            Logger.LogWarning("Không tìm thấy yêu cầu ID {Id} hoặc cập nhật trạng thái thất bại.", id);
            return Json(new { success = false, message = "Không tìm thấy yêu cầu hoặc cập nhật thất bại." });
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang xử lý yêu cầu xóa yêu cầu bảo trì ID: {Id}", id);
            var success = await _service.DeleteAsync(id);
            if (success)
            {
                Logger.LogInformation("Xóa yêu cầu bảo trì ID: {Id} thành công.", id);
                return Json(new { success = true, message = "Đã xóa yêu cầu bảo trì." });
            }
            Logger.LogWarning("Xóa yêu cầu bảo trì ID: {Id} thất bại.", id);
            return Json(new { success = false, message = "Xóa thất bại." });
        }
    }
}
