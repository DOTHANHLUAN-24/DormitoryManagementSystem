using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Core
{
    public static class RoomTypeDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            ctx.RoomTypes = new List<RoomType>
            {
                new RoomType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Phòng 2 sinh viên",
                    BasePrice = 1575000m,
                    MaxOccupants = 2,
                    IsActive = true
                },
                new RoomType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Phòng 4 sinh viên",
                    BasePrice = 800000m,
                    MaxOccupants = 4,
                    IsActive = true
                },
                new RoomType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Phòng 6 sinh viên",
                    BasePrice = 275000m,
                    MaxOccupants = 6,
                    IsActive = true
                }, 
                new RoomType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Phòng 8 sinh viên",
                    BasePrice = 205000m,
                    MaxOccupants = 8,
                    IsActive = true
                }
            };
        }
    }
}
