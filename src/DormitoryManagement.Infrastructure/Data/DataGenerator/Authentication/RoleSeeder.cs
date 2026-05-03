using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Authentication
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = { "Admin", "ManagementStaff", "Student", "TechnicalStaff" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }
    }
}
