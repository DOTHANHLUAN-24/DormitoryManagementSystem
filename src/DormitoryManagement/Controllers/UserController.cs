using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
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

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllActiveUsersAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> BannedList()
        {
            var bannedUsers = await _userService.GetAllBanUserAsync();
            return View(bannedUsers);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.CreateUserAsync(user);
                    TempData["Success"] = "Thêm người dùng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Thêm người dùng thất bại", ex.Message);
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, User user)
        {
            if (id != user.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateUserProfileAsync(user);
                    TempData["Success"] = "Cập nhật hồ sơ thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Cập nhật người dùng thất bại", ex.Message);
                }
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                await _userService.DeactivateUserAsync(id);
                return Json(new { success = true, message = "Đã khóa người dùng thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
