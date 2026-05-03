using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;

namespace DormitoryManagement.Application.Services.Implements
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllStudentsAsync();
        Task<bool> RegisterDormitoryAsync(UserRequestDto request);
    }
}
