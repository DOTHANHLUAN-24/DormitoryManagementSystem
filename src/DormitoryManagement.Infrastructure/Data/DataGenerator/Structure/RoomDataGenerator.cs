using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Structure
{
    public static class RoomDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            foreach (var block in ctx.Blocks)
            {
                for (int i = 1; i <= 5; i++)
                {
                    var roomType = ctx.Faker.PickRandom(ctx.RoomTypes);

                    var room = new Room
                    {
                        Id = Guid.NewGuid(),
                        RoomNumber = $"{block.BlockName.Last()}{i:00}",
                        Floor = ctx.Faker.Random.Int(1, block.TotalFloors),
                        BlockId = block.Id,
                        RoomTypeId = roomType.Id,
                        Status = RoomStatus.Available,
                        IsActive = true
                    };

                    ctx.Rooms.Add(room);
                }
            }
        }
    }
}
