using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.RoomTypes;
using DormitoryManagement.Application.Dtos.Responses.RoomTypes;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services.Implements
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomTypeService(IRoomTypeRepository roomTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomTypeResponseDto>> GetAllRoomTypesAsync()
        {
            var types = await _roomTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoomTypeResponseDto>>(types);
        }

        public async Task<RoomTypeResponseDto?> GetRoomTypeByIdAsync(Guid id)
        {
            var type = await _roomTypeRepository.GetByIdAsync(id);
            return _mapper.Map<RoomTypeResponseDto>(type);
        }

        public async Task<bool> CreateRoomTypeAsync(RoomTypeRequestDto request)
        {
            if (await _roomTypeRepository.IsTypeNameDuplicateAsync(request.TypeName))
                throw new Exception("Tên loại phòng này đã tồn tại.");

            var roomType = _mapper.Map<RoomType>(request);
            await _roomTypeRepository.AddAsync(roomType);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRoomTypeAsync(Guid id, RoomTypeRequestDto request)
        {
            var type = await _roomTypeRepository.GetByIdAsync(id);
            if (type == null) return false;

            if (await _roomTypeRepository.IsTypeNameDuplicateAsync(request.TypeName, id))
                throw new Exception("Tên loại phòng mới bị trùng với loại khác.");

            _mapper.Map(request, type);
            await _roomTypeRepository.UpdateAsync(type);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRoomTypeAsync(Guid id)
        {
            var type = await _roomTypeRepository.GetRoomTypeWithRoomsAsync(id);
            if (type == null) return false;

            if (type.Rooms != null && type.Rooms.Any(r => !r.IsDeleted))
                throw new Exception("Không thể xóa loại phòng này vì đang có phòng sử dụng.");

            await _roomTypeRepository.DeleteAsync(type, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}