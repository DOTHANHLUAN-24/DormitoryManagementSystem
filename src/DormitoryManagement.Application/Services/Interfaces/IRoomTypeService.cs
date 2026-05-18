using DormitoryManagement.Application.Dtos.Requests.RoomTypes;
using DormitoryManagement.Application.Dtos.Responses.RoomTypes;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomTypeResponseDto>> GetAllRoomTypesAsync();
        Task<RoomTypeResponseDto?> GetRoomTypeByIdAsync(Guid id);
        Task<bool> CreateRoomTypeAsync(RoomTypeRequestDto request);
        Task<bool> UpdateRoomTypeAsync(Guid id, RoomTypeRequestDto request);
        Task<bool> DeleteRoomTypeAsync(Guid id);
    }
}