using DormitoryManagement.Models.DTOs;
using DormitoryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Tạo người dùng mới
        /// </summary>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(createUserDTO);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng
        /// </summary>
        [HttpGet("all")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Lấy danh sách người dùng hoạt động
        /// </summary>
        [HttpGet("active")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetActiveUsers()
        {
            var users = await _userService.GetActiveUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Lấy chi tiết người dùng
        /// </summary>
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var user = await _userService.GetUserDetailsAsync(userId);
            
            if (user == null)
                return NotFound(new { message = "Không tìm thấy người dùng" });

            return Ok(user);
        }

        /// <summary>
        /// Tìm kiếm người dùng
        /// </summary>
        [HttpGet("search")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> SearchUsers([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest(new { message = "Vui lòng nhập từ khóa tìm kiếm" });

            var users = await _userService.SearchUsersAsync(searchTerm);
            return Ok(users);
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        [HttpPut("update")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateUserAsync(updateUserDTO);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Xóa người dùng (xóa mềm)
        /// </summary>
        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Khôi phục người dùng bị xóa
        /// </summary>
        [HttpPost("{userId}/restore")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RestoreUser(string userId)
        {
            var result = await _userService.RestoreUserAsync(userId);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.ChangePasswordAsync(changePasswordDTO);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Đặt lại mật khẩu
        /// </summary>
        [HttpPost("{userId}/reset-password")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetPassword(string userId)
        {
            var result = await _userService.ResetPasswordAsync(userId);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message, newPassword = result.NewPassword });
        }

        /// <summary>
        /// Khóa tài khoản người dùng
        /// </summary>
        [HttpPost("{userId}/lock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LockUser(string userId, [FromQuery] int lockoutMinutes = 30)
        {
            var lockoutDuration = TimeSpan.FromMinutes(lockoutMinutes);
            var result = await _userService.LockUserAsync(userId, lockoutDuration);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Mở khóa tài khoản người dùng
        /// </summary>
        [HttpPost("{userId}/unlock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnlockUser(string userId)
        {
            var result = await _userService.UnlockUserAsync(userId);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Gán roles cho người dùng
        /// </summary>
        [HttpPost("{userId}/assign-roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRolesToUser(string userId, [FromBody] List<string> roles)
        {
            if (!roles.Any())
                return BadRequest(new { message = "Vui lòng cung cấp ít nhất một role" });

            var result = await _userService.AssignRolesToUserAsync(userId, roles);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Lấy roles của người dùng
        /// </summary>
        [HttpGet("{userId}/roles")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var roles = await _userService.GetUserRolesAsync(userId);
            return Ok(new { roles = roles.ToList() });
        }

        /// <summary>
        /// Kiểm tra tên người dùng đã tồn tại
        /// </summary>
        [HttpGet("check-username/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckUserNameExists(string userName)
        {
            var exists = await _userService.UserNameExistsAsync(userName);
            return Ok(new { exists });
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại
        /// </summary>
        [HttpGet("check-email/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var exists = await _userService.EmailExistsAsync(email);
            return Ok(new { exists });
        }
    }
}
