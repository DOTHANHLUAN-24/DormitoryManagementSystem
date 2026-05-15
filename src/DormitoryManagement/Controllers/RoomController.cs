using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Route("Room")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(RoomFilterRequest filter)
        {
            // Đảm bảo filter có giá trị mặc định nếu cần (tùy vào logic Service của bạn)
            var pagedRooms = await _roomService.GetPagedRoomsAsync(filter);

            // Lưu lại filter để giữ trạng thái tìm kiếm trên giao diện
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
        public IActionResult Create()
        {
            // Trả về DTO rỗng để Helper Tag của ASP.NET Core hoạt động chuẩn
            return View(new CreateRoomRequest());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Log lỗi ra console để debug nhanh trong quá trình phát triển
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine(">>> LỖI CREATE ROOM: " + string.Join(", ", errors));
                return View(request);
            }

            try
            {
                await _roomService.CreateRoomAsync(request);
                TempData["Success"] = "Thêm phòng mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Lỗi: " + ex.Message);
                return View(request);
            }
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var roomResponse = await _roomService.GetRoomByIdAsync(id);
            if (roomResponse == null) return NotFound();

            // Sử dụng AutoMapper để chuyển từ Response DTO sang Update Request DTO
            var updateRequest = _mapper.Map<UpdateRoomRequest>(roomResponse);

            return View(updateRequest);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                var success = await _roomService.UpdateRoomAsync(id, request);
                if (success)
                {
                    TempData["Success"] = "Cập nhật thông tin phòng thành công!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Không tìm thấy phòng để cập nhật hoặc cập nhật thất bại.");
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Lỗi: " + ex.Message);
                return View(request);
            }
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken] // Thêm bảo mật cho request xóa
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _roomService.DeleteRoomAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Đã xóa phòng thành công." });
                }
                return Json(new { success = false, message = "Không thể xóa phòng này (có thể đang có sinh viên)." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}