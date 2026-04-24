using DormitoryManagement.Data.Entities;

namespace DormitoryManagement.Data.Dao
{
    public class RoomDao
    {
        ApplicationDbContext db = null!;

        public RoomDao()
        {
            db = new ApplicationDbContext();
        }

        public IEnumerable<Room> ListAllRoom()
        {
            IQueryable<Room> listRooms = db.Rooms;

            return listRooms.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public IEnumerable<RoomType> ListAllRoomType()
        {
            IQueryable<RoomType> listRoomTypes = db.RoomTypes;

            return listRoomTypes.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<bool> Insert(Room entity)
        {
            db.Rooms.Add(entity);

            entity.CreatedDate = DateTime.Now;

            var result = await db.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                var room = db.Rooms.Find(id);
                if (room == null)
                {
                    return false;
                }
                db.Rooms.Remove(room);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Room entity)
        {
            try
            {
                var room = db.Rooms.Find(entity.Id);
                if (room == null)
                {
                    return false;
                }
                room.RoomNumber = entity.RoomNumber;
                room.Floor = entity.Floor;
                room.Status = entity.Status;
                room.BlockId = entity.BlockId;
                room.RoomTypeId = entity.RoomTypeId;

                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
