using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Structure
{
    public static class BedDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            foreach (var room in ctx.Rooms)
            {
                var roomType = ctx.RoomTypes.First(x => x.Id == room.RoomTypeId);

                for (int i = 1; i <= roomType.MaxOccupants; i++)
                {
                    ctx.Beds.Add(new Bed
                    {
                        Id = Guid.NewGuid(),
                        BedNumber = $"{room.RoomNumber}-B{i}",
                        RoomId = room.Id,
                        Status = BedStatus.Available,
                        IsActive = true
                    });
                }
            }
        }
    }
}
