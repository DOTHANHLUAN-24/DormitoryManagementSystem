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
        [HttpGet("Index")]
        public async Task<IActionResult> Index(RoomFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = PageSize;

            Logger.LogInformation("Đang tải danh sách phòng trang {Page}, tìm kiếm: '{Search}', tòa: '{BlockId}', loại: '{RoomTypeId}'", filter.PageNumber, filter.SearchTerm, filter.BlockId, filter.RoomTypeId);
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
            Logger.LogInformation("Đang xem chi tiết phòng ID: {Id}", id);
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                Logger.LogWarning("Không tìm thấy thông tin phòng ID: {Id}", id);
                return NotFound();
            }

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
            Logger.LogInformation("Đang thực hiện gán sinh viên ID: {UserId} vào giường ID: {BedId}", userId, bedId);
            var bed = await _bedRepository.GetByIdAsync(bedId);
            if (bed == null || bed.Status != BedStatus.Available)
            {
                Logger.LogWarning("Gán sinh viên thất bại: Giường ID {BedId} không còn trống hoặc không tồn tại.", bedId);
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

                Logger.LogInformation("Gán sinh viên ID: {UserId} vào giường ID: {BedId} thành công với hợp đồng {ContractCode}.", userId, bedId, contractCode);
                return Json(new { success = true, message = "Gán sinh viên vào phòng thành công!" });
            }

            Logger.LogWarning("Lỗi tạo hợp đồng gán sinh viên ID {UserId} vào giường ID {BedId} tại Service.", userId, bedId);
            return Json(new { success = false, message = "Có lỗi xảy ra khi tạo hợp đồng." });
        }

        [HttpGet("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            Logger.LogInformation("Đang tải trang thêm phòng mới.");
            await PopulateDropdownsAsync();
            var model = new CreateRoomRequest { Status = RoomStatus.Available, Floor = 1 };
            return View(model);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomRequest request)
        {
            Logger.LogInformation("Đang thực hiện tạo phòng mới số: {RoomNumber} tòa ID: {BlockId}", request.RoomNumber, request.BlockId);
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roomService.CreateRoomAsync(request);
                    if (result)
                    {
                        Logger.LogInformation("Tạo phòng {RoomNumber} thành công.", request.RoomNumber);
                        TempData["Success"] = "Thêm phòng mới thành công!";
                        return RedirectToAction(nameof(Index));
                    }

                    Logger.LogWarning("Không thể tạo phòng {RoomNumber} tại Service.", request.RoomNumber);
                    ModelState.AddModelError("", "Không thể tạo phòng. Vui lòng kiểm tra lại.");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Lỗi xảy ra khi tạo phòng {RoomNumber}.", request.RoomNumber);
                    TempData["Error"] = ex.Message;
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                Logger.LogWarning("Dữ liệu tạo phòng mới không hợp lệ.");
            }

            ViewBag.Blocks = new SelectList(await _blockService.GetAllBlocksAsync(), "Id", "BlockName", request.BlockId);
            ViewBag.RoomTypes = new SelectList(await _roomTypeService.GetAllRoomTypesAsync(), "Id", "TypeName", request.RoomTypeId);

            return View(request);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang tải trang chỉnh sửa phòng ID: {Id}", id);
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                Logger.LogWarning("Không tìm thấy thông tin phòng ID {Id} để chỉnh sửa.", id);
                return NotFound();
            }

            // Ánh xạ từ RoomResponse sang UpdateRoomRequest
            var updateRequest = _mapper.Map<UpdateRoomRequest>(room);

            await PopulateDropdownsAsync(room.BlockId, room.RoomTypeId);
            return View(updateRequest);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateRoomRequest request)
        {
            Logger.LogInformation("Đang xử lý cập nhật phòng ID: {Id}", id);
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roomService.UpdateRoomAsync(id, request);
                    if (result)
                    {
                        Logger.LogInformation("Cập nhật phòng ID: {Id} thành công.", id);
                        TempData["Success"] = "Cập nhật thông tin phòng thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    Logger.LogWarning("Cập nhật phòng ID: {Id} thất bại tại Service.", id);
                    TempData["Error"] = "Cập nhật thất bại.";
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Lỗi xảy ra khi cập nhật phòng ID: {Id}.", id);
                    TempData["Error"] = ex.Message;
                }
            }
            else
            {
                Logger.LogWarning("Dữ liệu cập nhật phòng ID: {Id} không hợp lệ.", id);
            }

            await PopulateDropdownsAsync(request.BlockId, request.RoomTypeId);
            return View(request);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa phòng ID: {Id}", id);
            try
            {
                var result = await _roomService.DeleteRoomAsync(id);

                if (result)
                {
                    Logger.LogInformation("Đã xóa mềm (chuyển vào thùng rác) phòng ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Đã chuyển phòng vào thùng rác." });
                }

                Logger.LogWarning("Xóa phòng ID: {Id} thất bại.", id);
                return Json(new { success = false, message = "Xóa thất bại. Vui lòng thử lại." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa phòng ID: {Id}.", id);
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

            Logger.LogInformation("Đang tải danh sách phòng đã xóa (thùng rác) trang {Page}, tìm kiếm: '{Search}'", filter.PageNumber, filter.SearchTerm);
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
            Logger.LogInformation("Đang yêu cầu khôi phục phòng ID: {Id}", id);
            try
            {
                var result = await _roomService.RestoreRoomAsync(id);
                if (result)
                {
                    Logger.LogInformation("Khôi phục phòng ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Khôi phục phòng thành công!" });
                }
                Logger.LogWarning("Khôi phục phòng ID: {Id} thất bại.", id);
                return Json(new { success = false, message = "Không thể khôi phục phòng này." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi khôi phục phòng ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn phòng ID: {Id}", id);
            try
            {
                var result = await _roomService.DeletePermanentlyAsync(id);
                if (result)
                {
                    Logger.LogInformation("Xóa vĩnh viễn phòng ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Phòng đã bị xóa vĩnh viễn khỏi hệ thống." });
                }
                Logger.LogWarning("Xóa vĩnh viễn phòng ID: {Id} thất bại tại Service.", id);
                return Json(new { success = false, message = "Xóa vĩnh viễn thất bại." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa vĩnh viễn phòng ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}