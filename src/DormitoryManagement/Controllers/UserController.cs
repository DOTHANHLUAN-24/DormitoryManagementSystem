using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Route("User")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            int pageSize = 5;
            // result trả về kiểu PagedResult<UserResponseDto>
            var result = await _userService.GetActiveUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet]
        [Route("Banned")]
        public async Task<IActionResult> BannedList(int page = 1, string search = "")
        {
            int pageSize = 5;
            var result = await _userService.GetBannedUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet]
        [Route("RecycleBin")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            int pageSize = 5;
            var result = await _userService.GetDeletedUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            // Truyền DTO rỗng để View render form chính xác
            return View(new UserRequestDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRequestDto userDto)
        {
            if (!ModelState.IsValid)
            {
                // MÔI TRƯỜNG DEV: In lỗi ra Console để bạn biết chính xác trường nào đang bị thiếu/sai
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine(">>> LỖI TẠI FORM CREATE USER: " + string.Join(", ", errors));
                return View(userDto);
            }

            // Kiểm tra trùng username trước
            if (await _userService.IsUsernameExistAsync(userDto.UserName))
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại.");
                return View(userDto);
            }

            try
            {
                await _userService.CreateUserAsync(userDto);
                TempData["Success"] = "Thêm người dùng thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Thêm người dùng thất bại: " + ex.Message);
                return View(userDto);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var userResponse = await _userService.GetUserByIdAsync(id);
            if (userResponse == null) return NotFound();

            // Mapping phải đảm bảo: userRequest.Role = userResponse.Role;
            var userRequest = _mapper.Map<UserRequestDto>(userResponse);

            return View(userRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserRequestDto userRequest)
        {
            // Kiểm tra xem dữ liệu có vào đến đây không bằng cách đặt Breakpoint
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserProfileAsync(id, userRequest);
                return RedirectToAction(nameof(Index));
            }
            // Nếu không vào được if, trang sẽ load lại và hiện lỗi nhờ asp-validation-summary="All"
            return View(userRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Kiểm tra token được gửi từ AJAX
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)

        {
            if (id == Guid.Empty) return Json(new { success = false, message = "ID không hợp lệ" });

            try
            {
                var result = await _userService.DeactivateUserAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Đã chuyển vào thùng rác thành công." });
                }
                return Json(new { success = false, message = "Không thể thực hiện xóa mềm." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Restore/{id}")]
        public async Task<IActionResult> Restore(Guid id)
        {
            try
            {
                await _userService.RestoreUserAsync(id);
                return Json(new { success = true, message = "Khôi phục người dùng thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            try
            {
                await _userService.ToggleUserStatusAsync(id);
                return Json(new { success = true, message = "Cập nhật trạng thái thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Trong UserController.cs
        [HttpPost]
        [Route("DeletePermanently/{id}")]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            try
            {
                var result = await _userService.DeletePermanentlyAsync(id);
                return Json(new { success = true, message = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                // Lấy thông báo lỗi chi tiết nhất từ SQL
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { success = false, message = "Lỗi hệ thống: " + message });
            }
        }

        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> Profile()
        {
            // Lấy ID của người dùng đang đăng nhập từ Claims trong JWT Token
            var userIdString = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value
                            ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Kiểm tra token bảo mật
        [Route("ToggleLock/{id}")]
        public async Task<IActionResult> ToggleLock(Guid id)
        {
            // Gọi hàm ToggleUserStatusAsync mà bạn đã viết trong UserService
            var result = await _userService.ToggleUserStatusAsync(id);

            if (result)
            {
                return Json(new { success = true, message = "Đã thay đổi trạng thái tài khoản thành công." });
            }
            return Json(new { success = false, message = "Không tìm thấy người dùng hoặc lỗi hệ thống." });
        }
    }
}