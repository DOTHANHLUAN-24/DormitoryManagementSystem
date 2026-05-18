using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
<<<<<<< HEAD
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
=======
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DormitoryManagement.Controllers
{
    [Route("Room")]
    [Authorize(Roles = "Admin,ManagerStaff")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
<<<<<<< HEAD
        private readonly IUnitOfWork _unitOfWork; // Thêm IUnitOfWork để load dữ liệu cho dropdown
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _roomService = roomService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
=======
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
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(RoomFilterRequest filter)
        {
<<<<<<< HEAD
            // Đảm bảo filter có giá trị mặc định nếu cần (tùy vào logic Service của bạn)
            var pagedRooms = await _roomService.GetPagedRoomsAsync(filter);

            // Lưu lại filter để giữ trạng thái tìm kiếm trên giao diện
            ViewBag.Filter = filter;
=======
            // 1. Thiết lập phân trang mặc định
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = 5;

            // 2. Lấy danh sách phòng đã phân trang từ Service
            var pagedRooms = await _roomService.GetPagedRoomsAsync(filter);

            // 3. Lấy thông số thống kê thực tế từ database
            var stats = await _roomService.GetRoomStatisticsAsync();
            ViewBag.TotalRooms = stats.TotalRooms;
            ViewBag.AvailableRooms = stats.AvailableRooms;
            ViewBag.OccupiedRooms = stats.OccupiedRooms;
            ViewBag.MaintenanceRooms = stats.MaintenanceRooms;

            // 4. Load các Dropdown (Tòa nhà, Loại phòng, Trạng thái)
            await PopulateDropdownsAsync(filter.BlockId, filter.RoomTypeId);

            // 5. Gán filter vào ViewBag để View giữ trạng thái các ô Search/Filter
            ViewBag.Filter = filter;

>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
            return View(pagedRooms);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            return View(room);
        }

<<<<<<< HEAD
        // GET: Room/Create
=======
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
        [HttpGet("Create")]
        public IActionResult Create()
        {
<<<<<<< HEAD
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
=======
            await PopulateDropdownsAsync();
            var model = new CreateRoomRequest { Status = RoomStatus.Available, Floor = 1 };
            return View(model);
        }

        [HttpPost("Create")]
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomRequest request) // 1. Phải là CreateRoomRequest
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
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
=======
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
            return View(request);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            // Ánh xạ từ RoomResponse sang UpdateRoomRequest
            var updateRequest = _mapper.Map<UpdateRoomRequest>(room);
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002

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

<<<<<<< HEAD
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
=======
            await PopulateDropdownsAsync(request.BlockId, request.RoomTypeId);
            return View(request);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            if (result) return Json(new { success = true, message = "Đã chuyển phòng vào thùng rác." });
            return Json(new { success = false, message = "Xóa thất bại." });
        }

        // --- CÁC PHƯƠNG THỨC BỔ TRỢ ---

        private async Task PopulateDropdownsAsync(Guid? selectedBlock = null, Guid? selectedType = null)
        {
            // Lấy dữ liệu từ các service tương ứng
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
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
        }
    }
}