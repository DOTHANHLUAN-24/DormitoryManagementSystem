using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Business
{
    public static class ContractDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            var students = ctx.Users.Where(x => x.Role == UserRole.Student).ToList();
            var beds = ctx.Beds.Where(x => x.Status == BedStatus.Available).ToList();

            int count = Math.Min(students.Count, beds.Count);

            for (int i = 0; i < count; i++)
            {
                ctx.Contracts.Add(new Contract
                {
                    Id = Guid.NewGuid(),
                    UserId = students[i].Id,
                    BedId = beds[i].Id,
                    ContractCode = ctx.Faker.Random.Replace("HD-########"),
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now.AddMonths(6),
                    Status = ContractStatus.Active,
                    IsActive = true
                });

                beds[i].Status = BedStatus.Occupied;
            }
        }
    }
}
