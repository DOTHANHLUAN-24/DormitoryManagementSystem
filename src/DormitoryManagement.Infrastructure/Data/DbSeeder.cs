using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
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
                if (data.RoomTypes.Count > 0) await context.RoomTypes.AddRangeAsync(data.RoomTypes);
                if (data.Blocks.Count > 0) await context.Blocks.AddRangeAsync(data.Blocks);
                if (data.Rooms.Count > 0) await context.Rooms.AddRangeAsync(data.Rooms);
                if (data.Beds.Count > 0) await context.Beds.AddRangeAsync(data.Beds);

                await context.SaveChangesAsync();
            }

            // 🔴 4. Seed Contracts (Hợp đồng)
            // Chỉ seed nếu chưa có hợp đồng nào và có dữ liệu mẫu
            if (!await context.Contracts.AnyAsync() && data.Contracts.Count > 0)
            {
                await context.Contracts.AddRangeAsync(data.Contracts);
                await context.SaveChangesAsync();
            }

            // 🔴 5. Seed Utilities (Dịch vụ / Tiện ích)
            if (!await context.Utilities.AnyAsync() && data.Utilities.Count > 0)
            {
                await context.Utilities.AddRangeAsync(data.Utilities);
                await context.SaveChangesAsync();
            }

            // 🟠 6. Seed Assets (Tài sản)
            if (!await context.Assets.AnyAsync() && data.Assets.Count > 0)
            {
                await context.Assets.AddRangeAsync(data.Assets);
                await context.SaveChangesAsync();
            }

            // 🟠 7. Seed Vehicles (Phương tiện)
            if (!await context.Vehicles.AnyAsync() && data.Vehicles.Count > 0)
            {
                await context.Vehicles.AddRangeAsync(data.Vehicles);
                await context.SaveChangesAsync();
            }

            // 🟠 8. Seed Invoices, UtilityUsages, Surcharges, Payments (Hóa đơn và phụ trợ)
            if (!await context.Invoices.AnyAsync() && data.Invoices.Count > 0)
            {
                await context.Invoices.AddRangeAsync(data.Invoices);
                if (data.UtilityUsages.Count > 0) await context.UtilityUsages.AddRangeAsync(data.UtilityUsages);
                if (data.Surcharges.Count > 0) await context.Surcharges.AddRangeAsync(data.Surcharges);
                if (data.Payments.Count > 0) await context.Payments.AddRangeAsync(data.Payments);
                await context.SaveChangesAsync();
            }

            // 🟠 9. Seed MaintenanceRequests (Yêu cầu bảo trì)
            if (!await context.MaintenanceRequests.AnyAsync() && data.MaintenanceRequests.Count > 0)
            {
                await context.MaintenanceRequests.AddRangeAsync(data.MaintenanceRequests);
                await context.SaveChangesAsync();
            }

            // 🟠 10. Seed Violations (Vi phạm)
            if (!await context.Violations.AnyAsync() && data.Violations.Count > 0)
            {
                await context.Violations.AddRangeAsync(data.Violations);
                await context.SaveChangesAsync();
            }

            // 🟠 11. Seed UtilityServiceRequests (Yêu cầu đăng ký dịch vụ)
            if (!await context.UtilityServiceRequests.AnyAsync() && data.UtilityServiceRequests.Count > 0)
            {
                await context.UtilityServiceRequests.AddRangeAsync(data.UtilityServiceRequests);
                await context.SaveChangesAsync();
            }

            // 🟢 12. Đồng bộ lại trạng thái phòng bị sai lệch (ví dụ do dữ liệu cũ hoặc seeding)
            var rooms = await context.Rooms.Include(r => r.Beds).Where(r => !r.IsDeleted).ToListAsync();
            bool hasChanges = false;
            foreach (var room in rooms)
            {
                if (room.Status == RoomStatus.Maintenance) continue;

                var activeBeds = room.Beds.Where(b => !b.IsDeleted).ToList();
                bool allOccupied = activeBeds.Count > 0 && activeBeds.All(b => b.Status == BedStatus.Occupied);

                if (allOccupied && room.Status != RoomStatus.Full)
                {
                    room.Status = RoomStatus.Full;
                    hasChanges = true;
                }
                else if (!allOccupied && room.Status == RoomStatus.Full)
                {
                    room.Status = RoomStatus.Available;
                    hasChanges = true;
                }
            }
            if (hasChanges)
            {
                await context.SaveChangesAsync();
            }
        }
    }
}