using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DormitoryManagement.Controllers
{
    [Route("Room")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IUnitOfWork _unitOfWork; // Thêm IUnitOfWork để load dữ liệu cho dropdown
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _roomService = roomService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        // GET: Room/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            // Gọi hàm fake dữ liệu trước khi trả về View
            LoadDropdownDataFake();

            // Khởi tạo model mặc định
            var model = new CreateRoomRequest
            {
                Status = RoomStatus.Available, // Giá trị mặc định
                Floor = 1
            };

            return View(model);
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomRequest request) // 1. Phải là CreateRoomRequest
        {
            if (!ModelState.IsValid)
            {
                // 2. LỖI THƯỜNG GẶP: Quên nạp lại SelectList khi dữ liệu không hợp lệ
                // Nếu thiếu 2 dòng này, khi trả về View nó sẽ báo lỗi ViewBag null
                LoadDropdownDataFake();

                return View(request); // Trả về đúng model request
            }

            try
            {
                // 3. Logic lưu vào DB (Sau này bạn sẽ code ở đây)
                // Hiện tại tạm thời redirect để test giao diện
                TempData["SuccessMessage"] = "Thêm phòng thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                LoadDropdownDataFake();
                return View(request);
            }
        }

        private void LoadDropdownDataFake()
        {
            // 1. Fake danh sách Tòa nhà (Blocks)
            var fakeBlocks = new List<object>
        {
            new { Id = 1, Name = "Tòa Nhà A (Khu Nam)" },
            new { Id = 2, Name = "Tòa Nhà B (Khu Bắc)" },
            new { Id = 3, Name = "Tòa Nhà C (VIP)" }
        };
            ViewBag.Blocks = new SelectList(fakeBlocks, "Id", "Name");

            // 2. Fake danh sách Loại phòng (RoomTypes)
            var fakeRoomTypes = new List<object>
        {
            new { Id = 10, Name = "Phòng Đơn (1 Người)" },
            new { Id = 11, Name = "Phòng Đôi (2 Người)" },
            new { Id = 12, Name = "Phòng Tập Thể (8 Người)" }
        };
            ViewBag.RoomTypes = new SelectList(fakeRoomTypes, "Id", "Name");
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