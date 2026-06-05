using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Users;
using DormitoryManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class UserController(IUserService userService, IMapper mapper) : BaseController
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách người dùng hoạt động trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            // result trả về kiểu PagedResult<UserResponseDto>
            var result = await _userService.GetActiveUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("Banned")]
        public async Task<IActionResult> BannedList(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách tài khoản bị khóa trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var result = await _userService.GetBannedUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("RecycleBin")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách người dùng đã bị xóa trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var result = await _userService.GetDeletedUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            Logger.LogInformation("Đang xem chi tiết người dùng ID: {Id}", id);
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                Logger.LogWarning("Không tìm thấy người dùng ID: {Id}", id);
                return NotFound();
            }

            return View(user);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang thêm mới người dùng.");
            // Truyền DTO rỗng để View render form chính xác
            return View(new UserRequestDto());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRequestDto userDto)
        {
            Logger.LogInformation("Đang thực hiện tạo người dùng mới: '{UserName}' (Role: {Role})", userDto.UserName, userDto.Role);
            if (!ModelState.IsValid)
            {
                // MÔI TRƯỜNG DEV: In lỗi ra Console để bạn biết chính xác trường nào đang bị thiếu/sai
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Logger.LogWarning("Dữ liệu tạo người dùng không hợp lệ: {Errors}", string.Join(", ", errors));
                return View(userDto);
            }

            // Kiểm tra trùng username trước
            if (await _userService.IsUsernameExistAsync(userDto.UserName))
            {
                Logger.LogWarning("Tạo người dùng thất bại: Tên đăng nhập '{UserName}' đã tồn tại.", userDto.UserName);
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại.");
                return View(userDto);
            }

            try
            {
                await _userService.CreateUserAsync(userDto);
                Logger.LogInformation("Tạo người dùng '{UserName}' thành công.", userDto.UserName);
                TempData["Success"] = "Thêm người dùng thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi tạo người dùng '{UserName}'.", userDto.UserName);
                ModelState.AddModelError(string.Empty, "Thêm người dùng thất bại: " + ex.Message);
                return View(userDto);
            }
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang truy cập trang chỉnh sửa người dùng ID: {Id}", id);
            var userResponse = await _userService.GetUserByIdAsync(id);
            if (userResponse == null)
            {
                Logger.LogWarning("Không tìm thấy người dùng ID: {Id} để chỉnh sửa.", id);
                return NotFound();
            }

            // Map từ Response sang UpdateDto để đổ dữ liệu vào Form
            var updateDto = _mapper.Map<UserUpdateDto>(userResponse);

            return View(updateDto);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserUpdateDto updateDto)
        {
            Logger.LogInformation("Đang xử lý cập nhật thông tin người dùng ID: {Id}", id);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu cập nhật người dùng ID: {Id} không hợp lệ.", id);
                return View(updateDto);
            }

            try
            {
                var result = await _userService.UpdateUserProfileAsync(id, updateDto);
                if (result)
                {
                    Logger.LogInformation("Cập nhật người dùng ID: {Id} thành công.", id);
                    TempData["Success"] = "Cập nhật thành công";
                    return RedirectToAction(nameof(Index));
                }

                Logger.LogWarning("Cập nhật người dùng ID: {Id} thất bại.", id);
                ModelState.AddModelError("", "Cập nhật thất bại.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi cập nhật thông tin người dùng ID: {Id}.", id);
                ModelState.AddModelError("", ex.Message);
            }

            return View(updateDto);
        }

        [HttpPost("Deactivate/{id}")]
        [ValidateAntiForgeryToken] // Kiểm tra token được gửi từ AJAX
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa mềm (vô hiệu hóa) người dùng ID: {Id}", id);
            if (id == Guid.Empty)
            {
                Logger.LogWarning("Yêu cầu vô hiệu hóa thất bại do ID trống.");
                return Json(new { success = false, message = "ID không hợp lệ" });
            }

            try
            {
                var result = await _userService.DeactivateUserAsync(id);
                if (result)
                {
                    Logger.LogInformation("Vô hiệu hóa người dùng ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Đã chuyển vào thùng rác thành công." });
                }
                Logger.LogWarning("Không thể thực hiện xóa mềm cho người dùng ID: {Id}.", id);
                return Json(new { success = false, message = "Không thể thực hiện xóa mềm." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi vô hiệu hóa người dùng ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục người dùng ID: {Id}", id);
            try
            {
                await _userService.RestoreUserAsync(id);
                Logger.LogInformation("Khôi phục người dùng ID: {Id} thành công.", id);
                return Json(new { success = true, message = "Khôi phục người dùng thành công." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi khôi phục người dùng ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu thay đổi trạng thái hoạt động người dùng ID: {Id}", id);
            try
            {
                await _userService.ToggleUserStatusAsync(id);
                Logger.LogInformation("Thay đổi trạng thái hoạt động người dùng ID: {Id} thành công.", id);
                return Json(new { success = true, message = "Cập nhật trạng thái thành công." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thay đổi trạng thái hoạt động người dùng ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("DeletePermanently/{id}")]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn người dùng ID: {Id}", id);
            try
            {
                var result = await _userService.DeletePermanentlyAsync(id);
                Logger.LogInformation("Đã xóa vĩnh viễn người dùng ID: {Id} thành công.", id);
                return Json(new { success = true, message = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa vĩnh viễn người dùng ID: {Id}.", id);
                // Lấy thông báo lỗi chi tiết nhất từ SQL
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { success = false, message = "Lỗi hệ thống: " + message });
            }
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = CurrentUserId;

            if (userId == null)
            {
                Logger.LogWarning("Truy cập hồ sơ cá nhân thất bại do chưa đăng nhập hoặc lỗi Token.");
                return RedirectToAction("Login", "Account");
            }

            Logger.LogInformation("Người dùng có Token ID: {UserIdString} đang truy cập trang hồ sơ cá nhân.", userId.Value.ToString());

            var user = await _userService.GetUserByIdAsync(userId.Value);
            if (user == null)
            {
                Logger.LogWarning("Không tìm thấy người dùng ID: {UserId} để hiển thị hồ sơ.", userId);
                return NotFound();
            }

            return View(user);
        }

        [HttpPost("ToggleLock/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLock(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khóa/mở khóa tài khoản người dùng ID: {Id}", id);
            try
            {
                // Gọi hàm ToggleUserStatusAsync mà bạn đã viết trong UserService
                var result = await _userService.ToggleUserStatusAsync(id);

                if (result)
                {
                    Logger.LogInformation("Đã khóa/mở khóa thành công tài khoản ID: {Id}.", id);
                    return Json(new { success = true, message = "Đã thay đổi trạng thái tài khoản thành công." });
                }
                Logger.LogWarning("Khóa/mở khóa tài khoản ID: {Id} thất bại.", id);
                return Json(new { success = false, message = "Không tìm thấy người dùng hoặc lỗi hệ thống." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thay đổi trạng thái khóa tài khoản ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var userIdString = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value
                            ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            Logger.LogInformation("Người dùng Token ID {UserIdString} truy cập trang tự cập nhật hồ sơ cá nhân.", userIdString);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                Logger.LogWarning("Truy cập trang chỉnh sửa hồ sơ thất bại do chưa đăng nhập.");
                return RedirectToAction("Login", "Account");
            }

            // Lấy thông tin user hiện tại
            var userResponse = await _userService.GetUserByIdAsync(userId);
            if (userResponse == null)
            {
                Logger.LogWarning("Không tìm thấy thông tin tài khoản ID: {UserId} để chỉnh sửa.", userId);
                return NotFound();
            }

            // Map từ Response sang UpdateDto để đổ dữ liệu vào Form
            var updateDto = _mapper.Map<UserUpdateDto>(userResponse);

            return View(updateDto);
        }

        [HttpPost("EditProfile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserUpdateDto updateDto)
        {
            var userIdString = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value
                            ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                Logger.LogWarning("Cập nhật thông tin cá nhân thất bại do chưa đăng nhập.");
                return RedirectToAction("Login", "Account");
            }

            Logger.LogInformation("Người dùng ID {UserId} đang thực hiện tự cập nhật hồ sơ cá nhân.", userId);
            // Gán lại ID cho DTO đề phòng DTO yêu cầu trường Id phải có dữ liệu
            updateDto.Id = userId;

            // Lấy thông tin user hiện tại từ database để giữ nguyên vai trò (Role) tránh bị ghi đè hoặc lỗi validation
            var currentUser = await _userService.GetUserByIdAsync(userId);
            if (currentUser != null)
            {
                updateDto.Role = currentUser.Role;
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Logger.LogWarning("Dữ liệu tự chỉnh sửa hồ sơ không hợp lệ: {Errors}", string.Join(", ", errors));
                return View(updateDto);
            }

            try
            {
                var result = await _userService.UpdateUserProfileAsync(userId, updateDto);

                if (result)
                {
                    Logger.LogInformation("Người dùng ID {UserId} tự cập nhật hồ sơ thành công.", userId);
                    TempData["Success"] = "Cập nhật thông tin cá nhân thành công!";
                    return RedirectToAction(nameof(Profile));
                }

                Logger.LogWarning("Người dùng ID {UserId} tự cập nhật hồ sơ thất bại tại Service.", userId);
                ModelState.AddModelError("", "Cập nhật thất bại. Vui lòng thử lại.");
                return View(updateDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi người dùng ID {UserId} tự cập nhật hồ sơ.", userId);
                ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                return View(updateDto);
            }
        }
    }
}