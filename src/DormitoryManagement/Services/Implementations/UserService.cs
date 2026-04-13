using DormitoryManagement.Data.Entities;
using DormitoryManagement.Models.DTOs;
using DormitoryManagement.Repositories.Interfaces;
using DormitoryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<(bool Success, string Message)> CreateUserAsync(CreateUserDTO userDTO)
        {
            try
            {
                if (await _userRepository.UserExistsAsync(userDTO.UserName))
                    return (false, "Tên người dùng đã tồn tại");

                if (await _userRepository.EmailExistsAsync(userDTO.Email))
                    return (false, "Email đã tồn tại");

                var user = new User(
                    Guid.NewGuid().ToString(),
                    userDTO.UserName,
                    userDTO.FirstName,
                    userDTO.LastName,
                    userDTO.Email,
                    userDTO.PhoneNumber
                );

                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError($"Tạo người dùng {userDTO.UserName} thất bại: {errors}");
                    return (false, $"Tạo người dùng thất bại: {errors}");
                }

                if (userDTO.Roles.Any())
                {
                    var validRoles = new List<string>();
                    foreach (var role in userDTO.Roles)
                    {
                        if (await _roleManager.RoleExistsAsync(role))
                            validRoles.Add(role);
                    }

                    if (validRoles.Any())
                    {
                        var roleResult = await _userManager.AddToRolesAsync(user, validRoles);
                        if (!roleResult.Succeeded)
                        {
                            _logger.LogError($"Thêm role cho người dùng {userDTO.UserName} thất bại");
                        }
                    }
                }

                await _userRepository.AddAsync(user);
                _logger.LogInformation($"Tạo người dùng {userDTO.UserName} thành công");

                return (true, "Tạo người dùng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi tạo người dùng: {ex.Message}");
                return (false, "Có lỗi xảy ra khi tạo người dùng");
            }
        }

        public async Task<IEnumerable<UserListDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return await MapUsersToListDTOAsync(users);
        }

        public async Task<IEnumerable<UserListDTO>> GetActiveUsersAsync()
        {
            var users = await _userRepository.GetActiveUsersAsync();
            return await MapUsersToListDTOAsync(users);
        }

        public async Task<UserDetailsDTO?> GetUserDetailsAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDetailsDTO
            {
                Id = user.Id,
                UserName = user.UserName!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                IsActive = user.IsActive,
                Roles = roles.ToList(),
                LockoutEnd = user.LockoutEnd?.UtcDateTime
            };
        }

        public async Task<IEnumerable<UserListDTO>> SearchUsersAsync(string searchTerm)
        {
            var users = await _userRepository.SearchUsersAsync(searchTerm);
            return await MapUsersToListDTOAsync(users);
        }

        public async Task<(bool Success, string Message)> UpdateUserAsync(UpdateUserDTO userDTO)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userDTO.Id));
                if (user == null)
                    return (false, "Không tìm thấy người dùng");

                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;
                user.Email = userDTO.Email;
                user.PhoneNumber = userDTO.PhoneNumber;
                user.IsActive = userDTO.IsActive;

                _userRepository.Update(user);

                // Cập nhật roles
                if (userDTO.Roles.Any())
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                    var validRoles = new List<string>();
                    foreach (var role in userDTO.Roles)
                    {
                        if (await _roleManager.RoleExistsAsync(role))
                            validRoles.Add(role);
                    }

                    if (validRoles.Any())
                    {
                        await _userManager.AddToRolesAsync(user, validRoles);
                    }
                }

                _logger.LogInformation($"Cập nhật người dùng {user.UserName} thành công");
                return (true, "Cập nhật người dùng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi cập nhật người dùng: {ex.Message}");
                return (false, "Có lỗi xảy ra khi cập nhật người dùng");
            }
        }

        public async Task<(bool Success, string Message)> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                    return (false, "Không tìm thấy người dùng");

                user.IsActive = false;
                _userRepository.Update(user);

                _logger.LogInformation($"Xóa người dùng {user.UserName} thành công");
                return (true, "Xóa người dùng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi xóa người dùng: {ex.Message}");
                return (false, "Có lỗi xảy ra khi xóa người dùng");
            }
        }

        public async Task<(bool Success, string Message)> RestoreUserAsync(string userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                    return (false, "Không tìm thấy người dùng");

                user.IsActive = true;
                _userRepository.Update(user);

                _logger.LogInformation($"Khôi phục người dùng {user.UserName} thành công");
                return (true, "Khôi phục người dùng thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi khôi phục người dùng: {ex.Message}");
                return (false, "Có lỗi xảy ra khi khôi phục người dùng");
            }
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Guid.Parse(changePasswordDTO.UserId));
                if (user == null)
                    return (false, "Không tìm thấy người dùng");

                var result = await _userManager.ChangePasswordAsync(
                    user,
                    changePasswordDTO.CurrentPassword,
                    changePasswordDTO.NewPassword
                );

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return (false, $"Đổi mật khẩu thất bại: {errors}");
                }

                _logger.LogInformation($"Đổi mật khẩu cho {user.UserName} thành công");
                return (true, "Đổi mật khẩu thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi đổi mật khẩu: {ex.Message}");
                return (false, "Có lỗi xảy ra khi đổi mật khẩu");
            }
        }

        public async Task<(bool Success, string Message, string? NewPassword)> ResetPasswordAsync(string userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                    return (false, "Không tìm thấy người dùng", null);

                var newPassword = GenerateRandomPassword();
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return (false, $"Đặt lại mật khẩu thất bại: {errors}", null);
                }

                _logger.LogInformation($"Đặt lại mật khẩu cho {user.UserName} thành công");
                return (true, "Đặt lại mật khẩu thành công", newPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi đặt lại mật khẩu: {ex.Message}");
                return (false, "Có lỗi xảy ra khi đặt lại mật khẩu", null);
            }
        }

        public async Task<(bool Success, string Message)> LockUserAsync(string userId, TimeSpan lockoutDuration)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                    return (false, "Không tìm thấy người dùng");

                await _userManager.SetLockoutEnabledAsync(user, true);
                var result = await _userManager.SetLockoutEndDateAsync(
                    user,
                    DateTimeOffset.UtcNow.Add(lockoutDuration)
                );

                if (!result.Succeeded)
                    return (false, "Khóa tài khoản thất bại");

                _logger.LogInformation($"Khóa tài khoản {user.UserName} thành công");
                return (true, "Khóa tài khoản thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi khóa tài khoản: {ex.Message}");
                return (false, "Có lỗi xảy ra khi khóa tài khoản");
            }
        }

        public async Task<(bool Success, string Message)> UnlockUserAsync(string userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                    return (false, "Không tìm thấy người dùng");

                var result = await _userManager.SetLockoutEndDateAsync(user, null);

                if (!result.Succeeded)
                    return (false, "Mở khóa tài khoản thất bại");

                _logger.LogInformation($"Mở khóa tài khoản {user.UserName} thành công");
                return (true, "Mở khóa tài khoản thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi mở khóa tài khoản: {ex.Message}");
                return (false, "Có lỗi xảy ra khi mở khóa tài khoản");
            }
        }

        public async Task<(bool Success, string Message)> AssignRolesToUserAsync(string userId, IEnumerable<string> roles)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                    return (false, "Không tìm thấy người dùng");

                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                var validRoles = new List<string>();
                foreach (var role in roles)
                {
                    if (await _roleManager.RoleExistsAsync(role))
                        validRoles.Add(role);
                }

                if (!validRoles.Any())
                    return (false, "Không có role hợp lệ");

                var result = await _userManager.AddToRolesAsync(user, validRoles);

                if (!result.Succeeded)
                    return (false, "Gán role thất bại");

                _logger.LogInformation($"Gán roles cho {user.UserName} thành công");
                return (true, "Gán roles thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi gán roles: {ex.Message}");
                return (false, "Có lỗi xảy ra khi gán roles");
            }
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
            if (user == null)
                return Enumerable.Empty<string>();

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _userRepository.UserExistsAsync(userName);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        private async Task<IEnumerable<UserListDTO>> MapUsersToListDTOAsync(IEnumerable<User> users)
        {
            var result = new List<UserListDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserListDTO
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    IsActive = user.IsActive,
                    Roles = roles.ToList()
                });
            }

            return result;
        }

        private string GenerateRandomPassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%";
            var random = new Random();
            return new string(Enumerable.Repeat(validChars, 10)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
    }
}
