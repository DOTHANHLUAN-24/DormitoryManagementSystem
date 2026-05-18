using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
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

        [HttpGet("")]
        public async Task<IActionResult> Index(RoomFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = 5;

            var pagedRooms = await _roomService.GetPagedRoomsAsync(filter);

            var stats = await _roomService.GetRoomStatisticsAsync();
            ViewBag.TotalRooms = stats.TotalRooms;
            ViewBag.AvailableRooms = stats.AvailableRooms;
            ViewBag.OccupiedRooms = stats.OccupiedRooms;
            ViewBag.MaintenanceRooms = stats.MaintenanceRooms;

            await PopulateDropdownsAsync(filter.BlockId, filter.RoomTypeId);

            ViewBag.Filter = filter;

            return View(pagedRooms);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            await PopulateDropdownsAsync();
            var model = new CreateRoomRequest { Status = RoomStatus.Available, Floor = 1 };
            return View(model);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomRequest request) // 1. Phải là CreateRoomRequest
        {
            if (ModelState.IsValid)
            {
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
            }

            await PopulateDropdownsAsync(request.BlockId, request.RoomTypeId);
            ViewBag.Blocks = new SelectList(await _blockService.GetAllBlocksAsync(), "Id", "BlockName");
            ViewBag.RoomTypes = new SelectList(await _roomTypeService.GetAllRoomTypesAsync(), "Id", "TypeName");
            return View(request);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            // Ánh xạ từ RoomResponse sang UpdateRoomRequest
            var updateRequest = _mapper.Map<UpdateRoomRequest>(room);

            await PopulateDropdownsAsync(room.BlockId, room.RoomTypeId);
            return View(updateRequest);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateRoomRequest request)
        {
            if (ModelState.IsValid)
            {
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
            }

            await PopulateDropdownsAsync(request.BlockId, request.RoomTypeId);
            return View(request);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _roomService.DeleteRoomAsync(id);

                if (result)
                {
                    return Json(new { success = true, message = "Đã chuyển phòng vào thùng rác." });
                }

                return Json(new { success = false, message = "Xóa thất bại. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi cụ thể từ Service (ví dụ: "Không thể xóa phòng đang có sinh viên cư trú")
                return Json(new { success = false, message = ex.Message });
            }
        }
        private async Task PopulateDropdownsAsync(Guid? selectedBlock = null, Guid? selectedType = null)
        {
            var blocks = await _blockService.GetAllBlocksAsync();
            var roomTypes = await _roomTypeService.GetAllRoomTypesAsync();

            // Đưa vào ViewBag dưới dạng SelectList để dễ dàng dùng asp-items trong View
            ViewBag.Blocks = new SelectList(blocks, "Id", "BlockName", selectedBlock);
            ViewBag.RoomTypes = new SelectList(roomTypes, "Id", "TypeName", selectedType);

            // Tạo danh sách trạng thái phòng (Nếu bạn muốn dùng dropdown cho trạng thái)
            var statusItems = Enum.GetValues(typeof(RoomStatus))
                .Cast<RoomStatus>()
                .Select(s => new
                {
                    Value = s,
                    Text = s switch
                    {
                        RoomStatus.Available => "Còn trống",
                        RoomStatus.Full => "Đã đầy",
                        RoomStatus.Maintenance => "Bảo trì",
                        RoomStatus.Reserved => "Đã đặt trước",
                        _ => s.ToString()
                    }
                }).ToList();

            ViewBag.Statuses = new SelectList(statusItems, "Value", "Text");
        }

        [HttpGet("RecycleBin")]
        public async Task<IActionResult> RecycleBin(RoomFilterRequest filter)
        {
            // 1. Đảm bảo các giá trị mặc định cho phân trang
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = 5; // Cùng kích thước với trang Index

            // 2. Gọi service với object filter (Service sẽ tự lọc theo filter.SearchTerm)
            var deletedRooms = await _roomService.GetDeletedRoomsPagedAsync(filter);

            // 3. Gán lại filter vào ViewBag để giữ giá trị trong ô tìm kiếm trên View
            ViewBag.Filter = filter;

            return View(deletedRooms);
        }

        // Thêm hành động Khôi phục phòng
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            try
            {
                var result = await _roomService.RestoreRoomAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Khôi phục phòng thành công!" });
                }
                return Json(new { success = false, message = "Không thể khôi phục phòng này." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            try
            {
                var result = await _roomService.DeletePermanentlyAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Phòng đã bị xóa vĩnh viễn khỏi hệ thống." });
                }
                return Json(new { success = false, message = "Xóa vĩnh viễn thất bại." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}