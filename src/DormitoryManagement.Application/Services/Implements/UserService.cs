using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<User>> GetAllActiveUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Where(u => u.IsActive && !u.IsDeleted).ToList();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null || user.IsDeleted) return null;
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userRepo.GetByUsernameAsync(username);
        }

        public async Task CreateUserAsync(User user)
        {
            var existingUser = await _userRepo.GetByUsernameAsync(user.UserName!);
            if (existingUser != null)
            {
                throw new Exception("Tên đăng nhập đã tồn tại.");
            }

            user.IsActive = true;
            user.IsDeleted = false;

            await _userRepo.AddAsync(user);
        }

        public async Task UpdateUserProfileAsync(User user)
        {
            var userInDb = await _userRepo.GetByIdAsync(user.Id);
            if (userInDb == null || userInDb.IsDeleted)
            {
                throw new KeyNotFoundException("Người dùng không tồn tại hoặc đã bị xóa.");
            }

            userInDb.FullName = user.FullName;
            userInDb.PhoneNumber = user.PhoneNumber;
            userInDb.Email = user.Email;
            userInDb.IdentityCardNumber = user.IdentityCardNumber;
            userInDb.Role = user.Role;

            ((IBaseRepository<User>)_userRepo).Update(userInDb);
        }

        public async Task DeactivateUserAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user != null)
            {
                _userRepo.Delete(user);
            }
        }

        public async Task<IEnumerable<User>> GetAllBanUserAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Where(u => !u.IsActive && !u.IsDeleted);
        }
    }
}
