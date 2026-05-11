using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Mappings;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ================= QUERY (Đọc dữ liệu) =================

        public async Task<PagedResult<UserResponseDto>> GetActiveUsersPagedAsync(int page, int pageSize, string? search)
        {
            var result = await _userRepository.GetByStatusPagedAsync(
                page, pageSize,
                isActive: true,
                isDeleted: false,
                predicate: u => string.IsNullOrEmpty(search) ||
                                u.UserName!.Contains(search) ||
                                u.Email!.Contains(search) ||
                                u.PhoneNumber!.Contains(search)
            );

            return result.MapToPagedResult<User, UserResponseDto>(_mapper);
        }

        public async Task<PagedResult<UserResponseDto>> GetBannedUsersPagedAsync(int page, int pageSize, string? search)
        {
            var result = await _userRepository.GetByStatusPagedAsync(
                page, pageSize,
                isActive: false,
                isDeleted: false,
                predicate: u => string.IsNullOrEmpty(search) || u.UserName!.Contains(search)
            );

            return result.MapToPagedResult<User, UserResponseDto>(_mapper);
        }

        public async Task<PagedResult<UserResponseDto>> GetDeletedUsersPagedAsync(int page, int pageSize, string? search)
        {
            var result = await _userRepository.GetByStatusPagedAsync(
                page, pageSize,
                isActive: null, // Không quan tâm active hay không
                isDeleted: true,
                predicate: u => string.IsNullOrEmpty(search) || u.UserName!.Contains(search)
            );

            return result.MapToPagedResult<User, UserResponseDto>(_mapper);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto?> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return _mapper.Map<UserResponseDto>(user);
        }

        // ================= COMMAND (Thay đổi dữ liệu) =================

        public async Task<bool> CreateUserAsync(UserRequestDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            // Bạn có thể xử lý Hash mật khẩu ở đây nếu cần
            await _userRepository.AddAsync(user);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> CreateUsersAsync(IEnumerable<UserRequestDto> userDtos)
        {
            var users = _mapper.Map<IEnumerable<User>>(userDtos);
            await _userRepository.AddRangeAsync(users);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateUserProfileAsync(Guid id, UserRequestDto userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) return false;

            // Tạm thời lưu lại các thông tin quan trọng không cho phép sửa/tránh bị ghi đè null
            var originalUserName = existingUser.UserName;
            var originalNormalizedUserName = existingUser.NormalizedUserName;
            var originalEmail = existingUser.Email; // Giữ lại email nếu bạn cũng không muốn form update profile đổi email

            _mapper.Map(userDto, existingUser); // Map đè dữ liệu mới vào entity cũ

            // Phục hồi lại tên đăng nhập và các trường quan trọng
            existingUser.UserName = originalUserName;
            existingUser.NormalizedUserName = originalNormalizedUserName;

            await _userRepository.UpdateAsync(existingUser);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> ToggleUserStatusAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.IsActive = !user.IsActive;
            await _userRepository.UpdateAsync(user);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> BanUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.IsActive = false;
            await _userRepository.UpdateAsync(user);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

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

        public async Task<bool> DeactivateUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user, isSoftDelete: true);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeactivateUsersAsync(IEnumerable<Guid> ids)
        {
            var users = await _userRepository.FindAsync(u => ids.Contains(u.Id));
            if (!users.Any()) return false;

            await _userRepository.DeleteRangeAsync(users, isSoftDelete: true);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RestoreUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.RestoreAsync(user);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

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

        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user, isSoftDelete: false);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        // ================= VALIDATION =================

        public async Task<bool> IsUsernameExistAsync(string username)
        {
            return await _userRepository.AnyAsync(u => u.UserName == username);
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return !await _userRepository.IsEmailUniqueAsync(email);
        }
    }
}