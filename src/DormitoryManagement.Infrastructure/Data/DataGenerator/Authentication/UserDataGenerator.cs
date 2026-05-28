using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Authentication
{
    public static class UserDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            // 1. Quản trị viên
            var admin = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Admin",
                UserName = "admin",
                Code = "ADMIN001",
                Role = UserRole.Admin,
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                IsActive = true
            };
            ctx.Users.Add(admin);

            int yearPrefix = 22;      
            int educationType = 2;    
            int globalStt = 1;        

            // Định nghĩa danh sách mã ngành và số lượng sinh viên cho mỗi ngành
            var majorConfigs = new List<(string Code, int Count)>
            {
                ("101", 10), 
                ("102", 10), 
                ("105", 10), 
                ("108", 10)  
            };

            foreach (var config in majorConfigs)
            {
                for (int i = 0; i < config.Count; i++)
                {
                    // Tạo MSSV: [22] + [2] + [Mã ngành] + [STT 4 số]
                    // Ví dụ: 2221010001, 2221010002, 2221020003...
                    string mssv = $"{yearPrefix}{educationType}{config.Code}{globalStt:D4}";

                    var student = new User
                    {
                        Id = Guid.NewGuid(),
                        FullName = ctx.Faker.Name.FullName(),
                        UserName = mssv,
                        Code = mssv,
                        Role = UserRole.Student,
                        IdentityCardNumber = ctx.Faker.Random.Replace("############"),
                        PhoneNumber = ctx.Faker.Phone.PhoneNumber("0#########"),
                        PhoneNumberConfirmed = true,
                        Email = ctx.Faker.Internet.Email(mssv),
                        EmailConfirmed = true,
                        IsActive = true
                    };

                    ctx.Users.Add(student);
                    globalStt++; // Tăng STT dùng chung để không bị trùng MSSV giữa các ngành
                }
            }

            // 3. Nhân viên quản lý (manager1, manager2)
            for (int i = 1; i <= 2; i++)
            {
                string username = $"manager{i}";
                string staffCode = $"NVQL00{i}";
                ctx.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FullName = ctx.Faker.Name.FullName(),
                    UserName = username,
                    Code = staffCode,
                    Role = UserRole.ManagementStaff,
                    IdentityCardNumber = ctx.Faker.Random.Replace("############"),
                    PhoneNumber = ctx.Faker.Phone.PhoneNumber("0#########"),
                    PhoneNumberConfirmed = true,
                    Email = $"{username}@gmail.com",
                    EmailConfirmed = true,
                    IsActive = true
                });
            }

            // 4. Nhân viên kỹ thuật (tech1, tech2, tech3)
            for (int i = 1; i <= 3; i++)
            {
                string username = $"tech{i}";
                string techCode = $"NVKT00{i}";
                ctx.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FullName = ctx.Faker.Name.FullName(),
                    UserName = username,
                    Code = techCode,
                    Role = UserRole.TechnicalStaff,
                    IdentityCardNumber = ctx.Faker.Random.Replace("############"),
                    PhoneNumber = ctx.Faker.Phone.PhoneNumber("0#########"),
                    PhoneNumberConfirmed = true,
                    Email = $"{username}@gmail.com",
                    EmailConfirmed = true,
                    IsActive = true
                });
            }
        }
    }
}