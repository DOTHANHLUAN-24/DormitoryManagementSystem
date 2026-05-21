using System;
using System.Collections.Generic;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Business
{
    public static class UtilityDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            ctx.Utilities.AddRange(new List<Utility>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UtilityName = "Điện sinh hoạt",
                    UnitPrice = 3500,
                    Unit = "kWh",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now.AddDays(-10)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UtilityName = "Nước sạch",
                    UnitPrice = 12000,
                    Unit = "m3",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now.AddDays(-10)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UtilityName = "Internet Wifi",
                    UnitPrice = 150000,
                    Unit = "Tháng",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now.AddDays(-10)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UtilityName = "Gửi xe máy",
                    UnitPrice = 80000,
                    Unit = "Tháng",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now.AddDays(-10)
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UtilityName = "Giặt ủi sấy khô",
                    UnitPrice = 15000,
                    Unit = "Lượt",
                    IsActive = false, // Đưa vào thùng rác sẵn để test
                    IsDeleted = false,
                    CreatedDate = DateTime.Now.AddDays(-10)
                }
            });
        }
    }
}
