using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Application.Dtos.Requests.Vehicles;
using AutoMapper;

namespace DormitoryManagement.Controllers
{
    public class VehicleController(IVehicleService vehicleService, IUserService userService, IMapper mapper) : BaseController
    {
        private readonly IVehicleService _vehicleService = vehicleService;
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(int page = 1, string search = "", string type = "", string status = "")
        {
            Logger.LogInformation("Đang truy cập trang danh sách phương tiện. Trang: {Page}, Tìm kiếm: {Search}, Loại: {Type}, Trạng thái: {Status}", page, search, type, status);

            Guid? ownerId = null;
            if (User.IsInRole("Student"))
            {
                ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
            }

            bool? isActive = null;
            if (string.IsNullOrEmpty(status) && !User.IsInRole("Student"))
            {
                isActive = null;
            }
            else if (User.IsInRole("Student"))
            {
                isActive = true;
            }

            var result = await _vehicleService.GetPagedVehiclesAsync(page, PageSize, search, type, isActive: isActive, isDeleted: false, ownerId: ownerId, status: status);

            ViewBag.Search = search;
            ViewBag.TypeFilter = type;
            ViewBag.StatusFilter = status;

            return View(result);
        }

        [HttpGet("Suspended")]
        public async Task<IActionResult> Suspended(int page = 1, string search = "", string type = "")
        {
            Logger.LogInformation("Đang truy cập trang phương tiện tạm ngưng. Trang: {Page}, Tìm kiếm: {Search}, Loại: {Type}", page, search, type);

            Guid? ownerId = null;
            if (User.IsInRole("Student"))
            {
                ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
            }

            var result = await _vehicleService.GetPagedVehiclesAsync(page, PageSize, search, type, isActive: false, isDeleted: false, ownerId: ownerId, status: "Approved");

            ViewBag.Search = search;
            ViewBag.TypeFilter = type;

            return View(result);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang đăng ký phương tiện mới.");
            var dto = new VehicleRequestDto();
            if (User.IsInRole("Student"))
            {
                dto.OwnerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
            }
            return View(dto);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleRequestDto vehicleDto)
        {
            Logger.LogInformation("Đang xử lý yêu cầu đăng ký phương tiện mới.");
            
            if (User.IsInRole("Student"))
            {
                vehicleDto.OwnerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
                ModelState.Remove("OwnerId");
            }

            if (vehicleDto.VehicleType == "Xe đạp" && string.IsNullOrWhiteSpace(vehicleDto.LicensePlate))
            {
                vehicleDto.LicensePlate = "XĐ-" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
                ModelState.Remove("LicensePlate");
            }

            if (!User.IsInRole("Student"))
            {
                vehicleDto.Status = "Approved";
                vehicleDto.IsActive = true;
            }
            else
            {
                vehicleDto.Status = "Pending";
                vehicleDto.IsActive = false;
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Logger.LogWarning("Dữ liệu đăng ký phương tiện không hợp lệ: {Errors}", string.Join(", ", errors));
                return View(vehicleDto);
            }

            try
            {
                var success = await _vehicleService.CreateVehicleAsync(vehicleDto);
                if (success)
                {
                    TempData["Success"] = "Đăng ký phương tiện mới thành công!";
                    Logger.LogInformation("Đăng ký phương tiện thành công, chuyển hướng về Index.");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Đăng ký phương tiện thất bại. Vui lòng thử lại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi đăng ký phương tiện mới.");
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(vehicleDto);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang truy cập trang chỉnh sửa phương tiện ID: {Id}", id);
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                Logger.LogWarning("Không tìm thấy phương tiện với ID: {Id}", id);
                return NotFound();
            }

            var updateDto = new VehicleUpdateDto
            {
                Id = vehicle.Id,
                VehicleType = vehicle.VehicleType,
                LicensePlate = vehicle.LicensePlate,
                OwnerId = vehicle.OwnerId,
                IsActive = vehicle.IsActive
            };

            ViewBag.VehicleId = vehicle.Id;
            ViewBag.LicensePlate = vehicle.LicensePlate;
            ViewBag.VehicleType = vehicle.VehicleType;
            ViewBag.OwnerName = $"{vehicle.OwnerCode} - {vehicle.OwnerFullName}";
            return View(updateDto);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleUpdateDto vehicleDto)
        {
            Logger.LogInformation("Đang xử lý yêu cầu cập nhật phương tiện ID: {Id}", id);
            if (id != vehicleDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu cập nhật phương tiện không hợp lệ.");
                return View(vehicleDto);
            }

            try
            {
                var success = await _vehicleService.UpdateVehicleAsync(id, vehicleDto);
                if (success)
                {
                    TempData["Success"] = "Cập nhật phương tiện thành công!";
                    Logger.LogInformation("Cập nhật phương tiện thành công, chuyển hướng về Index.");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Cập nhật phương tiện thất bại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi cập nhật phương tiện ID: {Id}.", id);
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            // Reload Owner Name for View in case of error
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle != null)
            {
                ViewBag.VehicleId = vehicle.Id;
                ViewBag.LicensePlate = vehicle.LicensePlate;
                ViewBag.VehicleType = vehicle.VehicleType;
                ViewBag.OwnerName = $"{vehicle.OwnerCode} - {vehicle.OwnerFullName}";
            }
            return View(vehicleDto);
        }

        [HttpGet("SearchUsers")]
        public async Task<IActionResult> SearchUsers(string q)
        {
            Logger.LogInformation("Đang thực hiện tìm kiếm người dùng cho phương tiện với từ khóa: {Query}", q);
            var usersResult = await _userService.GetActiveUsersPagedAsync(1, 20, q);
            var items = usersResult.Items.Select(u => new
            {
                id = u.Id,
                text = $"{u.Code} - {u.FullName}"
            });
            return Json(new { items });
        }

        // AJAX POST: ToggleStatus
        [HttpPost("ToggleStatus/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu thay đổi trạng thái hoạt động phương tiện ID: {Id}", id);
            try
            {
                if (User.IsInRole("Student"))
                {
                    var ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
                    var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                    if (vehicle == null || vehicle.OwnerId != ownerId)
                    {
                        return Json(new { success = false, message = "Bạn không có quyền thay đổi trạng thái phương tiện này." });
                    }
                }

                var result = await _vehicleService.ToggleVehicleStatusAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Đã thay đổi trạng thái hoạt động của phương tiện." });
                }
                return Json(new { success = false, message = "Không tìm thấy phương tiện." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thay đổi trạng thái phương tiện ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("SoftDelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa phương tiện ID: {Id}", id);
            try
            {
                if (User.IsInRole("Student"))
                {
                    var ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
                    var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                    if (vehicle == null || vehicle.OwnerId != ownerId)
                    {
                        return Json(new { success = false, message = "Bạn không có quyền xóa phương tiện này." });
                    }
                }

                var result = await _vehicleService.SoftDeleteVehicleAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Đã xóa phương tiện thành công." });
                }
                return Json(new { success = false, message = "Không tìm thấy phương tiện." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa phương tiện ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpGet("RecycleBin")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang truy cập thùng rác phương tiện trang {Page}, tìm kiếm: '{Search}'", page, search);
            var result = await _vehicleService.GetPagedVehiclesAsync(page, PageSize, search, isDeleted: true);
            ViewBag.Search = search;
            return View(result);
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục phương tiện ID: {Id}", id);
            try
            {
                var success = await _vehicleService.RestoreVehicleAsync(id);
                if (success)
                {
                    return Json(new { success = true, message = "Khôi phục phương tiện thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy phương tiện." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn phương tiện ID: {Id}", id);
            try
            {
                var success = await _vehicleService.DeletePermanentlyAsync(id);
                if (success)
                {
                    return Json(new { success = true, message = "Đã xóa vĩnh viễn phương tiện khỏi cơ sở dữ liệu." });
                }
                return Json(new { success = false, message = "Không tìm thấy phương tiện." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Student")]
        [HttpGet("GetMyVehicles")]
        public async Task<IActionResult> GetMyVehicles()
        {
            Logger.LogInformation("Sinh viên đang truy cập danh sách phương tiện của họ.");
            var ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
            
            var pagedVehicles = await _vehicleService.GetPagedVehiclesAsync(pageIndex: 1, pageSize: 100, searchTerm: null, isActive: null, isDeleted: false, ownerId: ownerId);
            
            var vehicles = pagedVehicles.Items;
            return Json(new { success = true, items = vehicles });
        }

        [Authorize(Roles = "Student")]
        [HttpPost("RegisterVehicle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterVehicle([FromForm] string vehicleType, [FromForm] string? licensePlate)
        {
            Logger.LogInformation("Sinh viên đăng ký phương tiện mới. Loại: {Type}, Biển số: {LicensePlate}", vehicleType, licensePlate);
            
            if (string.IsNullOrWhiteSpace(vehicleType))
            {
                return Json(new { success = false, message = "Loại phương tiện không được để trống." });
            }

            if (vehicleType == "Xe đạp")
            {
                licensePlate = "XĐ-" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            }
            else if (string.IsNullOrWhiteSpace(licensePlate))
            {
                return Json(new { success = false, message = "Biển số xe không được để trống đối với loại xe này." });
            }

            try
            {
                var ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
                
                var requestDto = new VehicleRequestDto
                {
                    VehicleType = vehicleType,
                    LicensePlate = licensePlate,
                    OwnerId = ownerId,
                    Status = "Pending",
                    IsActive = false
                };

                var success = await _vehicleService.CreateVehicleAsync(requestDto);
                if (success)
                {
                    return Json(new { success = true, message = "Gửi yêu cầu đăng ký phương tiện thành công! Vui lòng chờ phê duyệt." });
                }
                return Json(new { success = false, message = "Đăng ký phương tiện thất bại." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi sinh viên đăng ký phương tiện.");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("Approve/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id)
        {
            Logger.LogInformation("Đang phê duyệt phương tiện ID: {Id}", id);
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                if (vehicle == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy phương tiện." });
                }

                var updateDto = new VehicleUpdateDto
                {
                    Id = vehicle.Id,
                    VehicleType = vehicle.VehicleType,
                    LicensePlate = vehicle.LicensePlate,
                    OwnerId = vehicle.OwnerId,
                    IsActive = true,
                    Status = "Approved"
                };

                var success = await _vehicleService.UpdateVehicleAsync(id, updateDto);
                if (success)
                {
                    return Json(new { success = true, message = "Đã phê duyệt phương tiện thành công!" });
                }
                return Json(new { success = false, message = "Phê duyệt thất bại." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi khi phê duyệt phương tiện ID: {Id}", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        [HttpPost("Reject/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(Guid id)
        {
            Logger.LogInformation("Đang từ chối phê duyệt phương tiện ID: {Id}", id);
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                if (vehicle == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy phương tiện." });
                }

                var updateDto = new VehicleUpdateDto
                {
                    Id = vehicle.Id,
                    VehicleType = vehicle.VehicleType,
                    LicensePlate = vehicle.LicensePlate,
                    OwnerId = vehicle.OwnerId,
                    IsActive = false,
                    Status = "Rejected"
                };

                var success = await _vehicleService.UpdateVehicleAsync(id, updateDto);
                if (success)
                {
                    return Json(new { success = true, message = "Đã từ chối phương tiện thành công." });
                }
                return Json(new { success = false, message = "Từ chối phê duyệt thất bại." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi khi từ chối phương tiện ID: {Id}", id);
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
