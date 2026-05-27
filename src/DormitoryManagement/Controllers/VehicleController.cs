using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Application.Dtos.Requests.Vehicles;
using AutoMapper;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    public class VehicleController(IVehicleService vehicleService, IUserService userService, IMapper mapper) : BaseController
    {
        private readonly IVehicleService _vehicleService = vehicleService;
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        // GET: Vehicle
        public async Task<IActionResult> Index(int page = 1, string search = "", string type = "")
        {
            Logger.LogInformation("Đang truy cập trang danh sách phương tiện. Trang: {Page}, Tìm kiếm: {Search}, Loại: {Type}", page, search, type);

            var result = await _vehicleService.GetPagedVehiclesAsync(page, PageSize, search, type);

            ViewBag.Search = search;
            ViewBag.TypeFilter = type;

            return View(result);
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang đăng ký phương tiện mới.");
            return View(new VehicleRequestDto());
        }

        // POST: Vehicle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleRequestDto vehicleDto)
        {
            Logger.LogInformation("Đang xử lý yêu cầu đăng ký phương tiện mới.");
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

        // GET: Vehicle/Edit/{id}
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

        // POST: Vehicle/Edit/{id}
        [HttpPost]
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

        // API phục vụ tìm kiếm cho Select2
        [HttpGet]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu thay đổi trạng thái hoạt động phương tiện ID: {Id}", id);
            try
            {
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

        // AJAX POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa phương tiện ID: {Id}", id);
            try
            {
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
    }
}
