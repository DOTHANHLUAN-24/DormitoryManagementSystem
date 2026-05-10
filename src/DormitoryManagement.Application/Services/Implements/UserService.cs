using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Application.Mappings;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
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

        public async Task CreateUserAsync(UserRequestDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            // Bạn có thể xử lý Hash mật khẩu ở đây nếu cần
            await _userRepository.AddAsync(user);
        }

        public async Task CreateUsersAsync(IEnumerable<UserRequestDto> userDtos)
        {
            var users = _mapper.Map<IEnumerable<User>>(userDtos);
            await _userRepository.AddRangeAsync(users);
        }

        public async Task UpdateUserProfileAsync(Guid id, UserRequestDto userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser != null)
            {
                _mapper.Map(userDto, existingUser); // Map đè dữ liệu mới vào entity cũ
                await _userRepository.UpdateAsync(existingUser);
            }
        }

        public async Task ToggleUserStatusAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task BanUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task UnbanUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.IsActive = true;
                await _userRepository.UpdateAsync(user);
            }
        }

        // ================= DELETE & RESTORE =================

        public async Task DeactivateUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user, isSoftDelete: true);
            }
        }

        public async Task DeactivateUsersAsync(IEnumerable<Guid> ids)
        {
            var users = await _userRepository.FindAsync(u => ids.Contains(u.Id));
            await _userRepository.DeleteRangeAsync(users, isSoftDelete: true);
        }

        public async Task RestoreUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.RestoreAsync(user);
            }
        }

        public async Task RestoreUsersAsync(IEnumerable<Guid> ids)
        {
            var users = await _userRepository.FindAsync(u => ids.Contains(u.Id));
            foreach (var user in users)
            {
                await _userRepository.RestoreAsync(user);
            }
        }

        public async Task DeletePermanentlyAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user, isSoftDelete: false);
            }
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