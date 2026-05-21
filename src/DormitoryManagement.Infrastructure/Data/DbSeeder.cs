using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Infrastructure.Data.DataGenerator;
using DormitoryManagement.Infrastructure.Data.DataGenerator.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DormitoryManagement.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            // Luôn đảm bảo DB đã được migrate lên bản mới nhất
            await context.Database.MigrateAsync();

            // Lấy data từ Builder (chỉ lấy 1 lần để dùng chung bên dưới)
            var data = SeedDataBuilder.Build();

            // 🔵 1. Seed Roles (Kiểm tra nếu chưa có Role nào thì mới Seed)
            if (!await roleManager.Roles.AnyAsync())
            {
                await RoleSeeder.SeedAsync(roleManager);
            }

            // 🔵 2. Seed Users (Kiểm tra nếu chưa có User nào thì mới Seed)
            if (!await userManager.Users.AnyAsync())
            {
                await IdentitySeeder.SeedAsync(userManager, data.Users);
            }

            // 🟢 3. Seed Business Data (Dãy nhà, Loại phòng, Phòng, Giường)
            // Kiểm tra một bảng đại diện, ví dụ: RoomTypes hoặc Blocks
            if (!await context.Blocks.AnyAsync())
            {
                if (data.RoomTypes.Any()) await context.RoomTypes.AddRangeAsync(data.RoomTypes);
                if (data.Blocks.Any()) await context.Blocks.AddRangeAsync(data.Blocks);
                if (data.Rooms.Any()) await context.Rooms.AddRangeAsync(data.Rooms);
                if (data.Beds.Any()) await context.Beds.AddRangeAsync(data.Beds);

                await context.SaveChangesAsync();
            }

            // 🔴 4. Seed Contracts (Hợp đồng)
            // Chỉ seed nếu chưa có hợp đồng nào và có dữ liệu mẫu
            if (!await context.Contracts.AnyAsync() && data.Contracts.Any())
            {
                await context.Contracts.AddRangeAsync(data.Contracts);
                await context.SaveChangesAsync();
            }

            // 🔴 5. Seed Utilities (Dịch vụ / Tiện ích)
            if (!await context.Utilities.AnyAsync() && data.Utilities.Any())
            {
                await context.Utilities.AddRangeAsync(data.Utilities);
                await context.SaveChangesAsync();
            }

            // 🟠 6. Seed Assets (Tài sản)
            if (!await context.Assets.AnyAsync() && data.Assets.Any())
            {
                await context.Assets.AddRangeAsync(data.Assets);
                await context.SaveChangesAsync();
            }

            // 🟠 7. Seed Vehicles (Phương tiện)
            if (!await context.Vehicles.AnyAsync() && data.Vehicles.Any())
            {
                await context.Vehicles.AddRangeAsync(data.Vehicles);
                await context.SaveChangesAsync();
            }

            // 🟠 8. Seed Invoices, UtilityUsages, Surcharges, Payments (Hóa đơn và phụ trợ)
            if (!await context.Invoices.AnyAsync() && data.Invoices.Any())
            {
                await context.Invoices.AddRangeAsync(data.Invoices);
                if (data.UtilityUsages.Any()) await context.UtilityUsages.AddRangeAsync(data.UtilityUsages);
                if (data.Surcharges.Any()) await context.Surcharges.AddRangeAsync(data.Surcharges);
                if (data.Payments.Any()) await context.Payments.AddRangeAsync(data.Payments);
                await context.SaveChangesAsync();
            }

            // 🟠 9. Seed MaintenanceRequests (Yêu cầu bảo trì)
            if (!await context.MaintenanceRequests.AnyAsync() && data.MaintenanceRequests.Any())
            {
                await context.MaintenanceRequests.AddRangeAsync(data.MaintenanceRequests);
                await context.SaveChangesAsync();
            }

            // 🟠 10. Seed Violations (Vi phạm)
            if (!await context.Violations.AnyAsync() && data.Violations.Any())
            {
                await context.Violations.AddRangeAsync(data.Violations);
                await context.SaveChangesAsync();
            }
        }
    }
}