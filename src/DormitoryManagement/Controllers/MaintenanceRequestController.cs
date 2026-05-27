using DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // =====================================================================
        // MVC VIEWS — ADMIN / MANAGER / TECHNICAL STAFF
        // =====================================================================

        /// <summary>
        /// Danh sách tất cả yêu cầu bảo trì có phân trang và bộ lọc (Admin/Manager/TechnicalStaff).
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, ManagementStaff, TechnicalStaff")]
        public async Task<IActionResult> Index(MaintenanceRequestFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = PageSize;

            Logger.LogInformation(
                "Tải danh sách yêu cầu bảo trì – trang {Page}, tìm kiếm: '{Search}', trạng thái: '{Status}', độ ưu tiên: '{Priority}'",
                filter.PageNumber, filter.SearchTerm, filter.Status, filter.Priority);

            var result = await _service.GetAllPagedAsync(
                filter.PageNumber, filter.PageSize,
                filter.SearchTerm, filter.Status, filter.Priority);

            ViewBag.Filter = filter;
            return View(result);
        }

        // =====================================================================
        // MVC VIEWS — STUDENT
        // =====================================================================

        /// <summary>
        /// Danh sách yêu cầu bảo trì của chính sinh viên đang đăng nhập.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyRequests()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                Logger.LogWarning("MyRequests: không lấy được UserId từ token.");
                return Unauthorized();
            }

            Logger.LogInformation("Sinh viên {UserId} tải danh sách yêu cầu sửa chữa cá nhân.", userId);
            var result = await _service.GetByRequesterIdAsync(userId.Value);
            return View(result);
        }

        /// <summary>
        /// Form tạo yêu cầu bảo trì mới (GET – Student).
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                Logger.LogWarning("Create GET: không lấy được UserId từ token.");
                return Unauthorized();
            }

            var room = await GetActiveRoomForUserAsync(userId.Value);
            if (room == null)
            {
                Logger.LogWarning("Sinh viên {UserId} chưa có phòng hoạt động.", userId);
                TempData["Error"] = "Bạn chưa được xếp phòng hoặc không có hợp đồng hợp lệ để báo sửa chữa.";
                return RedirectToAction(nameof(MyRequests));
            }

            ViewBag.RoomNumber = room.Value.roomNumber;
            return View(new CreateMaintenanceRequestDto { RoomId = room.Value.roomId });
        }

        /// <summary>
        /// Xử lý gửi yêu cầu bảo trì mới (POST – Student).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMaintenanceRequestDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                Logger.LogWarning("Create POST: không lấy được UserId từ token.");
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu yêu cầu sửa chữa không hợp lệ.");
                var room = await GetActiveRoomForUserAsync(userId.Value);
                if (room != null) ViewBag.RoomNumber = room.Value.roomNumber;
                return View(dto);
            }

            try
            {
                await _service.CreateAsync(dto, userId.Value);
                Logger.LogInformation("Sinh viên {UserId} tạo yêu cầu sửa chữa thành công.", userId);
                TempData["Success"] = "Gửi yêu cầu bảo trì thành công! Kỹ thuật viên sẽ liên hệ với bạn sớm.";
                return RedirectToAction(nameof(MyRequests));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi khi sinh viên {UserId} tạo yêu cầu sửa chữa.", userId);
                TempData["Error"] = "Đã xảy ra lỗi hệ thống. Vui lòng thử lại.";
                return View(dto);
            }
        }

        /// <summary>
        /// Student hủy yêu cầu của mình (chỉ được hủy khi trạng thái còn là Open).
        /// </summary>
        [HttpPost]
        [Route("MaintenanceRequest/{id}/Cancel")]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Json(new { success = false, message = "Chưa đăng nhập." });

            // Lấy yêu cầu để kiểm tra quyền sở hữu và trạng thái
            var request = await _service.GetByIdAsync(id);
            if (request == null)
                return Json(new { success = false, message = "Không tìm thấy yêu cầu." });

            if (request.RequesterId != userId.Value)
                return Json(new { success = false, message = "Bạn không có quyền hủy yêu cầu này." });

            if (request.Status != "Open")
                return Json(new { success = false, message = "Chỉ có thể hủy yêu cầu đang ở trạng thái 'Mới tiếp nhận'." });

            var dto = new UpdateMaintenanceStatusDto { Status = MaintenanceStatus.Closed, HandlerId = null };
            var success = await _service.UpdateStatusAsync(id, dto, null);

            Logger.LogInformation("Sinh viên {UserId} hủy yêu cầu bảo trì ID: {Id} – kết quả: {Success}", userId, id, success);
            return Json(new
            {
                success,
                message = success ? "Đã hủy yêu cầu bảo trì." : "Hủy thất bại, vui lòng thử lại."
            });
        }

        // =====================================================================
        // AJAX ENDPOINTS — ADMIN / MANAGER / TECHNICALSTAFF
        // =====================================================================

        /// <summary>
        /// Cập nhật trạng thái yêu cầu bảo trì (AJAX – Admin/Manager/TechnicalStaff).
        /// Route: POST /MaintenanceRequest/{id}/UpdateStatus
        /// </summary>
        [HttpPost]
        [Route("MaintenanceRequest/{id}/UpdateStatus")]
        [Authorize(Roles = "Admin, TechnicalStaff, ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(Guid id, string status)
        {
            var handlerId = GetCurrentUserId();

            if (!Enum.TryParse<MaintenanceStatus>(status, out var parsedStatus))
            {
                Logger.LogWarning("UpdateStatus: giá trị trạng thái '{Status}' không hợp lệ.", status);
                return Json(new { success = false, message = "Trạng thái không hợp lệ." });
            }

            Logger.LogInformation("Cập nhật trạng thái yêu cầu {Id} → {Status} bởi {HandlerId}", id, parsedStatus, handlerId);
            var dto = new UpdateMaintenanceStatusDto { Status = parsedStatus, HandlerId = handlerId };
            var success = await _service.UpdateStatusAsync(id, dto, handlerId);

            return Json(success
                ? new { success = true, message = "Cập nhật trạng thái thành công!" }
                : new { success = false, message = "Không tìm thấy yêu cầu hoặc cập nhật thất bại." });
        }

        /// <summary>
        /// Xóa mềm yêu cầu bảo trì (AJAX – Admin/Manager only).
        /// Route: POST /MaintenanceRequest/{id}/Delete
        /// </summary>
        [HttpPost]
        [Route("MaintenanceRequest/{id}/Delete")]
        [Authorize(Roles = "Admin, ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Xóa yêu cầu bảo trì ID: {Id}", id);
            var success = await _service.DeleteAsync(id);
            return Json(success
                ? new { success = true, message = "Đã xóa yêu cầu bảo trì." }
                : new { success = false, message = "Xóa thất bại." });
        }

        // =====================================================================
        // PRIVATE HELPERS
        // =====================================================================

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null && Guid.TryParse(claim.Value, out var id) ? id : null;
        }

        private async Task<(Guid roomId, string roomNumber)?> GetActiveRoomForUserAsync(Guid userId)
        {
            var contract = await _contractRepository.GetQuery()
                .Include(c => c.Bed).ThenInclude(b => b.Room)
                .Where(c => c.UserId == userId && c.Status == ContractStatus.Active)
                .FirstOrDefaultAsync();

            if (contract?.Bed?.Room == null) return null;
            return (contract.Bed.Room.Id, contract.Bed.Room.RoomNumber);
        }
    }
}
