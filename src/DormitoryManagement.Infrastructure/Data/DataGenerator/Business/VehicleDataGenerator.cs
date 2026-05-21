using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Business
{
    public static class VehicleDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            var students = ctx.Users.Where(x => x.Role == UserRole.Student).ToList();
            var vehicleTypes = new[] { "Xe máy", "Xe máy điện", "Xe đạp điện", "Xe đạp" };

            foreach (var student in students)
            {
                // Khoảng 60% sinh viên đăng ký gửi xe trong ký túc xá
                if (ctx.Faker.Random.Bool(0.6f))
                {
                    var vehicleType = ctx.Faker.PickRandom(vehicleTypes);
                    string licensePlate = vehicleType == "Xe đạp"
                        ? $"XD-{ctx.Faker.Random.Number(1000, 9999)}"
                        : $"{ctx.Faker.Random.Number(29, 99)}-{ctx.Faker.Random.Char('A', 'Z')}{ctx.Faker.Random.Number(1, 9)} {ctx.Faker.Random.Number(100, 999)}.{ctx.Faker.Random.Number(10, 99)}";

                    ctx.Vehicles.Add(new Vehicle
                    {
                        Id = Guid.NewGuid(),
                        OwnerId = student.Id,
                        VehicleType = vehicleType,
                        LicensePlate = licensePlate,
                        IsActive = true,
                        IsDeleted = false
                    });
                }
            }
        }
    }
}
