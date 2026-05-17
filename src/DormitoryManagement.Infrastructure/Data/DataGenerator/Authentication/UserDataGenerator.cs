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
                ("101", 2), 
                ("102", 2), 
                ("105", 3), 
                ("108", 3)  
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

            // 3. Nhân viên quản lý (Giữ nguyên)
            var managementStaff = ctx.Faker.Make(2, () =>
            {
                string staffCode = $"NVQL{ctx.Faker.Random.Number(1000, 9999)}";
                return new User
                {
                    Id = Guid.NewGuid(),
                    FullName = ctx.Faker.Name.FullName(),
                    UserName = staffCode.ToLower(),
                    Code = staffCode,
                    Role = UserRole.ManagementStaff,
                    IdentityCardNumber = ctx.Faker.Random.Replace("############"),
                    PhoneNumber = ctx.Faker.Phone.PhoneNumber("0#########"),
                    PhoneNumberConfirmed = true,
                    Email = ctx.Faker.Internet.Email(),
                    EmailConfirmed = true,
                    IsActive = true
                };
            });
            ctx.Users.AddRange(managementStaff);

            // 4. Nhân viên kỹ thuật (Giữ nguyên)
            var technicalStaff = ctx.Faker.Make(3, () =>
            {
                string techCode = $"NVKT{ctx.Faker.Random.Number(1000, 9999)}";
                return new User
                {
                    Id = Guid.NewGuid(),
                    FullName = ctx.Faker.Name.FullName(),
                    UserName = techCode.ToLower(),
                    Code = techCode,
                    Role = UserRole.TechnicalStaff,
                    IdentityCardNumber = ctx.Faker.Random.Replace("############"),
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