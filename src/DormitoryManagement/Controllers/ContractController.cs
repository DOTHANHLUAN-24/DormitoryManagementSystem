using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DormitoryManagement.Controllers
{
    public class ContractController(
        IContractService contractService,
        IUserRepository userRepository,
        IBedRepository bedRepository
    ) : BaseController
    {
        private readonly IContractService _contractService = contractService;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBedRepository _bedRepository = bedRepository;

        // Admin/Manager actions
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(int page = 1, string search = "", ContractStatus? status = null)
        {
            Logger.LogInformation("Đang tải danh sách hợp đồng trang {Page}, tìm kiếm: '{Search}', trạng thái: '{Status}'", page, search, status);
            var pagedContracts = await _contractService.GetPagedContractsAsync(page, PageSize, search, status);
            ViewBag.Search = search;
            ViewBag.Status = status;
            ViewBag.PendingCount = await _contractService.GetPendingCountAsync();
            ViewBag.ActiveCount = (await _contractService.GetPagedContractsAsync(1, 1, status: ContractStatus.Active)).TotalCount;
            ViewBag.TerminatedCount = (await _contractService.GetPagedContractsAsync(1, 1, status: ContractStatus.Terminated)).TotalCount;
            return View(pagedContracts);
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpGet("Pending")]
        public async Task<IActionResult> Pending(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách hợp đồng chờ duyệt trang {Page}, tìm kiếm: '{Search}'", page, search);
            var pagedContracts = await _contractService.GetPagedContractsAsync(page, PageSize, search, ContractStatus.Pending);
            ViewBag.Search = search;
            return View(pagedContracts);
        }

        // Student booking request
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Guid bedId, DateTime startDate, string? notes)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Logger.LogInformation("Đang xử lý yêu cầu đăng ký phòng cho giường ID: {BedId} từ tài khoản ID: {UserIdString}", bedId, userIdString);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                Logger.LogWarning("Yêu cầu đăng ký phòng thất bại: Người dùng chưa đăng nhập hoặc phiên đã hết hạn.");
                return Json(new { success = false, message = "Người dùng chưa đăng nhập hoặc phiên làm việc đã hết hạn." });
            }

            var bed = await _bedRepository.GetByIdAsync(bedId);
            if (bed == null || bed.Status != BedStatus.Available)
            {
                Logger.LogWarning("Yêu cầu đăng ký phòng thất bại: Giường ID {BedId} không còn trống hoặc không tồn tại.", bedId);
                return Json(new { success = false, message = "Giường đã chọn không còn trống hoặc không tồn tại." });
            }

            // Create a pending contract
            var randCode = "HD-" + DateTime.Now.Year + "-" + new Random().Next(1000, 9999);
            var contract = new Contract
            {
                ContractCode = randCode,
                UserId = userId,
                BedId = bedId,
                StartDate = startDate,
                EndDate = startDate.AddMonths(6), // Mặc định 6 tháng
                DepositAmount = 1000000, // Tiền cọc mặc định 1.000.000 VNĐ
                Status = ContractStatus.Pending,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            var success = await _contractService.CreateContractAsync(contract);
            if (success)
            {
                Logger.LogInformation("Đăng ký giường ID {BedId} cho người dùng ID {UserId} thành công với mã hợp đồng: {ContractCode}", bedId, userId, contract.ContractCode);
                return Json(new { success = true, message = "Yêu cầu đăng ký phòng thành công! Ban quản lý sẽ sớm phê duyệt hợp đồng của bạn." });
            }

            Logger.LogWarning("Tạo yêu cầu đăng ký hợp đồng cho giường ID {BedId} thất bại tại Service.", bedId);
            return Json(new { success = false, message = "Có lỗi xảy ra khi tạo yêu cầu. Vui lòng liên hệ Admin." });
        }

        [HttpGet("MyContracts")]
        public async Task<IActionResult> MyContracts()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Logger.LogInformation("Đang truy cập danh sách hợp đồng cá nhân của tài khoản ID: {UserIdString}", userIdString);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                Logger.LogWarning("Truy cập hợp đồng cá nhân thất bại: Chưa đăng nhập.");
                return RedirectToAction("Login", "Account");
            }

            var contracts = await _contractService.GetByUserIdAsync(userId);
            return View(contracts);
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            Logger.LogInformation("Đang truy cập trang tạo mới hợp đồng thủ công.");
            var users = await _userRepository.GetQuery()
                .Where(u => !u.IsDeleted && u.IsActive && u.Role == UserRole.Student)
                .ToListAsync();

            var beds = await _bedRepository.GetQuery()
                .Include(b => b.Room)
                .ThenInclude(r => r.Block)
                .Where(b => b.Status == BedStatus.Available)
                .ToListAsync();

            ViewBag.Users = new SelectList(users, "Id", "FullName");
            ViewBag.Beds = new SelectList(beds.Select(b => new
            {
                Id = b.Id,
                Text = $"Tòa {b.Room?.Block?.BlockName ?? ""} - Phòng {b.Room?.RoomNumber ?? ""} - Giường {b.BedNumber}"
            }), "Id", "Text");

            var model = new Contract
            {
                ContractCode = "HD-" + DateTime.Now.Year + "-" + new Random().Next(1000, 9999),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(6)
            };
            return View(model);
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            Logger.LogInformation("Đang xử lý tạo mới hợp đồng thủ công cho sinh viên ID: {UserId}, giường ID: {BedId}", contract.UserId, contract.BedId);
            // Loại bỏ các thuộc tính liên kết khỏi ModelState validation để tránh lỗi binding do nullable reference types
            ModelState.Remove("Bed");
            ModelState.Remove("User");

            // Kiểm tra và bắt lỗi bắt buộc chọn giường
            if (contract.BedId == Guid.Empty)
            {
                ModelState.Remove("BedId");
                ModelState.AddModelError("BedId", "Vui lòng chọn vị trí giường trống.");
            }

            // Kiểm tra và bắt lỗi bắt buộc chọn sinh viên
            if (contract.UserId == null || contract.UserId == Guid.Empty)
            {
                ModelState.Remove("UserId");
                ModelState.AddModelError("UserId", "Vui lòng chọn sinh viên thuê.");
            }

            // Kiểm tra tính hợp lệ của ngày thuê
            if (contract.EndDate <= contract.StartDate)
            {
                ModelState.AddModelError("EndDate", "Ngày hết hạn hợp đồng phải sau ngày bắt đầu thuê.");
            }

            if (ModelState.IsValid)
            {
                contract.Status = ContractStatus.Pending;
                contract.CreatedDate = DateTime.Now;
                contract.IsActive = true;
                contract.IsDeleted = false;

                var success = await _contractService.CreateContractAsync(contract);
                if (success)
                {
                    Logger.LogInformation("Tạo hợp đồng thủ công thành công, mã hợp đồng: {ContractCode}", contract.ContractCode);
                    TempData["Success"] = "Tạo hợp đồng thủ công thành công!";
                    return RedirectToAction(nameof(Index));
                }
                Logger.LogWarning("Tạo hợp đồng thủ công thất bại tại Service.");
                ModelState.AddModelError("", "Không thể lưu hợp đồng.");
            }
            else
            {
                Logger.LogWarning("Dữ liệu tạo hợp đồng thủ công không hợp lệ.");
            }

            var users = await _userRepository.GetQuery()
                .Where(u => !u.IsDeleted && u.IsActive && u.Role == UserRole.Student)
                .ToListAsync();

            var beds = await _bedRepository.GetQuery()
                .Include(b => b.Room)
                .ThenInclude(r => r.Block)
                .Where(b => b.Status == BedStatus.Available)
                .ToListAsync();

            ViewBag.Users = new SelectList(users, "Id", "FullName", contract.UserId);
            ViewBag.Beds = new SelectList(beds.Select(b => new
            {
                Id = b.Id,
                Text = $"Tòa {b.Room?.Block?.BlockName ?? ""} - Phòng {b.Room?.RoomNumber ?? ""} - Giường {b.BedNumber}"
            }), "Id", "Text", contract.BedId);

            return View(contract);
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpGet("Approve/{id}")]
        public async Task<IActionResult> Approve(Guid id)
        {
            Logger.LogInformation("Đang tải trang phê duyệt hợp đồng ID: {Id}", id);
            var contract = await _contractService.GetByIdAsync(id);
            if (contract == null)
            {
                Logger.LogWarning("Không tìm thấy hợp đồng ID {Id} để xem phê duyệt.", id);
                return NotFound();
            }

            return View(contract);
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("Approve/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id, string contractCode, DateTime startDate, DateTime endDate, decimal depositAmount, bool isApprove = true)
        {
            Logger.LogInformation("Đang xử lý phê duyệt hợp đồng ID: {Id}, quyết định isApprove = {IsApprove}", id, isApprove);
            var contract = await _contractService.GetByIdAsync(id);
            if (contract == null)
            {
                Logger.LogWarning("Không tìm thấy hợp đồng ID {Id} để thực hiện phê duyệt.", id);
                return NotFound();
            }

            contract.ContractCode = contractCode;
            contract.StartDate = startDate;
            contract.EndDate = endDate;
            contract.DepositAmount = depositAmount;

            if (isApprove)
            {
                contract.Status = ContractStatus.Active;
            }

            var success = await _contractService.UpdateContractAsync(contract);
            if (success)
            {
                Logger.LogInformation("Phê duyệt/Cập nhật hợp đồng ID: {Id} thành công.", id);
                return Json(new { success = true, message = isApprove ? "Đã phê duyệt và kích hoạt hợp đồng!" : "Đã cập nhật thông tin hợp đồng." });
            }

            Logger.LogWarning("Cập nhật thông tin phê duyệt hợp đồng ID: {Id} thất bại tại Service.", id);
            return Json(new { success = false, message = "Không thể lưu thông tin hợp đồng." });
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa hợp đồng ID: {Id}", id);
            var success = await _contractService.DeleteContractAsync(id);
            if (success)
            {
                Logger.LogInformation("Đã xóa hợp đồng ID: {Id} thành công.", id);
                return Json(new { success = true, message = "Đã xóa hợp đồng thành công." });
            }
            Logger.LogWarning("Xóa hợp đồng ID: {Id} thất bại.", id);
            return Json(new { success = false, message = "Xóa hợp đồng thất bại." });
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpGet("RecycleBin")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang truy cập thùng rác hợp đồng trang {Page}, tìm kiếm: '{Search}'", page, search);
            var pagedContracts = await _contractService.GetDeletedContractsPagedAsync(page, PageSize, search);
            ViewBag.Search = search;
            return View(pagedContracts);
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục hợp đồng ID: {Id}", id);
            var success = await _contractService.RestoreContractAsync(id);
            if (success)
            {
                return Json(new { success = true, message = "Khôi phục hợp đồng thành công!" });
            }
            return Json(new { success = false, message = "Khôi phục hợp đồng thất bại." });
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn hợp đồng ID: {Id}", id);
            var success = await _contractService.DeletePermanentlyAsync(id);
            if (success)
            {
                return Json(new { success = true, message = "Đã xóa vĩnh viễn hợp đồng khỏi cơ sở dữ liệu." });
            }
            return Json(new { success = false, message = "Xóa vĩnh viễn hợp đồng thất bại." });
        }
    }
}
