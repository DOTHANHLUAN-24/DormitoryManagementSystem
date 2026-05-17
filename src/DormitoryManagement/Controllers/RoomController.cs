using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DormitoryManagement.Controllers
{
    [Route("Room")]
    [Authorize(Roles = "Admin,ManagerStaff")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IBlockService _blockService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IMapper _mapper;

        public RoomController(
            IRoomService roomService,
            IBlockService blockService,
            IRoomTypeService roomTypeService,
            IMapper mapper)
        {
            _roomService = roomService;
            _blockService = blockService;
            _roomTypeService = roomTypeService;
            _mapper = mapper;
        }

        // ==========================================
        // 1. DANH SÁCH (INDEX)
        // ==========================================
        [HttpGet("")]
        public async Task<IActionResult> Index(RoomFilterRequest filter)
        {
            // RoomService mới sử dụng Predicate lọc ngay tại tầng Service
            var pagedRooms = await _roomService.GetPagedRoomsAsync(filter);

            await PopulateDropdownsAsync(filter.BlockId, filter.RoomTypeId);

            ViewBag.Filter = filter; // Để View hiển thị lại các giá trị đã lọc
            return View(pagedRooms);
        }

        // ==========================================
        // 2. CHI TIẾT (DETAILS)
        // ==========================================
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            return View(room);
        }

        // ==========================================
        // 3. THÊM MỚI (CREATE)
        // ==========================================
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            var model = new CreateRoomRequest { Status = RoomStatus.Available, Floor = 1 };
            return View(model);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(request.BlockId, request.RoomTypeId);
                return View(request);
            }

            try
            {
                var result = await _roomService.CreateRoomAsync(request);
                if (result)
                {
                    TempData["Success"] = "Thêm phòng mới thành công!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Không thể tạo phòng.");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            await PopulateDropdownsAsync(request.BlockId, request.RoomTypeId);
            return View(request);
        }

        // ==========================================
        // 4. CẬP NHẬT (EDIT)
        // ==========================================
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            // Ánh xạ từ RoomDetailResponse sang UpdateRoomRequest
            var updateRequest = _mapper.Map<UpdateRoomRequest>(room);

            await PopulateDropdownsAsync(room.BlockId, room.RoomTypeId);
            return View(updateRequest);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(request.BlockId, request.RoomTypeId);
                return View(request);
            }

            try
            {
                var result = await _roomService.UpdateRoomAsync(id, request);
                if (result)
                {
                    TempData["Success"] = "Cập nhật thông tin phòng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Cập nhật thất bại.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            await PopulateDropdownsAsync(request.BlockId, request.RoomTypeId);
            return View(request);
        }

        // ==========================================
        // 5. THÙNG RÁC (RECYCLE BIN)
        // ==========================================
        [HttpGet("Trash")]
        public async Task<IActionResult> Trash(RoomFilterRequest filter)
        {
            var deletedRooms = await _roomService.GetDeletedRoomsPagedAsync(filter);
            ViewBag.Filter = filter;
            return View(deletedRooms);
        }

        // ==========================================
        // 6. THAO TÁC (DELETE, RESTORE, PERMANENT)
        // ==========================================

        // Xóa mềm (Đưa vào thùng rác)
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            if (result) return Json(new { success = true, message = "Đã chuyển phòng vào thùng rác." });
            return Json(new { success = false, message = "Xóa thất bại." });
        }

        // Khôi phục
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            var result = await _roomService.RestoreRoomAsync(id);
            if (result) return Json(new { success = true, message = "Khôi phục phòng thành công." });
            return Json(new { success = false, message = "Không thể khôi phục." });
        }

        // Xóa vĩnh viễn
        [HttpPost("DeletePermanent/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanent(Guid id)
        {
            var result = await _roomService.DeletePermanentlyAsync(id);
            if (result) return Json(new { success = true, message = "Đã xóa vĩnh viễn phòng này." });
            return Json(new { success = false, message = "Không thể xóa vĩnh viễn." });
        }

        // ==========================================
        // HÀM HỖ TRỢ (HELPERS)
        // ==========================================
        private async Task PopulateDropdownsAsync(Guid? selectedBlock = null, Guid? selectedType = null)
        {
            var blocks = await _blockService.GetAllBlocksAsync();
            var roomTypes = await _roomTypeService.GetAllRoomTypesAsync();

            // Lưu ý: Đảm bảo thuộc tính hiển thị (BlockName, TypeName) khớp với DTO của bạn
            ViewBag.Blocks = new SelectList(blocks, "Id", "BlockName", selectedBlock);
            ViewBag.RoomTypes = new SelectList(roomTypes, "Id", "TypeName", selectedType);

            // Tạo danh sách trạng thái từ Enum
            var statusItems = Enum.GetValues(typeof(RoomStatus))
                .Cast<RoomStatus>()
                .Select(s => new
                {
                    Value = s, // Giữ nguyên kiểu Enum để binding vào RoomFilterRequest.Status
                    Text = s switch
                    {
                        RoomStatus.Available => "Còn trống",
                        RoomStatus.Full => "Đã đầy",
                        RoomStatus.Maintenance => "Bảo trì",
                        RoomStatus.Reserved => "Đã đặt trước",
                        _ => s.ToString()
                    }
                })
                .ToList();

            ViewBag.Statuses = new SelectList(statusItems, "Value", "Text");
        }
    }
}