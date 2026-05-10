using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // 1. Danh sách người dùng đang hoạt động
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            int pageSize = 5;
            // result trả về kiểu PagedResult<UserResponseDto>
            var result = await _userService.GetActiveUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        // 2. Danh sách người dùng bị khóa (Banned)
        [HttpGet]
        public async Task<IActionResult> BannedList(int page = 1, string search = "")
        {
            int pageSize = 5;
            var result = await _userService.GetBannedUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        // 3. Thùng rác (Danh sách người dùng đã bị xóa mềm)
        [HttpGet]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            int pageSize = 5;
            var result = await _userService.GetDeletedUsersPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        // 4. Chi tiết người dùng
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // 5. Thêm mới - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 6. Thêm mới - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRequestDto userDto)
        {
            if (ModelState.IsValid)
            {
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
                    ModelState.AddModelError("", "Thêm người dùng thất bại: " + ex.Message);
                }
            }
            return View(userDto);
        }

        // 7. Chỉnh sửa - GET
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            // Lưu ý: Nếu view Edit yêu cầu RequestDto, bạn có thể cần map ngược lại 
            // hoặc dùng chính ResponseDto nếu các trường tương ứng.
            return View(user);
        }

        // 8. Chỉnh sửa - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserRequestDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateUserProfileAsync(id, userDto);
                    TempData["Success"] = "Cập nhật hồ sơ thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Cập nhật thất bại: " + ex.Message);
                }
            }
            return View(userDto);
        }

        // 9. Xóa mềm (Gửi qua AJAX)
        [HttpPost]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                await _userService.DeactivateUserAsync(id);
                return Json(new { success = true, message = "Đã chuyển người dùng vào thùng rác." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // 10. Khôi phục người dùng (Gửi qua AJAX hoặc POST)
        [HttpPost]
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

        // 11. Chặn/Bỏ chặn (Toggle Status)
        [HttpPost]
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

        // 12. Xóa vĩnh viễn
        [HttpPost]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            try
            {
                await _userService.DeletePermanentlyAsync(id);
                return Json(new { success = true, message = "Đã xóa vĩnh viễn người dùng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}