using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        // Giả sử bạn có IUnitOfWork để SaveChanges, nếu không bạn có thể inject DbContext
        // Ở đây tôi minh họa dùng Repository trực tiếp

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<RoomResponse>> GetPagedRoomsAsync(RoomFilterRequest filter)
        {
            var pagedResult = await _roomRepository.SearchRoomsAsync(
                filter.SearchTerm,
                filter.BlockId,
                filter.RoomTypeId,
                filter.Status,
                filter.PageNumber,
                filter.PageSize
            );

            // Map từ PagedResult<Room> sang PagedResult<RoomResponse>
            var dtos = _mapper.Map<IEnumerable<RoomResponse>>(pagedResult.Items);

            return new PagedResult<RoomResponse>(
                dtos,
                pagedResult.TotalCount,
                pagedResult.PageNumber,
                pagedResult.PageSize);
        }

        public async Task<RoomDetailResponse?> GetRoomByIdAsync(Guid id)
        {
            var room = await _roomRepository.GetRoomWithDetailsAsync(id);
            if (room == null) return null;

            return _mapper.Map<RoomDetailResponse>(room);
        }

        public async Task<Guid> CreateRoomAsync(CreateRoomRequest request)
        {
            // 1. Kiểm tra trùng số phòng trong tòa nhà
            var isDuplicate = await _roomRepository.IsRoomNumberDuplicateAsync(request.RoomNumber, request.BlockId);
            if (isDuplicate)
                throw new Exception("Số phòng này đã tồn tại trong tòa nhà.");

            // 2. Map DTO sang Entity
            var room = _mapper.Map<Room>(request);
            room.Id = Guid.NewGuid();

            // 3. Lưu vào DB
            await _roomRepository.AddAsync(room);
            // await _unitOfWork.SaveChangesAsync(); // Thường sẽ gọi ở đây

            return room.Id;
        }

        public async Task<bool> UpdateRoomAsync(Guid id, UpdateRoomRequest request)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return false;

            // Kiểm tra trùng số phòng (trừ chính nó)
            var isDuplicate = await _roomRepository.IsRoomNumberDuplicateAsync(request.RoomNumber, request.BlockId, id);
            if (isDuplicate) throw new Exception("Số phòng bị trùng.");

            // Map dữ liệu từ request vào entity đã có
            _mapper.Map(request, room);

            await _roomRepository.UpdateAsync(room);
            return true;
        }

        public async Task<bool> DeleteRoomAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return false;

            await _roomRepository.DeleteAsync(room, isSoftDelete: true);
            return true;
        }

        public async Task<bool> RestoreRoomAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return false;

            await _roomRepository.RestoreAsync(room);
            return true;
        }

        public async Task<IEnumerable<RoomResponse>> GetRoomsByBlockAsync(Guid blockId)
        {
            var rooms = await _roomRepository.GetRoomsByBlockAsync(blockId);
            return _mapper.Map<IEnumerable<RoomResponse>>(rooms);
        }
    }
}