using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Controllers
{
    [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
    public class RoomController
    (
        IRoomService roomService,
        IBlockService blockService,
        IRoomTypeService roomTypeService,
        IContractRepository contractRepository,
        IContractService contractService,
        IBedRepository bedRepository,
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : BaseController
    {
        private readonly IRoomService _roomService = roomService;
        private readonly IBlockService _blockService = blockService;
        private readonly IRoomTypeService _roomTypeService = roomTypeService;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IContractService _contractService = contractService;
        private readonly IBedRepository _bedRepository = bedRepository;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet("")]
        public async Task<IActionResult> Index(RoomFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = PageSize;

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

            // Load active contracts for the beds in this room
            var bedIds = room.Beds.Select(b => b.Id).ToList();
            var activeContracts = await _contractRepository.GetQuery()
                .Include(c => c.User)
                .Where(c => bedIds.Contains(c.BedId) && c.Status == ContractStatus.Active)
                .ToListAsync();

            ViewBag.ActiveContracts = activeContracts;

            // Load students without room for the assignment dropdown
            var studentsWithoutRoom = await _userManager.Users
                .Where(u => u.Role == UserRole.Student && u.IsActive && !u.IsDeleted)
                .Where(u => !_contractRepository.GetQuery().Any(c => c.UserId == u.Id && c.Status == ContractStatus.Active))
                .ToListAsync();

            ViewBag.StudentsWithoutRoom = studentsWithoutRoom;

            return View(room);
        }

        [HttpPost("AssignStudent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignStudent(Guid bedId, Guid userId, string contractCode, DateTime startDate, DateTime endDate, decimal depositAmount)
        {
            var bed = await _bedRepository.GetByIdAsync(bedId);
            if (bed == null || bed.Status != BedStatus.Available)
            {
                return Json(new { success = false, message = "Giường không khả dụng hoặc đã có người." });
            }

            var contract = new Contract
            {
                ContractCode = contractCode,
                UserId = userId,
                BedId = bedId,
                StartDate = startDate,
                EndDate = endDate,
                DepositAmount = depositAmount,
                Status = ContractStatus.Active, // Phê duyệt trực tiếp nên là Active
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            var success = await _contractService.CreateContractAsync(contract);
            if (success)
            {
                // Cập nhật trạng thái giường thành Occupied
                bed.Status = BedStatus.Occupied;
                await _bedRepository.UpdateAsync(bed);
                await _unitOfWork.SaveChangesAsync();

                return Json(new { success = true, message = "Gán sinh viên vào phòng thành công!" });
            }

            return Json(new { success = false, message = "Có lỗi xảy ra khi tạo hợp đồng." });
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
        public async Task<IActionResult> Create(CreateRoomRequest request)
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

                    ModelState.AddModelError("", "Không thể tạo phòng. Vui lòng kiểm tra lại.");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Blocks = new SelectList(await _blockService.GetAllBlocksAsync(), "Id", "BlockName", request.BlockId);
            ViewBag.RoomTypes = new SelectList(await _roomTypeService.GetAllRoomTypesAsync(), "Id", "TypeName", request.RoomTypeId);

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
            filter.PageSize = PageSize; // Cùng kích thước với trang Index

            // 2. Gọi service với object filter (Service sẽ tự lọc theo filter.SearchTerm)
            var deletedRooms = await _roomService.GetDeletedRoomsPagedAsync(filter);

            // 3. Gán lại filter vào ViewBag để giữ giá trị trong ô tìm kiếm trên View
            ViewBag.Filter = filter;

            return View(deletedRooms);
        }

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