using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Assets;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DormitoryManagement.Controllers
{
    /// <summary>
    /// Controller quản lý tài sản/trang thiết bị ký túc xá.
    /// - Admin/Manager: toàn quyền CRUD + thùng rác.
    /// - TechnicalStaff: xem chi tiết.
    /// - Tất cả (kể cả chưa đăng nhập): xem danh mục công khai kèm giá đền bù.
    /// </summary>
    public class AssetController
    (
        IAssetService assetService,
        IRoomService roomService,
        IMapper mapper
    ) : BaseController
    {
        private readonly IAssetService _assetService = assetService;
        private readonly IRoomService _roomService = roomService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Danh sách tài sản phân trang với tìm kiếm và lọc trạng thái.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> Index(int page = 1, string search = "", string statusFilter = "")
        {
            Logger.LogInformation("Đang tải danh sách tài sản trang {Page}, tìm kiếm: '{Search}', trạng thái: '{StatusFilter}'", page, search, statusFilter);
            int pageSize = PageSize;
            AssetStatus? status = Enum.TryParse<AssetStatus>(statusFilter, out var s) ? s : null;
            var result = await _assetService.GetPagedAssetsAsync(page, pageSize, search, status);
            ViewBag.Search = search;
            ViewBag.StatusFilter = statusFilter;
            return View(result);
        }

        /// <summary>
        /// Thùng rác — danh sách tài sản đã bị xóa mềm.
        /// </summary>
        [HttpGet("RecycleBin")]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang truy cập thùng rác tài sản trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var result = await _assetService.GetDeletedAssetsPagedAsync(page, pageSize, search);
            ViewBag.Search = search;
            return View(result);
        }

        /// <summary>
        /// Xem chi tiết tài sản — Admin/Manager/TechnicalStaff.
        /// </summary>
        [HttpGet("Details/{id}")]
        [Authorize(Roles = "Admin,ManagementStaff,TechnicalStaff")]
        public async Task<IActionResult> Details(Guid id)
        {
            Logger.LogInformation("Đang xem chi tiết tài sản ID: {Id}", id);
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null)
            {
                Logger.LogWarning("Không tìm thấy tài sản với ID: {Id}", id);
                return NotFound();
            }
            return View(asset);
        }

        /// <summary>
        /// Hiển thị form thêm tài sản mới.
        /// </summary>
        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> Create()
        {
            Logger.LogInformation("Đang truy cập trang thêm mới tài sản.");
            await LoadRoomsDropdownAsync();
            return View(new CreateAssetRequest());
        }

        /// <summary>
        /// Xử lý thêm tài sản mới.
        /// </summary>
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> Create(CreateAssetRequest request)
        {
            Logger.LogInformation("Đang thực hiện thêm mới tài sản: '{AssetName}' cho phòng ID: {RoomId}", request.AssetName, request.RoomId);
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Logger.LogWarning("Dữ liệu thêm mới tài sản không hợp lệ: {Errors}", string.Join(", ", errors));
                await LoadRoomsDropdownAsync(request.RoomId);
                return View(request);
            }

            try
            {
                await _assetService.CreateAssetAsync(request);
                Logger.LogInformation("Thêm tài sản '{AssetName}' thành công.", request.AssetName);
                TempData["Success"] = $"Thêm tài sản '{request.AssetName}' thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thêm tài sản '{AssetName}'.", request.AssetName);
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadRoomsDropdownAsync(request.RoomId);
                return View(request);
            }
        }

        /// <summary>
        /// Hiển thị form chỉnh sửa thông tin tài sản.
        /// </summary>
        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang truy cập trang chỉnh sửa tài sản ID: {Id}", id);
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null)
            {
                Logger.LogWarning("Không tìm thấy tài sản ID: {Id} để chỉnh sửa.", id);
                return NotFound();
            }

            var updateDto = _mapper.Map<UpdateAssetRequest>(asset);
            await LoadRoomsDropdownAsync(updateDto.RoomId);
            return View(updateDto);
        }

        /// <summary>
        /// Xử lý cập nhật thông tin tài sản.
        /// </summary>
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> Edit(Guid id, UpdateAssetRequest request)
        {
            Logger.LogInformation("Đang xử lý yêu cầu cập nhật tài sản ID: {Id}", id);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu cập nhật tài sản ID: {Id} không hợp lệ.", id);
                await LoadRoomsDropdownAsync(request.RoomId);
                return View(request);
            }

            try
            {
                request.Id = id;
                var result = await _assetService.UpdateAssetAsync(id, request);
                if (result)
                {
                    Logger.LogInformation("Cập nhật tài sản ID: {Id} thành công.", id);
                    TempData["Success"] = "Cập nhật tài sản thành công!";
                    return RedirectToAction(nameof(Index));
                }
                Logger.LogWarning("Không tìm thấy tài sản ID: {Id} để cập nhật.", id);
                ModelState.AddModelError("", "Không tìm thấy tài sản để cập nhật.");
                await LoadRoomsDropdownAsync(request.RoomId);
                return View(request);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi cập nhật tài sản ID: {Id}.", id);
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadRoomsDropdownAsync(request.RoomId);
                return View(request);
            }
        }

        // =============================================
        // AJAX ACTIONS — Admin / Manager
        // =============================================

        /// <summary>
        /// Xóa mềm tài sản (chuyển vào thùng rác) — AJAX POST.
        /// </summary>
        [HttpPost("SoftDelete/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa mềm tài sản ID: {Id}", id);
            if (id == Guid.Empty)
            {
                Logger.LogWarning("Yêu cầu xóa mềm thất bại do ID trống.");
                return Json(new { success = false, message = "ID không hợp lệ" });
            }
            try
            {
                var result = await _assetService.SoftDeleteAssetAsync(id);
                if (result)
                {
                    Logger.LogInformation("Đã xóa mềm tài sản ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Đã chuyển tài sản vào thùng rác." });
                }
                Logger.LogWarning("Không tìm thấy tài sản ID: {Id} để xóa mềm.", id);
                return Json(new { success = false, message = "Không tìm thấy tài sản." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa mềm tài sản ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Khôi phục tài sản từ thùng rác — AJAX POST.
        /// </summary>
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục tài sản ID: {Id}", id);
            try
            {
                var result = await _assetService.RestoreAssetAsync(id);
                if (result)
                {
                    Logger.LogInformation("Đã khôi phục tài sản ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Khôi phục tài sản thành công." });
                }
                Logger.LogWarning("Không tìm thấy tài sản ID: {Id} trong thùng rác.", id);
                return Json(new { success = false, message = "Không tìm thấy tài sản trong thùng rác." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi khôi phục tài sản ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa vĩnh viễn tài sản — AJAX POST, chỉ Admin.
        /// </summary>
        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn tài sản ID: {Id}", id);
            try
            {
                var result = await _assetService.DeletePermanentlyAsync(id);
                if (result)
                {
                    Logger.LogInformation("Đã xóa vĩnh viễn tài sản ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Đã xóa vĩnh viễn tài sản." });
                }
                Logger.LogWarning("Không tìm thấy tài sản ID: {Id} để xóa vĩnh viễn.", id);
                return Json(new { success = false, message = "Không tìm thấy tài sản." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa vĩnh viễn tài sản ID: {Id}.", id);
                var msg = ex.InnerException?.Message ?? ex.Message;
                return Json(new { success = false, message = "Lỗi hệ thống: " + msg });
            }
        }

        /// <summary>
        /// Bật/Tắt trạng thái hoạt động tài sản — AJAX POST.
        /// </summary>
        [HttpPost("ToggleStatus/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu thay đổi trạng thái hoạt động tài sản ID: {Id}", id);
            try
            {
                var result = await _assetService.ToggleAssetStatusAsync(id);
                if (result)
                {
                    Logger.LogInformation("Thay đổi trạng thái tài sản ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Đã thay đổi trạng thái tài sản." });
                }
                Logger.LogWarning("Không tìm thấy tài sản ID: {Id} để thay đổi trạng thái.", id);
                return Json(new { success = false, message = "Không tìm thấy tài sản." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thay đổi trạng thái tài sản ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        // =============================================
        // CÔNG KHAI — Tất cả người dùng (kể cả sinh viên)
        // =============================================

        /// <summary>
        /// Danh mục tài sản công khai — mọi người đều xem được tên, phòng, trạng thái và giá đền bù.
        /// </summary>
        [HttpGet("Catalog")]
        [AllowAnonymous]
        public async Task<IActionResult> Catalog(int page = 1, string search = "", string statusFilter = "")
        {
            Logger.LogInformation("Đang truy cập danh mục tài sản công khai trang {Page}, tìm kiếm: '{Search}', trạng thái: '{StatusFilter}'", page, search, statusFilter);
            int pageSize = 8;
            AssetStatus? status = Enum.TryParse<AssetStatus>(statusFilter, out var s) ? s : null;
            var result = await _assetService.GetPagedAssetsAsync(page, pageSize, search, status);
            ViewBag.Search = search;
            ViewBag.StatusFilter = statusFilter;
            return View(result);
        }

        // =============================================
        // HELPER METHODS
        // =============================================

        private async Task LoadRoomsDropdownAsync(Guid? selectedRoomId = null)
        {
            var allRooms = await _roomService.GetPagedRoomsAsync(
                new RoomFilterRequest { PageNumber = 1, PageSize = 999 });

            ViewBag.Rooms = new SelectList(
                allRooms.Items.Select(r => new { r.Id, Display = $"{r.RoomNumber} - {r.BlockName}" }),
                "Id", "Display", selectedRoomId);
        }
    }
}