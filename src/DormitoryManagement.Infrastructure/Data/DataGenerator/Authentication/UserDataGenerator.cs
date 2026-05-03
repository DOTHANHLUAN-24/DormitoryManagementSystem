using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Authentication
{
    public static class UserDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Role = UserRole.Admin,
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                IsActive = true
            };

            ctx.Users.Add(admin);

            var students = ctx.Faker.Make(5, () =>
            {
                return new User
                {
                    Id = Guid.NewGuid(),
                    FullName = ctx.Faker.Name.FullName(),
                    UserName = $"sv_{Guid.NewGuid().ToString()[..6]}",
                    Code = $"SV{ctx.Faker.Random.Number(1000, 9999)}",
                    Role = UserRole.Student,
                    PhoneNumber = ctx.Faker.Phone.PhoneNumber("0#########"),
                    PhoneNumberConfirmed = true,
                    Email = ctx.Faker.Internet.Email(),
                    EmailConfirmed = true,
                    IsActive = true
                };
            });

            ctx.Users.AddRange(students);

            var managementStaff = ctx.Faker.Make(1, () =>
            {
                return new User
                {
                    Id = Guid.NewGuid(),
                    FullName = ctx.Faker.Name.FullName(),
                    UserName = $"nv_{Guid.NewGuid().ToString()[..6]}",
                    Code = $"NV{ctx.Faker.Random.Number(1000, 9999)}",
                    Role = UserRole.ManagementStaff,
                    PhoneNumber = ctx.Faker.Phone.PhoneNumber("0#########"),
                    PhoneNumberConfirmed = true,
                    Email = ctx.Faker.Internet.Email(),
                    EmailConfirmed = true,
                    IsActive = true
                };
            });

            ctx.Users.AddRange(managementStaff);

            var technicalStaff = ctx.Faker.Make(3, () =>
            {
                return new User
                {
                    Id = Guid.NewGuid(),
                    FullName = ctx.Faker.Name.FullName(),
                    UserName = $"nv_{Guid.NewGuid().ToString()[..6]}",
                    Code = $"NV{ctx.Faker.Random.Number(1000, 9999)}",
                    Role = UserRole.TechnicalStaff,
                    PhoneNumber = ctx.Faker.Phone.PhoneNumber("0#########"),
                    PhoneNumberConfirmed = true,
                    Email = ctx.Faker.Internet.Email(),
                    EmailConfirmed = true,
                    IsActive = true
                };
            });

            ctx.Users.AddRange(technicalStaff);
        }
    }
}
