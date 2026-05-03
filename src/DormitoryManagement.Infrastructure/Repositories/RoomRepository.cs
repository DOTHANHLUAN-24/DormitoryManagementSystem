using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Room> GetPagingQuery(string searchString)
        {
            var query = _db.Rooms.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.RoomNumber.Contains(searchString));
            }

            return query.OrderByDescending(x => x.CreatedDate);
        }

        public async Task<IEnumerable<Room>> ListAllRoomAsync()
        {
            return await _db.Rooms
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<RoomType>> ListAllRoomTypeAsync()
        {
            return await _db.RoomTypes
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<bool> Insert(Room entity)
        {
            entity.CreatedDate = DateTime.Now;
            await _db.Rooms.AddAsync(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Room entity)
        {
            try
            {
                var room = await _db.Rooms.FindAsync(entity.Id);
                if (room == null)
                    return false;

                room.RoomNumber = entity.RoomNumber;
                room.Floor = entity.Floor;
                room.Status = entity.Status;
                room.BlockId = entity.BlockId;
                room.RoomTypeId = entity.RoomTypeId;
                room.LastModified = DateTime.Now;

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var room = await _db.Rooms.FindAsync(id);
                if (room == null)
                    return false;

                _db.Rooms.Remove(room);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _db.Rooms.ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(Guid id)
        {
            return await _db.Rooms.FindAsync(id);
        }

        public async Task AddAsync(Room entity)
        {
            entity.CreatedDate = DateTime.Now;
            await _db.Rooms.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Room> entities)
        {
            var now = DateTime.Now;
            foreach (var e in entities)
            {
                e.CreatedDate = now;
            }

            await _db.Rooms.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
        }

        public void Delete(Room entity)
        {
            var room = _db.Rooms.Find(entity.Id);
            if (room == null) return;

            _db.Rooms.Remove(room);
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<Room> entities)
        {
            var ids = entities.Select(e => e.Id).ToList();
            var rooms = _db.Rooms.Where(r => ids.Contains(r.Id)).ToList();
            if (!rooms.Any()) return;

            _db.Rooms.RemoveRange(rooms);
            _db.SaveChanges();
        }

        void IBaseRepository<Room>.Update(Room entity)
        {
            throw new NotImplementedException();
        }
    }
}