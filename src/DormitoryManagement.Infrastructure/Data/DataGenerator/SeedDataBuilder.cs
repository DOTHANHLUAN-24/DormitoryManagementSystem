using Bogus;
using DormitoryManagement.Infrastructure.Data.DataGenerator.Authentication;
using DormitoryManagement.Infrastructure.Data.DataGenerator.Business;
using DormitoryManagement.Infrastructure.Data.DataGenerator.Core;
using DormitoryManagement.Infrastructure.Data.DataGenerator.Structure;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator
{
    public static class SeedDataBuilder
    {
        public static SeedContext Build()
        {
            Randomizer.Seed = new Random(1234);

            var ctx = new SeedContext();

            RoomTypeDataGenerator.Generate(ctx);
            BlockDataGenerator.Generate(ctx);

            RoomDataGenerator.Generate(ctx);
            BedDataGenerator.Generate(ctx);

            UserDataGenerator.Generate(ctx);

            ContractDataGenerator.Generate(ctx);

            return ctx;
        }
    }
}
