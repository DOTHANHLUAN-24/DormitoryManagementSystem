using System;
using System.Linq;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Business
{
    public static class UtilityServiceRequestDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            var contracts = ctx.Contracts.ToList();
            var utilities = ctx.Utilities.Where(u => !u.UtilityName.ToLower().Contains("điện") && !u.UtilityName.ToLower().Contains("nước")).ToList();

            if (contracts.Count == 0 || utilities.Count == 0) return;

            foreach (var contract in contracts)
            {
                // 60% probability of requesting services
                if (ctx.Faker.Random.Bool(0.6f))
                {
                    // Pick a random utility
                    var utility = ctx.Faker.PickRandom(utilities);
                    var bed = ctx.Beds.FirstOrDefault(b => b.Id == contract.BedId);
                    if (bed == null) continue;

                    var status = ctx.Faker.PickRandom("Approved", "Pending", "Rejected");
                    var quantity = ctx.Faker.Random.Number(1, 2);

                    ctx.UtilityServiceRequests.Add(new UtilityServiceRequest
                    {
                        Id = Guid.NewGuid(),
                        RoomId = bed.RoomId,
                        RequesterId = contract.UserId!,
                        UtilityId = utility.Id,
                        Status = status,
                        Quantity = quantity,
                        Notes = status == "Rejected" ? "Từ chối do hết chỗ hoặc không hợp lệ" : "Đăng ký dịch vụ sử dụng cho phòng",
                        CreatedDate = DateTime.Now.AddDays(-ctx.Faker.Random.Number(1, 25)),
                        IsActive = true,
                        IsDeleted = false
                    });
                }
            }
        }
    }
}
