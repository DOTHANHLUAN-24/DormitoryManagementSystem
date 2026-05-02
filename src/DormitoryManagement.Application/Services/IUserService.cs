using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;

namespace DormitoryManagement.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync();
        Task<bool> RegisterDormitoryAsync(StudentRequestDto request);
    }
}
