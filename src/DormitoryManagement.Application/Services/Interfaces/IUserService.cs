using System.Threading.Tasks;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllActiveUsersAsync();

        Task<IEnumerable<User>> GetAllBanUserAsync();
        
        Task<User?> GetUserByIdAsync(Guid id);
        
        Task<User?> GetByUsernameAsync(string username);
        
        Task CreateUserAsync(User user);
        
        Task UpdateUserProfileAsync(User user);
        
        Task DeactivateUserAsync(Guid id); 
    }
}
