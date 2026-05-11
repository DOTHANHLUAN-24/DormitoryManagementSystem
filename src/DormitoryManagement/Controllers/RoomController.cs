using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DormitoryManagement.Controllers
{
    [Route("Room")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(
            IRoomService roomService
            )
        {
            _roomService = roomService;
        }

        // 1. Danh sách & Tìm kiếm (Hỗ trợ phân trang)
        [HttpGet("")]
        public async Task<IActionResult> Index(RoomFilterRequest filter)
        {
            var pagedRooms = await _roomService.GetPagedRoomsAsync(filter);
            return View(pagedRooms);
        }

        // 2. Chi tiết
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            return room == null ? NotFound() : View(room);
        }

        // 3. Tạo mới (GET)
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return View(new CreateRoomRequest());
        }

        // 4. Tạo mới (POST)
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                await _roomService.CreateRoomAsync(request);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        // 5. Cập nhật (GET)
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            // if (room == null) return NotFound();

            // Chuyển từ Response DTO sang Request DTO
            var updateRequest = new UpdateRoomRequest { /* map properties */ };
            // Lưu ý: Nếu dùng AutoMapper, hãy map room -> updateRequest ở đây

            return View(updateRequest);
        }

        // 6. Cập nhật (POST)
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var success = await _roomService.UpdateRoomAsync(id, request);
            if (success) return RedirectToAction(nameof(Index));

            return View(request);
        }

        // 7. Xóa (AJAX)
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            return Json(new { success = result });
        }
    }
}