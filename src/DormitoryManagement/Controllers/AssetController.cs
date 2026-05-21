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
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null) return NotFound();
            return View(asset);
        }

        /// <summary>
        /// Hiển thị form thêm tài sản mới.
        /// </summary>
        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagementStaff")]
        public async Task<IActionResult> Create()
        {
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine(">>> LỖI TẠI FORM CREATE ASSET: " + string.Join(", ", errors));
                await LoadRoomsDropdownAsync(request.RoomId);
                return View(request);
            }

            try
            {
                await _assetService.CreateAssetAsync(request);
                TempData["Success"] = $"Thêm tài sản '{request.AssetName}' thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
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
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null) return NotFound();

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
            if (!ModelState.IsValid)
            {
                await LoadRoomsDropdownAsync(request.RoomId);
                return View(request);
            }

            try
            {
                request.Id = id;
                var result = await _assetService.UpdateAssetAsync(id, request);
                if (result)
                {
                    TempData["Success"] = "Cập nhật tài sản thành công!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Không tìm thấy tài sản để cập nhật.");
                await LoadRoomsDropdownAsync(request.RoomId);
                return View(request);
            }
            catch (Exception ex)
            {
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
            if (id == Guid.Empty) return Json(new { success = false, message = "ID không hợp lệ" });
            try
            {
                var result = await _assetService.SoftDeleteAssetAsync(id);
                return result
                    ? Json(new { success = true, message = "Đã chuyển tài sản vào thùng rác." })
                    : Json(new { success = false, message = "Không tìm thấy tài sản." });
            }
            catch (Exception ex)
            {
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
            try
            {
                var result = await _assetService.RestoreAssetAsync(id);
                return result
                    ? Json(new { success = true, message = "Khôi phục tài sản thành công." })
                    : Json(new { success = false, message = "Không tìm thấy tài sản trong thùng rác." });
            }
            catch (Exception ex)
            {
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
            try
            {
                var result = await _assetService.DeletePermanentlyAsync(id);
                return result
                    ? Json(new { success = true, message = "Đã xóa vĩnh viễn tài sản." })
                    : Json(new { success = false, message = "Không tìm thấy tài sản." });
            }
            catch (Exception ex)
            {
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
            try
            {
                var result = await _assetService.ToggleAssetStatusAsync(id);
                return result
                    ? Json(new { success = true, message = "Đã thay đổi trạng thái tài sản." })
                    : Json(new { success = false, message = "Không tìm thấy tài sản." });
            }
            catch (Exception ex)
            {
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
            int pageSize = PageSize;
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