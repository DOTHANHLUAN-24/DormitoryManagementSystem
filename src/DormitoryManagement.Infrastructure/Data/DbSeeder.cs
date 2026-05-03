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

            await context.Database.MigrateAsync();

            if (context.Users.Any())
                return;

            var data = SeedDataBuilder.Build();

            // 🔵 1. Seed Roles trước
            await RoleSeeder.SeedAsync(roleManager);

            // 🔵 2. Seed Users (Identity)
            await IdentitySeeder.SeedAsync(userManager, data.Users);

            // 🟢 3. Business data (KHÔNG phụ thuộc User)
            context.RoomTypes.AddRange(data.RoomTypes);
            context.Blocks.AddRange(data.Blocks);
            context.Rooms.AddRange(data.Rooms);
            context.Beds.AddRange(data.Beds);

            await context.SaveChangesAsync();

            // 🔴 4. Contract (PHỤ THUỘC User + Bed → để cuối)
            context.Contracts.AddRange(data.Contracts);

            await context.SaveChangesAsync();
        }
    }
}