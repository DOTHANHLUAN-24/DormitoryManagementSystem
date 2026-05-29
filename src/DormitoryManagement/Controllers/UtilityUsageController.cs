using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UtilityUsageController(
        IUtilityUsageService service,
        IUtilityService utilityService,
        IBlockService blockService,
        IRoomService roomService,
        IContractService contractService
    ) : BaseController
    {
        private readonly IUtilityUsageService _service = service;
        private readonly IUtilityService _utilityService = utilityService;
        private readonly IBlockService _blockService = blockService;
        private readonly IRoomService _roomService = roomService;
        private readonly IContractService _contractService = contractService;

        /// <summary>
        /// GET: UtilityUsage/Index
        /// Danh sách chỉ số điện/nước của toàn bộ KTX (Admin/Quản lý).
        /// </summary>
        [HttpGet("")]
        [HttpGet("Index")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Index(
            string search = "", 
            Guid? blockId = null, 
            Guid? roomId = null, 
            int? month = null, 
            int? year = null, 
            Guid? utilityId = null, 
            string? isActive = null, 
            int page = 1)
        {
            Logger.LogInformation("Admin tải danh sách ghi nhận chỉ số trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageIndex = page > 0 ? page : 1;

            bool? activeFilter = null;
            if (isActive == "true") activeFilter = true;
            else if (isActive == "false") activeFilter = false;

            var result = await _service.GetPagedUtilityUsagesAsync(
                pageIndex, 
                PageSize, 
                search, 
                blockId, 
                roomId, 
                month, 
                year, 
                utilityId, 
                activeFilter, 
                isDeleted: false);

            // Populate ViewBags cho bộ lọc
            ViewBag.Blocks = await _blockService.GetAllBlocksAsync();
            if (blockId.HasValue)
            {
                ViewBag.Rooms = await _roomService.GetRoomsByBlockAsync(blockId.Value);
            }
            else
            {
                var allRoomsResult = await _roomService.GetPagedRoomsAsync(new RoomFilterRequest { PageNumber = 1, PageSize = 999 });
                ViewBag.Rooms = allRoomsResult.Items;
            }

            var activeUtilities = await _utilityService.GetAllActiveUtilitiesAsync();
            ViewBag.Utilities = activeUtilities.Where(u => u.UtilityName.ToLower().Contains("điện") || u.UtilityName.ToLower().Contains("nước")).ToList();

            ViewBag.Search = search;
            ViewBag.BlockId = blockId;
            ViewBag.RoomId = roomId;
            ViewBag.Month = month;
            ViewBag.Year = year;
            ViewBag.UtilityId = utilityId;
            ViewBag.IsActive = isActive;

            return View(result);
        }

        /// <summary>
        /// GET: UtilityUsage/Create
        /// Giao diện ghi nhận số điện/nước mới.
        /// </summary>
        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Create()
        {
            Logger.LogInformation("Truy cập trang tạo mới ghi nhận số điện nước.");
            await PopulateCreateViewBagAsync();
            return View();
        }

        /// <summary>
        /// POST: UtilityUsage/Create
        /// Xử lý lưu ghi nhận số điện/nước mới.
        /// </summary>
        [HttpPost("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid roomId, Guid utilityId, int month, int year, double previousIndex, double currentIndex, bool isActive = true)
        {
            Logger.LogInformation("Xử lý thêm mới ghi nhận điện nước cho phòng ID {RoomId}, Dịch vụ ID {UtilityId}", roomId, utilityId);

            if (roomId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng chọn phòng ở.");
            }
            if (utilityId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Vui lòng chọn dịch vụ điện hoặc nước.");
            }
            if (month < 1 || month > 12)
            {
                ModelState.AddModelError(string.Empty, "Tháng ghi nhận chỉ số không hợp lệ.");
            }
            if (year < 2000 || year > 2100)
            {
                ModelState.AddModelError(string.Empty, "Năm ghi nhận chỉ số không hợp lệ.");
            }
            if (currentIndex < previousIndex)
            {
                ModelState.AddModelError(string.Empty, "Chỉ số mới (cuối kỳ) không được nhỏ hơn chỉ số cũ (đầu kỳ).");
            }

            if (!ModelState.IsValid)
            {
                await PopulateCreateViewBagAsync();
                return View();
            }

            try
            {
                var success = await _service.CreateUtilityUsageAsync(roomId, utilityId, month, year, previousIndex, currentIndex, isActive);
                if (success)
                {
                    TempData["Success"] = "Ghi nhận chỉ số điện/nước mới thành công.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Lưu chỉ số tiêu thụ thất bại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi lưu chỉ số tiêu thụ điện nước.");
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            await PopulateCreateViewBagAsync();
            return View();
        }

        /// <summary>
        /// GET: UtilityUsage/Edit/{id}
        /// Giao diện chỉnh sửa chỉ số tiêu thụ.
        /// </summary>
        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Truy cập trang chỉnh sửa chỉ số tiêu thụ ID: {Id}", id);
            var item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                Logger.LogWarning("Không tìm thấy chỉ số tiêu thụ ID: {Id} để sửa.", id);
                return NotFound();
            }

            return View(item);
        }

        /// <summary>
        /// POST: UtilityUsage/Edit/{id}
        /// Xử lý lưu thông tin chỉnh sửa chỉ số tiêu thụ.
        /// </summary>
        [HttpPost("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, double previousIndex, double currentIndex, bool isActive)
        {
            Logger.LogInformation("Xử lý cập nhật chỉ số tiêu thụ ID: {Id}", id);

            if (currentIndex < previousIndex)
            {
                ModelState.AddModelError(string.Empty, "Chỉ số mới (cuối kỳ) không được nhỏ hơn chỉ số cũ (đầu kỳ).");
            }

            if (!ModelState.IsValid)
            {
                var item = await _service.GetByIdAsync(id);
                return View(item);
            }

            try
            {
                var success = await _service.UpdateUtilityUsageAsync(id, previousIndex, currentIndex, isActive);
                if (success)
                {
                    TempData["Success"] = "Cập nhật chỉ số tiêu thụ thành công.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Cập nhật chỉ số tiêu thụ thất bại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi cập nhật chỉ số tiêu thụ điện nước ID {Id}.", id);
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            var usage = await _service.GetByIdAsync(id);
            return View(usage);
        }

        /// <summary>
        /// POST: UtilityUsage/Delete/{id}
        /// Xóa mềm chỉ số tiêu thụ (AJAX).
        /// </summary>
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Xóa mềm ghi nhận chỉ số ID: {Id}", id);
            var success = await _service.SoftDeleteUtilityUsageAsync(id);
            return Json(new { success = success, message = success ? "Đã đưa ghi nhận chỉ số vào thùng rác." : "Xóa chỉ số thất bại." });
        }

        /// <summary>
        /// GET: UtilityUsage/RecycleBin
        /// Danh sách chỉ số đã xóa mềm.
        /// </summary>
        [HttpGet("RecycleBin")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> RecycleBin(string search = "", int page = 1)
        {
            Logger.LogInformation("Truy cập thùng rác chỉ số điện nước trang {Page}", page);
            int pageIndex = page > 0 ? page : 1;
            
            var result = await _service.GetPagedUtilityUsagesAsync(
                pageIndex, 
                PageSize, 
                search, 
                isDeleted: true);

            ViewBag.Search = search;
            return View(result);
        }

        /// <summary>
        /// POST: UtilityUsage/Restore/{id}
        /// Khôi phục chỉ số đã xóa mềm (AJAX).
        /// </summary>
        [HttpPost("Restore/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Khôi phục ghi nhận chỉ số ID: {Id}", id);
            var success = await _service.RestoreUtilityUsageAsync(id);
            return Json(new { success = success, message = success ? "Khôi phục ghi nhận chỉ số thành công." : "Khôi phục thất bại." });
        }

        /// <summary>
        /// GET: UtilityUsage/GetPreviousIndex
        /// API phụ trợ lấy chỉ số gần đây nhất của phòng và dịch vụ (AJAX).
        /// </summary>
        [HttpGet("GetPreviousIndex")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> GetPreviousIndex(Guid roomId, Guid utilityId)
        {
            if (roomId == Guid.Empty || utilityId == Guid.Empty)
            {
                return Json(new { success = false, previousIndex = 0.0 });
            }

            var index = await _service.GetLatestIndexAsync(roomId, utilityId);
            return Json(new { success = true, previousIndex = index });
        }

        /// <summary>
        /// GET: UtilityUsage/MyUsages
        /// Lịch sử sử dụng điện nước của Sinh viên đăng nhập.
        /// </summary>
        [HttpGet("MyUsages")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyUsages(int page = 1)
        {
            var userId = CurrentUserId;
            if (userId == null)
            {
                return Unauthorized();
            }

            Logger.LogInformation("Sinh viên {UserId} xem lịch sử điện nước của phòng.", userId);
            
            // Tìm hợp đồng hoạt động của sinh viên để xác định phòng
            var contracts = await _contractService.GetByUserIdAsync(userId.Value);
            var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active && !c.IsDeleted);

            if (activeContract == null || activeContract.Bed?.Room == null)
            {
                TempData["Error"] = "Bạn không có hợp đồng thuê phòng nào đang hoạt động để tra cứu điện nước.";
                return RedirectToAction("Index", "Home");
            }

            var roomId = activeContract.Bed.Room.Id;
            ViewBag.RoomNumber = activeContract.Bed.Room.RoomNumber;
            ViewBag.BlockName = activeContract.Bed.Room.Block?.BlockName;

            int pageIndex = page > 0 ? page : 1;
            var result = await _service.GetPagedUtilityUsagesByRoomIdAsync(roomId, pageIndex, PageSize, null, isActive: true);

            return View(result);
        }

        private async Task PopulateCreateViewBagAsync()
        {
            ViewBag.Blocks = await _blockService.GetAllBlocksAsync();
            
            var allRoomsResult = await _roomService.GetPagedRoomsAsync(new RoomFilterRequest { PageNumber = 1, PageSize = 999 });
            ViewBag.Rooms = allRoomsResult.Items.Where(r => r.Status == RoomStatus.Available.ToString() || r.Status == RoomStatus.Full.ToString() || r.Status == RoomStatus.Reserved.ToString()).ToList();

            var activeUtilities = await _utilityService.GetAllActiveUtilitiesAsync();
            ViewBag.Utilities = activeUtilities.Where(u => u.UtilityName.ToLower().Contains("điện") || u.UtilityName.ToLower().Contains("nước")).ToList();

            ViewBag.Month = DateTime.Now.Month;
            ViewBag.Year = DateTime.Now.Year;
        }
    }
}
