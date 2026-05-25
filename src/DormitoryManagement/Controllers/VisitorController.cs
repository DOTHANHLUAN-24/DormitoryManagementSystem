using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    public class VisitorController(
        IContractService contractService,
        IUserRepository userRepository,
        IVisitorLogRepository visitorLogRepository,
        IUnitOfWork unitOfWork
    ) : BaseController
    {
        private readonly IContractService _contractService = contractService;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IVisitorLogRepository _visitorLogRepository = visitorLogRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("")]
        public IActionResult Index()
        {
            if (User.IsInRole("Student"))
            {
                return RedirectToAction(nameof(Request));
            }
            return View();
        }

        [HttpGet("GetList")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> GetList()
        {
            var visitors = await _visitorLogRepository.GetQuery()
                .Include(v => v.Host)
                .Where(v => !v.IsDeleted)
                .OrderByDescending(v => v.CreatedDate)
                .ToListAsync();

            var hostIds = visitors.Select(v => v.HostId).Distinct().ToList();
            var hostContracts = new Dictionary<Guid, string>();
            foreach (var hostId in hostIds)
            {
                var contracts = await _contractService.GetByUserIdAsync(hostId);
                var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);
                var roomNumber = activeContract?.Bed?.Room?.RoomNumber ?? "Chưa xếp phòng";
                var blockName = activeContract?.Bed?.Room?.Block?.BlockName ?? "";
                var room = string.IsNullOrEmpty(blockName) ? roomNumber : $"{roomNumber} - {blockName}";
                hostContracts[hostId] = room;
            }

            var list = visitors.Select(v => new VisitorViewModel
            {
                Id = v.Id.ToString(),
                VisitorName = v.VisitorName,
                IdentityCard = v.IdNumber,
                PhoneNumber = v.PhoneNumber,
                Relationship = v.Relationship,
                Purpose = v.Purpose,
                HostId = v.HostId,
                StudentName = v.Host?.FullName ?? "Sinh viên",
                Room = hostContracts.GetValueOrDefault(v.HostId, "Chưa xếp phòng"),
                CheckIn = v.CheckInTime,
                CheckOut = v.CheckOutTime,
                IsCheckedOut = v.IsCheckedOut,
                Status = v.Status,
                CreatedDate = v.CreatedDate
            }).ToList();

            return Json(list);
        }

        [HttpGet("GetVisitor/{id}")]
        public async Task<IActionResult> GetVisitor(string id)
        {
            if (!Guid.TryParse(id, out var visitorId)) return BadRequest();

            var v = await _visitorLogRepository.GetQuery()
                .Include(v => v.Host)
                .FirstOrDefaultAsync(v => v.Id == visitorId && !v.IsDeleted);

            if (v == null) return NotFound();

            var contracts = await _contractService.GetByUserIdAsync(v.HostId);
            var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);
            var roomNumber = activeContract?.Bed?.Room?.RoomNumber ?? "Chưa xếp phòng";
            var blockName = activeContract?.Bed?.Room?.Block?.BlockName ?? "";
            var room = string.IsNullOrEmpty(blockName) ? roomNumber : $"{roomNumber} - {blockName}";

            var model = new VisitorViewModel
            {
                Id = v.Id.ToString(),
                VisitorName = v.VisitorName,
                IdentityCard = v.IdNumber,
                PhoneNumber = v.PhoneNumber,
                Relationship = v.Relationship,
                Purpose = v.Purpose,
                HostId = v.HostId,
                StudentName = v.Host?.FullName ?? "Sinh viên",
                Room = room,
                CheckIn = v.CheckInTime,
                CheckOut = v.CheckOutTime,
                IsCheckedOut = v.IsCheckedOut,
                Status = v.Status,
                CreatedDate = v.CreatedDate
            };

            return Json(model);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string visitorName, string identityCard, string phoneNumber, string purpose, string studentName, string room, DateTime checkIn, DateTime checkOut)
        {
            var users = await _userRepository.GetAllAsync();
            var host = users.FirstOrDefault(u => u.FullName.Contains(studentName, StringComparison.OrdinalIgnoreCase));
            if (host == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sinh viên tương ứng." });
            }

            var newLog = new VisitorLog
            {
                VisitorName = visitorName,
                IdNumber = identityCard,
                PhoneNumber = phoneNumber ?? "",
                Relationship = "Khách",
                Purpose = purpose ?? "",
                HostId = host.Id,
                CheckInTime = checkIn,
                CheckOutTime = checkOut,
                IsCheckedOut = false,
                Status = "Đang ở trong",
                CreatedDate = DateTime.Now
            };

            await _visitorLogRepository.AddAsync(newLog);
            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true, id = newLog.Id.ToString() });
        }

        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public IActionResult Edit(string id)
        {
            ViewData["VisitorId"] = id;
            return View();
        }

        [HttpPost("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string visitorName, string identityCard, string phoneNumber, string purpose, string studentName, string room, DateTime checkIn, DateTime? checkOut, string status)
        {
            if (!Guid.TryParse(id, out var visitorId)) return BadRequest();

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null) return NotFound();

            var users = await _userRepository.GetAllAsync();
            var host = users.FirstOrDefault(u => u.FullName.Contains(studentName, StringComparison.OrdinalIgnoreCase));
            if (host != null)
            {
                visitor.HostId = host.Id;
            }

            visitor.VisitorName = visitorName;
            visitor.IdNumber = identityCard;
            visitor.PhoneNumber = phoneNumber ?? "";
            visitor.Purpose = purpose ?? "";
            visitor.CheckInTime = checkIn;
            visitor.CheckOutTime = checkOut;
            visitor.Status = status;
            visitor.IsCheckedOut = status == "Đã rời đi";

            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost("Approve/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(string id)
        {
            if (!Guid.TryParse(id, out var visitorId)) return BadRequest();

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null) return NotFound();

            visitor.Status = "Đang ở trong";
            visitor.CheckInTime = DateTime.Now;

            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost("Reject/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(string id)
        {
            if (!Guid.TryParse(id, out var visitorId)) return BadRequest();

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null) return NotFound();

            visitor.Status = "Từ chối";

            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost("CheckOut/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(string id)
        {
            if (!Guid.TryParse(id, out var visitorId)) return BadRequest();

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null) return NotFound();

            visitor.IsCheckedOut = true;
            visitor.Status = "Đã rời đi";
            visitor.CheckOutTime = DateTime.Now;

            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Guid.TryParse(id, out var visitorId)) return BadRequest();

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null) return NotFound();

            await _visitorLogRepository.DeleteAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true });
        }

        // --- Student Actions ---

        [HttpGet("Request")]
        [Authorize(Roles = "Student")]
        public new async Task<IActionResult> Request()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var visitors = await _visitorLogRepository.GetQuery()
                .Include(v => v.Host)
                .Where(v => v.HostId == userId && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedDate)
                .ToListAsync();

            var contracts = await _contractService.GetByUserIdAsync(userId);
            var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);
            var roomNumber = activeContract?.Bed?.Room?.RoomNumber ?? "Chưa xếp phòng";
            var blockName = activeContract?.Bed?.Room?.Block?.BlockName ?? "";
            var room = string.IsNullOrEmpty(blockName) ? roomNumber : $"{roomNumber} - {blockName}";

            var list = visitors.Select(v => new VisitorViewModel
            {
                Id = v.Id.ToString(),
                VisitorName = v.VisitorName,
                IdentityCard = v.IdNumber,
                PhoneNumber = v.PhoneNumber,
                Relationship = v.Relationship,
                Purpose = v.Purpose,
                HostId = v.HostId,
                StudentName = v.Host?.FullName ?? "Sinh viên",
                Room = room,
                CheckIn = v.CheckInTime,
                CheckOut = v.CheckOutTime,
                IsCheckedOut = v.IsCheckedOut,
                Status = v.Status,
                CreatedDate = v.CreatedDate
            }).ToList();

            return View(list);
        }

        [HttpGet("CreateRequest")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> CreateRequest()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            var contracts = await _contractService.GetByUserIdAsync(userId);
            var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);

            var roomNumber = activeContract?.Bed?.Room?.RoomNumber ?? "Chưa xếp phòng";
            var blockName = activeContract?.Bed?.Room?.Block?.BlockName ?? "";

            ViewBag.StudentName = user?.FullName ?? "";
            ViewBag.RoomName = string.IsNullOrEmpty(blockName) ? roomNumber : $"{roomNumber} - {blockName}";

            return View();
        }

        [HttpPost("CreateRequest")]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(string visitorName, string identityCard, string phoneNumber, string relationship, string purpose, DateTime checkIn, DateTime checkOut)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Json(new { success = false, message = "Phiên đăng nhập đã hết hạn." });
            }

            var newRequest = new VisitorLog
            {
                VisitorName = visitorName,
                IdNumber = identityCard,
                PhoneNumber = phoneNumber ?? "",
                Relationship = relationship ?? "",
                Purpose = purpose ?? "",
                HostId = userId,
                CheckInTime = checkIn,
                CheckOutTime = checkOut,
                IsCheckedOut = false,
                Status = "Chờ duyệt",
                CreatedDate = DateTime.Now
            };

            await _visitorLogRepository.AddAsync(newRequest);
            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}