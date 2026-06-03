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
        [HttpGet("Index")]
        public IActionResult Index()
        {
            Logger.LogInformation("Đang truy cập trang quản lý khách ghé thăm.");
            if (User.IsInRole("Student"))
            {
                Logger.LogInformation("Người dùng là sinh viên, chuyển hướng đến trang gửi yêu cầu.");
                return RedirectToAction(nameof(Request));
            }
            return View();
        }

        [HttpGet("GetList")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> GetList()
        {
            Logger.LogInformation("Đang truy cập API GetList lấy danh sách khách ghé thăm.");
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

            var list = visitors.Select(v =>
            {
                var status = v.Status;
                if (status == "Đang ở trong" && v.CheckOutTime.HasValue && DateTime.Now > v.CheckOutTime.Value)
                {
                    status = "Quá giờ";
                }
                return new VisitorViewModel
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
                    Status = status,
                    CreatedDate = v.CreatedDate
                };
            }).ToList();

            return Json(list);
        }

        [HttpGet("GetVisitor/{id}")]
        public async Task<IActionResult> GetVisitor(string id)
        {
            Logger.LogInformation("Đang lấy thông tin chi tiết khách ghé thăm ID: {Id}", id);
            if (!Guid.TryParse(id, out var visitorId))
            {
                Logger.LogWarning("ID khách ghé thăm không hợp lệ: {Id}", id);
                return BadRequest();
            }

            var v = await _visitorLogRepository.GetQuery()
                .Include(v => v.Host)
                .FirstOrDefaultAsync(v => v.Id == visitorId && !v.IsDeleted);

            if (v == null)
            {
                Logger.LogWarning("Không tìm thấy khách ghé thăm ID: {Id}", id);
                return NotFound();
            }

            var contracts = await _contractService.GetByUserIdAsync(v.HostId);
            var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);
            var roomNumber = activeContract?.Bed?.Room?.RoomNumber ?? "Chưa xếp phòng";
            var blockName = activeContract?.Bed?.Room?.Block?.BlockName ?? "";
            var room = string.IsNullOrEmpty(blockName) ? roomNumber : $"{roomNumber} - {blockName}";

            var status = v.Status;
            if (status == "Đang ở trong" && v.CheckOutTime.HasValue && DateTime.Now > v.CheckOutTime.Value)
            {
                status = "Quá giờ";
            }

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
                Status = status,
                CreatedDate = v.CreatedDate
            };

            return Json(model);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang đăng ký khách ghé thăm.");
            return View();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string visitorName, string identityCard, string phoneNumber, string relationship, string purpose, string studentName, string room, DateTime checkIn, DateTime checkOut)
        {
            Logger.LogInformation("Đang xử lý đăng ký khách ghé thăm mới: '{VisitorName}' cho sinh viên '{StudentName}'", visitorName, studentName);
            if (checkIn >= checkOut)
            {
                Logger.LogWarning("Đăng ký khách ghé thăm thất bại: Thời gian ra dự kiến phải sau thời gian vào.");
                return Json(new { success = false, message = "Thời gian ra dự kiến phải sau thời gian vào." });
            }

            var users = await _userRepository.GetAllAsync();
            var matchedUsers = users.Where(u => u.FullName.Contains(studentName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!matchedUsers.Any())
            {
                Logger.LogWarning("Đăng ký khách ghé thăm thất bại: Không tìm thấy sinh viên có tên '{StudentName}'", studentName);
                return Json(new { success = false, message = "Không tìm thấy sinh viên tương ứng." });
            }

            User? host = null;
            if (matchedUsers.Count == 1)
            {
                host = matchedUsers.First();
            }
            else
            {
                foreach (var user in matchedUsers)
                {
                    var contracts = await _contractService.GetByUserIdAsync(user.Id);
                    var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);
                    if (activeContract != null)
                    {
                        var roomNumber = activeContract.Bed?.Room?.RoomNumber ?? "";
                        var blockName = activeContract.Bed?.Room?.Block?.BlockName ?? "";
                        var userRoom = string.IsNullOrEmpty(blockName) ? roomNumber : $"{roomNumber} - {blockName}";
                        if (userRoom.Contains(room, StringComparison.OrdinalIgnoreCase) || room.Contains(userRoom, StringComparison.OrdinalIgnoreCase))
                        {
                            host = user;
                            break;
                        }
                    }
                }
                host ??= matchedUsers.First();
            }

            var newLog = new VisitorLog
            {
                VisitorName = visitorName,
                IdNumber = identityCard,
                PhoneNumber = phoneNumber ?? "",
                Relationship = relationship ?? "Khách",
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

            Logger.LogInformation("Đăng ký khách ghé thăm thành công. ID: {Id}", newLog.Id);
            return Json(new { success = true, id = newLog.Id.ToString() });
        }

        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public IActionResult Edit(string id)
        {
            Logger.LogInformation("Đang truy cập trang chỉnh sửa khách ghé thăm ID: {Id}", id);
            ViewData["VisitorId"] = id;
            return View();
        }

        [HttpPost("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string visitorName, string identityCard, string phoneNumber, string relationship, string purpose, string studentName, string room, DateTime checkIn, DateTime? checkOut, string status)
        {
            Logger.LogInformation("Đang xử lý cập nhật thông tin khách ghé thăm ID: {Id}", id);
            if (!Guid.TryParse(id, out var visitorId))
            {
                Logger.LogWarning("ID khách ghé thăm để cập nhật không hợp lệ: {Id}", id);
                return BadRequest();
            }

            if (checkOut.HasValue && checkIn >= checkOut.Value)
            {
                Logger.LogWarning("Cập nhật thất bại: Thời gian ra dự kiến phải sau thời gian vào.");
                return Json(new { success = false, message = "Thời gian ra dự kiến phải sau thời gian vào." });
            }

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null)
            {
                Logger.LogWarning("Không tìm thấy thông tin khách ghé thăm ID: {Id} để cập nhật.", id);
                return NotFound();
            }

            var users = await _userRepository.GetAllAsync();
            var matchedUsers = users.Where(u => u.FullName.Contains(studentName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedUsers.Any())
            {
                User? host = null;
                if (matchedUsers.Count == 1)
                {
                    host = matchedUsers.First();
                }
                else
                {
                    foreach (var user in matchedUsers)
                    {
                        var contracts = await _contractService.GetByUserIdAsync(user.Id);
                        var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);
                        if (activeContract != null)
                        {
                            var roomNumber = activeContract.Bed?.Room?.RoomNumber ?? "";
                            var blockName = activeContract.Bed?.Room?.Block?.BlockName ?? "";
                            var userRoom = string.IsNullOrEmpty(blockName) ? roomNumber : $"{roomNumber} - {blockName}";
                            if (userRoom.Contains(room, StringComparison.OrdinalIgnoreCase) || room.Contains(userRoom, StringComparison.OrdinalIgnoreCase))
                            {
                                host = user;
                                break;
                            }
                        }
                    }
                    host ??= matchedUsers.First();
                }
                visitor.HostId = host.Id;
            }

            visitor.VisitorName = visitorName;
            visitor.IdNumber = identityCard;
            visitor.PhoneNumber = phoneNumber ?? "";
            visitor.Relationship = relationship ?? "";
            visitor.Purpose = purpose ?? "";
            visitor.CheckInTime = checkIn;
            visitor.CheckOutTime = checkOut;
            visitor.Status = status;
            visitor.IsCheckedOut = status == "Đã rời đi";

            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            Logger.LogInformation("Cập nhật thông tin khách ghé thăm ID: {Id} thành công.", id);
            return Json(new { success = true });
        }

        [HttpPost("Approve/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(string id)
        {
            Logger.LogInformation("Đang thực hiện duyệt cho phép khách ghé thăm ID: {Id} vào KTX", id);
            if (!Guid.TryParse(id, out var visitorId))
            {
                Logger.LogWarning("ID khách để duyệt không hợp lệ: {Id}", id);
                return BadRequest();
            }

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null)
            {
                Logger.LogWarning("Không tìm thấy khách ghé thăm ID: {Id} để duyệt.", id);
                return NotFound();
            }

            visitor.Status = "Đang ở trong";
            visitor.CheckInTime = DateTime.Now;

            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            Logger.LogInformation("Đã duyệt khách ghé thăm ID: {Id} vào KTX.", id);
            return Json(new { success = true });
        }

        [HttpPost("Reject/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(string id)
        {
            Logger.LogInformation("Đang từ chối yêu cầu vào KTX của khách ghé thăm ID: {Id}", id);
            if (!Guid.TryParse(id, out var visitorId))
            {
                Logger.LogWarning("ID khách để từ chối không hợp lệ: {Id}", id);
                return BadRequest();
            }

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null)
            {
                Logger.LogWarning("Không tìm thấy khách ghé thăm ID: {Id} để từ chối.", id);
                return NotFound();
            }

            visitor.Status = "Từ chối";

            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            Logger.LogInformation("Đã từ chối khách ghé thăm ID: {Id}.", id);
            return Json(new { success = true });
        }

        [HttpPost("CheckOut/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(string id)
        {
            Logger.LogInformation("Đang ghi nhận khách rời đi (Check-out) ID: {Id}", id);
            if (!Guid.TryParse(id, out var visitorId))
            {
                Logger.LogWarning("ID khách check-out không hợp lệ: {Id}", id);
                return BadRequest();
            }

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null)
            {
                Logger.LogWarning("Không tìm thấy khách ghé thăm ID: {Id} để check-out.", id);
                return NotFound();
            }

            visitor.IsCheckedOut = true;
            visitor.Status = "Đã rời đi";
            visitor.CheckOutTime = DateTime.Now;

            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            Logger.LogInformation("Ghi nhận check-out khách ghé thăm ID: {Id} thành công.", id);
            return Json(new { success = true });
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            Logger.LogInformation("Đang yêu cầu xóa log khách ghé thăm ID: {Id}", id);
            if (!Guid.TryParse(id, out var visitorId))
            {
                Logger.LogWarning("ID khách để xóa không hợp lệ: {Id}", id);
                return BadRequest();
            }

            var visitor = await _visitorLogRepository.GetByIdAsync(visitorId);
            if (visitor == null)
            {
                Logger.LogWarning("Không tìm thấy khách ghé thăm ID: {Id} để xóa.", id);
                return NotFound();
            }

            await _visitorLogRepository.DeleteAsync(visitor);
            await _unitOfWork.SaveChangesAsync();

            Logger.LogInformation("Đã xóa log khách ghé thăm ID: {Id} thành công.", id);
            return Json(new { success = true });
        }

        // --- Student Actions ---

        [HttpGet("Request")]
        [Authorize(Roles = "Student")]
        public new async Task<IActionResult> Request()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Logger.LogInformation("Sinh viên ID {UserIdString} truy cập trang danh sách yêu cầu đăng ký khách ghé thăm.", userIdString);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                Logger.LogWarning("Sinh viên chưa đăng nhập, chuyển hướng về đăng nhập.");
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

            var list = visitors.Select(v =>
            {
                var status = v.Status;
                if (status == "Đang ở trong" && v.CheckOutTime.HasValue && DateTime.Now > v.CheckOutTime.Value)
                {
                    status = "Quá giờ";
                }
                return new VisitorViewModel
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
                    Status = status,
                    CreatedDate = v.CreatedDate
                };
            }).ToList();

            return View(list);
        }

        [HttpGet("CreateRequest")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> CreateRequest()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Logger.LogInformation("Sinh viên ID {UserIdString} đang truy cập form tạo yêu cầu đăng ký khách ghé thăm.", userIdString);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                Logger.LogWarning("Chưa đăng nhập, chuyển hướng về trang đăng nhập.");
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
            Logger.LogInformation("Sinh viên ID {UserIdString} gửi yêu cầu đăng ký khách ghé thăm '{VisitorName}'", userIdString, visitorName);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                Logger.LogWarning("Yêu cầu thất bại do phiên đăng nhập hết hạn.");
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

            Logger.LogInformation("Gửi yêu cầu đăng ký khách ghé thăm thành công. ID: {Id}", newRequest.Id);
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpGet("RecycleBin")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang truy cập thùng rác khách ghé thăm trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            
            var query = _visitorLogRepository.GetQuery()
                .Include(v => v.Host)
                .Where(v => v.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                var lowerSearch = search.ToLower().Trim();
                query = query.Where(v => v.VisitorName.ToLower().Contains(lowerSearch) || v.IdNumber.Contains(lowerSearch));
            }

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(v => v.CreatedDate)
                                  .Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            var list = items.Select(v =>
            {
                var status = v.Status;
                if (status == "Đang ở trong" && v.CheckOutTime.HasValue && DateTime.Now > v.CheckOutTime.Value)
                {
                    status = "Quá giờ";
                }
                return new VisitorViewModel
                {
                    Id = v.Id.ToString(),
                    VisitorName = v.VisitorName,
                    IdentityCard = v.IdNumber,
                    PhoneNumber = v.PhoneNumber,
                    Relationship = v.Relationship,
                    Purpose = v.Purpose,
                    HostId = v.HostId,
                    StudentName = v.Host?.FullName ?? "Sinh viên",
                    CheckIn = v.CheckInTime,
                    CheckOut = v.CheckOutTime,
                    IsCheckedOut = v.IsCheckedOut,
                    Status = status,
                    CreatedDate = v.CreatedDate
                };
            }).ToList();

            var pagedResult = new DormitoryManagement.Domain.Common.PagedResult<VisitorViewModel>(list, totalCount, page, pageSize);

            ViewBag.Search = search;
            return View(pagedResult);
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục log khách ghé thăm ID: {Id}", id);
            var visitor = await _visitorLogRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (visitor == null || !visitor.IsDeleted) return Json(new { success = false, message = "Không tìm thấy khách hoặc không ở trạng thái đã xóa." });

            visitor.IsDeleted = false;
            await _visitorLogRepository.UpdateAsync(visitor);
            await _unitOfWork.SaveChangesAsync();
            return Json(new { success = true, message = "Khôi phục khách ghé thăm thành công!" });
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn log khách ghé thăm ID: {Id}", id);
            var visitor = await _visitorLogRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (visitor == null) return Json(new { success = false, message = "Không tìm thấy khách ghé thăm." });

            await _visitorLogRepository.DeleteAsync(visitor, isSoftDelete: false);
            await _unitOfWork.SaveChangesAsync();
            return Json(new { success = true, message = "Đã xóa vĩnh viễn khách ghé thăm khỏi cơ sở dữ liệu." });
        }
    }
}