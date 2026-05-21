using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Users;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Mappings;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý người dùng (UserService).
    /// </summary>
    public class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;

        // ================= QUERY (Đọc dữ liệu) =================

        /// <summary>
        /// Lấy danh sách người dùng đang hoạt động phân trang và tìm kiếm.
        /// </summary>
        public async Task<PagedResult<UserResponseDto>> GetActiveUsersPagedAsync(int page, int pageSize, string? search)
        {
            var result = await _userRepository.GetByStatusPagedAsync(
                page, pageSize,
                isActive: true,
                isDeleted: false,
                predicate: u => string.IsNullOrEmpty(search) ||
                                u.UserName!.Contains(search) ||
                                u.FullName.Contains(search) || // Thêm tìm kiếm theo tên
                                u.Code.Contains(search) ||     // Thêm tìm kiếm theo MSSV
                                u.Email!.Contains(search)
            );

            return result.MapToPagedResult<User, UserResponseDto>(_mapper);
        }

        /// <summary>
        /// Lấy danh sách người dùng bị khóa (Ban) phân trang và tìm kiếm.
        /// </summary>
        public async Task<PagedResult<UserResponseDto>> GetBannedUsersPagedAsync(int page, int pageSize, string? search)
        {
            var result = await _userRepository.GetByStatusPagedAsync(
                page, pageSize,
                isActive: false,
                isDeleted: false,
                predicate: u => string.IsNullOrEmpty(search) || u.UserName!.Contains(search) || u.FullName.Contains(search)
            );

            return result.MapToPagedResult<User, UserResponseDto>(_mapper);
        }

        /// <summary>
        /// Lấy danh sách người dùng đã bị xóa mềm phân trang và tìm kiếm.
        /// </summary>
        public async Task<PagedResult<UserResponseDto>> GetDeletedUsersPagedAsync(int page, int pageSize, string? search)
        {
            var result = await _userRepository.GetByStatusPagedAsync(
                page, pageSize,
                isActive: null, // Không quan tâm active hay không
                isDeleted: true,
                predicate: u => string.IsNullOrEmpty(search) || u.UserName!.Contains(search) || u.FullName.Contains(search)
            );

            return result.MapToPagedResult<User, UserResponseDto>(_mapper);
        }

        /// <summary>
        /// Lấy chi tiết thông tin người dùng theo Id.
        /// </summary>
        public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserResponseDto>(user);
        }

        /// <summary>
        /// Lấy thông tin người dùng theo tên tài khoản (Username).
        /// </summary>
        public async Task<UserResponseDto?> GetByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _mapper.Map<UserResponseDto>(user);
        }

        // ================= COMMAND (Thay đổi dữ liệu) =================

        /// <summary>
        /// Tạo mới một người dùng (sử dụng UserManager để mã hóa mật khẩu và tạo user).
        /// </summary>
        public async Task<bool> CreateUserAsync(UserRequestDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.IsActive = true;
            user.IsDeleted = false;
            user.CreatedDate = DateTime.Now;

            user.Role = userDto.Role;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception("Lỗi Identity: " + errors);
            }

            await _userManager.AddToRoleAsync(user, userDto.Role.ToString());

            return true;
        }

        /// <summary>
        /// Tạo hàng loạt nhiều người dùng.
        /// </summary>
        public async Task<bool> CreateUsersAsync(IEnumerable<UserRequestDto> userDtos)
        {
            var users = _mapper.Map<IEnumerable<User>>(userDtos);
            await _userRepository.AddRangeAsync(users);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Cập nhật thông tin hồ sơ của người dùng bao gồm cả đặt lại mật khẩu nếu có.
        /// </summary>
        public async Task<bool> UpdateUserProfileAsync(Guid id, UserUpdateDto userDto)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());
            if (existingUser == null) return false;

            if (existingUser.Role == UserRole.Admin && userDto.Role != UserRole.Admin)
            {
                var activeAdminCount = _userManager.Users.Count(u => u.Role == UserRole.Admin && u.IsActive && !u.IsDeleted);
                if (activeAdminCount <= 1 && existingUser.IsActive && !existingUser.IsDeleted)
                {
                    throw new Exception("Không thể thay đổi vai trò của tài khoản admin cuối cùng");
                }
            }

            existingUser.FullName = userDto.FullName;
            existingUser.Email = userDto.Email;
            existingUser.PhoneNumber = userDto.PhoneNumber;
            existingUser.IdentityCardNumber = userDto.IdentityCardNumber;
            existingUser.Code = userDto.Code;
            existingUser.Role = userDto.Role;
            existingUser.LastModified = DateTime.Now;

            if (!string.IsNullOrEmpty(userDto.NewPassword))
            {
                var removeResult = await _userManager.RemovePasswordAsync(existingUser);
                if (removeResult.Succeeded)
                {
                    await _userManager.AddPasswordAsync(existingUser, userDto.NewPassword);
                }
            }

            var result = await _userManager.UpdateAsync(existingUser);
            return result.Succeeded;
        }

        /// <summary>
        /// Bật/Tắt trạng thái hoạt động của người dùng (kết hợp thiết lập khóa tài khoản lockout).
        /// </summary>
        public async Task<bool> ToggleUserStatusAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return false;

            if (user.IsActive)
            {
                if (user.Role == UserRole.Admin && !user.IsDeleted)
                {
                    var activeAdminCount = _userManager.Users.Count(u => u.Role == UserRole.Admin && u.IsActive && !u.IsDeleted);
                    if (activeAdminCount <= 1)
                    {
                        throw new Exception("Không thể ban/vô hiệu hóa tài khoản admin cuối cùng");
                    }
                }
            }

            user.IsActive = !user.IsActive;
            user.LastModified = DateTime.Now;

            if (user.IsActive)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        /// <summary>
        /// Khóa (Ban) người dùng.
        /// </summary>
        public async Task<bool> BanUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            if (user.IsActive)
            {
                if (user.Role == UserRole.Admin && !user.IsDeleted)
                {
                    var activeAdminCount = _userManager.Users.Count(u => u.Role == UserRole.Admin && u.IsActive && !u.IsDeleted);
                    if (activeAdminCount <= 1)
                    {
                        throw new Exception("Không thể ban/vô hiệu hóa tài khoản admin cuối cùng");
                    }
                }
            }

            user.IsActive = false;
            await _userRepository.UpdateAsync(user);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Mở khóa (Unban) người dùng.
        /// </summary>
        public async Task<bool> UnbanUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.IsActive = true;
            await _userRepository.UpdateAsync(user);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        // ================= DELETE & RESTORE =================

        /// <summary>
        /// Xóa mềm một người dùng (IsDeleted = true).
        /// </summary>
        public async Task<bool> DeactivateUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            if (user.Role == UserRole.Admin && user.IsActive && !user.IsDeleted)
            {
                var activeAdminCount = _userManager.Users.Count(u => u.Role == UserRole.Admin && u.IsActive && !u.IsDeleted);
                if (activeAdminCount <= 1)
                {
                    throw new Exception("Không thể xóa tài khoản admin cuối cùng");
                }
            }

            await _userRepository.DeleteAsync(user, isSoftDelete: true);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Xóa mềm nhiều người dùng cùng lúc.
        /// </summary>
        public async Task<bool> DeactivateUsersAsync(IEnumerable<Guid> ids)
        {
            var users = await _userRepository.FindAsync(u => ids.Contains(u.Id));
            if (!users.Any()) return false;

            var activeAdminIdsInRequest = users.Where(u => u.Role == UserRole.Admin && u.IsActive && !u.IsDeleted).Select(u => u.Id).ToList();
            if (activeAdminIdsInRequest.Any())
            {
                var activeAdminsInDb = _userManager.Users.Where(u => u.Role == UserRole.Admin && u.IsActive && !u.IsDeleted).Select(u => u.Id).ToList();
                if (activeAdminsInDb.All(id => activeAdminIdsInRequest.Contains(id)))
                {
                    throw new Exception("Không thể xóa tài khoản admin cuối cùng");
                }
            }

            await _userRepository.DeleteRangeAsync(users, isSoftDelete: true);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Khôi phục một người dùng đã bị xóa mềm.
        /// </summary>
        public async Task<bool> RestoreUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.RestoreAsync(user);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Khôi phục nhiều người dùng cùng lúc.
        /// </summary>
        public async Task<bool> RestoreUsersAsync(IEnumerable<Guid> ids)
        {
            var users = await _userRepository.FindAsync(u => ids.Contains(u.Id));
            foreach (var user in users)
            {
                await _userRepository.RestoreAsync(user);
            }
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Xóa vĩnh viễn người dùng khỏi hệ thống thông qua UserManager.
        /// </summary>
        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return false;

            if (user.Role == UserRole.Admin)
            {
                var totalAdminCount = _userManager.Users.Count(u => u.Role == UserRole.Admin);
                if (totalAdminCount <= 1)
                {
                    throw new Exception("Không thể xóa tài khoản admin cuối cùng");
                }
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception("Lỗi Identity: " + errors);
            }

            return true;
        }

        // ================= VALIDATION =================

        /// <summary>
        /// Kiểm tra tên tài khoản đã tồn tại hay chưa.
        /// </summary>
        public async Task<bool> IsUsernameExistAsync(string username)
        {
            return await _userRepository.AnyAsync(u => u.UserName == username);
        }

        /// <summary>
        /// Kiểm tra địa chỉ email đã tồn tại hay chưa.
        /// </summary>
        public async Task<bool> IsEmailExistAsync(string email)
        {
            return !await _userRepository.IsEmailUniqueAsync(email);
        }
    }
}