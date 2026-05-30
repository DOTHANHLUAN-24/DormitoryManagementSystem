using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Application.Dtos.Requests.Utilities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UtilityServiceRequestController(
        IUtilityServiceRequestService service,
        IUtilityService utilityService,
        IContractRepository contractRepository
    ) : BaseController
    {
        private readonly IUtilityServiceRequestService _service = service;
        private readonly IUtilityService _utilityService = utilityService;
        private readonly IContractRepository _contractRepository = contractRepository;

        /// <summary>
        /// Danh sách yêu cầu đăng ký dịch vụ dành cho Admin / ManagementStaff.
        /// </summary>
        [HttpGet("")]
        [HttpGet("Index")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Index(string search = "", string? status = null, int page = 1)
        {
            Logger.LogInformation("Admin tải danh sách đăng ký dịch vụ trang {Page}, tìm kiếm: '{Search}', trạng thái: '{Status}'", page, search, status);
            int pageIndex = page > 0 ? page : 1;
            
            var pagedRequests = await _service.GetPagedServiceRequestsAsync(pageIndex, PageSize, search, status);
            
            ViewBag.Search = search;
            ViewBag.Status = status;
            
            return View(pagedRequests);
        }

        /// <summary>
        /// Danh sách yêu cầu đăng ký dịch vụ cá nhân của Sinh viên.
        /// </summary>
        [HttpGet("MyRequests")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyRequests()
        {
            var userId = CurrentUserId;
            if (userId == null)
            {
                Logger.LogWarning("MyRequests: Không lấy được UserId từ context.");
                return Unauthorized();
            }

            Logger.LogInformation("Sinh viên {UserId} tải lịch sử đăng ký dịch vụ cá nhân.", userId);
            var requests = await _service.GetServiceRequestsByUserIdAsync(userId.Value);
            return View(requests);
        }

        /// <summary>
        /// Form đăng ký dịch vụ tiện ích mới (GET - Student).
        /// </summary>
        [HttpGet("Create")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create()
        {
            var userId = CurrentUserId;
            if (userId == null)
            {
                Logger.LogWarning("Create GET: Không lấy được UserId từ context.");
                return Unauthorized();
            }

            // Lấy thông tin phòng từ hợp đồng đang hoạt động
            var contract = await _contractRepository.GetQuery()
                .Include(c => c.Bed)
                    .ThenInclude(b => b.Room)
                        .ThenInclude(r => r.Block)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == DormitoryManagement.Domain.Enums.ContractStatus.Active);

            if (contract == null || contract.Bed?.Room == null)
            {
                TempData["Error"] = "Bạn không có hợp đồng thuê phòng hoạt động để đăng ký dịch vụ.";
                return RedirectToAction(nameof(MyRequests));
            }

            ViewBag.RoomNumber = contract.Bed.Room.RoomNumber;
            ViewBag.BlockName = contract.Bed.Room.Block?.BlockName;

            // Lấy danh sách dịch vụ hoạt động, bỏ qua điện/nước vì điện/nước tự động ghi nhận
            var activeUtilities = await _utilityService.GetAllActiveUtilitiesAsync();
            var registrableUtilities = activeUtilities
                .Where(u => !u.UtilityName.ToLower().Contains("điện") && !u.UtilityName.ToLower().Contains("nước"))
                .ToList();
            
            ViewBag.Utilities = registrableUtilities;

            return View(new RegisterUtilityServiceRequestDto());
        }

        /// <summary>
        /// Đăng ký dịch vụ tiện ích mới (POST - Student).
        /// </summary>
        [HttpPost("Create")]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterUtilityServiceRequestDto dto)
        {
            var userId = CurrentUserId;
            if (userId == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                await PopulateCreateViewBagAsync(userId.Value);
                return View(dto);
            }

            try
            {
                var result = await _service.RegisterServiceRequestAsync(userId.Value, dto.UtilityId, dto.Quantity, dto.Notes);
                if (result)
                {
                    TempData["Success"] = "Đăng ký dịch vụ thành công! Yêu cầu của bạn đang chờ phê duyệt.";
                    return RedirectToAction(nameof(MyRequests));
                }
                
                ModelState.AddModelError(string.Empty, "Đăng ký dịch vụ thất bại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi sinh viên {UserId} đăng ký dịch vụ {UtilityId}", userId, dto.UtilityId);
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            await PopulateCreateViewBagAsync(userId.Value);
            return View(dto);
        }

        /// <summary>
        /// Phê duyệt yêu cầu dịch vụ (POST / AJAX - Admin).
        /// </summary>
        [HttpPost("ApproveRequest/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> ApproveRequest(Guid id)
        {
            var userId = CurrentUserId;
            if (userId == null)
            {
                return Json(new { success = false, message = "Người dùng chưa đăng nhập." });
            }

            Logger.LogInformation("Quản lý {UserId} phê duyệt yêu cầu đăng ký dịch vụ {Id}", userId, id);
            var result = await _service.ApproveServiceRequestAsync(id, userId.Value);
            return Json(new { success = result, message = result ? "Phê duyệt đăng ký dịch vụ thành công." : "Phê duyệt thất bại." });
        }

        /// <summary>
        /// Từ chối yêu cầu dịch vụ (POST / AJAX - Admin).
        /// </summary>
        [HttpPost("RejectRequest/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> RejectRequest(Guid id)
        {
            var userId = CurrentUserId;
            if (userId == null)
            {
                return Json(new { success = false, message = "Người dùng chưa đăng nhập." });
            }

            Logger.LogInformation("Quản lý {UserId} từ chối yêu cầu đăng ký dịch vụ {Id}", userId, id);
            var result = await _service.RejectServiceRequestAsync(id, userId.Value);
            return Json(new { success = result, message = result ? "Từ chối đăng ký dịch vụ thành công." : "Từ chối thất bại." });
        }

        private async Task PopulateCreateViewBagAsync(Guid userId)
        {
            var contract = await _contractRepository.GetQuery()
                .Include(c => c.Bed)
                    .ThenInclude(b => b.Room)
                        .ThenInclude(r => r.Block)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == DormitoryManagement.Domain.Enums.ContractStatus.Active);

            if (contract != null && contract.Bed?.Room != null)
            {
                ViewBag.RoomNumber = contract.Bed.Room.RoomNumber;
                ViewBag.BlockName = contract.Bed.Room.Block?.BlockName;
            }

            var activeUtilities = await _utilityService.GetAllActiveUtilitiesAsync();
            var registrableUtilities = activeUtilities
                .Where(u => !u.UtilityName.ToLower().Contains("điện") && !u.UtilityName.ToLower().Contains("nước"))
                .ToList();
            
            ViewBag.Utilities = registrableUtilities;
        }
    }
}
