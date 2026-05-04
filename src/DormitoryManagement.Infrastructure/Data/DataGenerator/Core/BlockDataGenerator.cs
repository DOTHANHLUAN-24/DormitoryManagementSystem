using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Core
{
    public static class BlockDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            ctx.Blocks = new List<Block>
            {
                new Block
                {
                    Id = Guid.NewGuid(),
                    BlockName = "Khu A",
                    TotalFloors = 5,
                    Description = "Khu A là khu của nam, được xây từ những năm 2018",
                    IsActive = true
                },
                new Block
                {
                    Id = Guid.NewGuid(),
                    BlockName = "Khu B",
                    TotalFloors = 4,
                    Description = "Khu B là khu của nữ, được xây từ những năm 2019",
                    IsActive = true
                }
            };
        }
    }
}
