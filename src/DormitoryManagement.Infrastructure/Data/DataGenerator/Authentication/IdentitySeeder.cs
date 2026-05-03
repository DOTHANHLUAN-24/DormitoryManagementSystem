using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Authentication
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            UserManager<User> userManager,
            List<User> users)
        {
            foreach (var user in users)
            {
                var existing = await userManager.FindByNameAsync(user.UserName!);
                if (existing != null) continue;

                var password = user.Role switch
                {
                    UserRole.Admin => "Admin@123",
                    UserRole.ManagementStaff => "Manager@123",
                    UserRole.TechnicalStaff => "Tech@123",
                    _ => "Student@123"
                };

                // Tạo user
                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception("Create user failed: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                // Gán role
                await userManager.AddToRoleAsync(user, user.Role.ToString());
            }
        }
    }
}
