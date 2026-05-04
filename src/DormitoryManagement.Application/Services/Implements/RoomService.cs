using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services.Implements
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepo;

        public RoomService(IRoomRepository roomRepo)
        {
            _roomRepo = roomRepo;
        }

        public async Task<IEnumerable<Room>> GetActiveRoomsAsync()
        {
            var rooms = await _roomRepo.GetAllAsync();
            return rooms.Where(r => r.IsActive == true);
        }

        public async Task<Room?> GetRoomDetailAsync(Guid id)
        {
            return await _roomRepo.GetByIdAsync(id);
        }

        public async Task CreateNewRoomAsync(Room room)
        {
            var existing = await _roomRepo.GetAllAsync();
            if (existing.Any(x => x.RoomNumber == room.RoomNumber))
            {
                throw new Exception("Số phòng này đã tồn tại trong hệ thống!");
            }

            await _roomRepo.AddAsync(room);
        }

        public async Task UpdateRoomInfoAsync(Room room)
        {
            var roomInDb = await _roomRepo.GetByIdAsync(room.Id);
            if (roomInDb == null) throw new KeyNotFoundException("Không tìm thấy phòng");

            roomInDb.RoomNumber = room.RoomNumber;
            roomInDb.Floor = room.Floor;
            roomInDb.Status = room.Status;
            roomInDb.RoomTypeId = room.RoomTypeId;

            _roomRepo.Update(roomInDb);
        }

        public async Task RemoveRoomAsync(Guid id)
        {
            var room = await _roomRepo.GetByIdAsync(id);
            if (room != null)
            {
                _roomRepo.Delete(room);
            }
        }
    }
}
