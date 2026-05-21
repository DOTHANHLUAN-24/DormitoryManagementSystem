using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Business
{
    public static class MaintenanceRequestDataGenerator
    {
        private static readonly (string Title, string Description, MaintenancePriority Priority)[] MaintenanceTemplates =
        {
            ("Sửa vòi sen phòng tắm rỉ nước", "Vòi hoa sen trong phòng tắm bị nứt ở phần tay cầm gây rỉ nước liên tục khi mở khóa nước.", MaintenancePriority.Medium),
            ("Hỏng bóng đèn tuýp phòng sinh hoạt chung", "Bóng đèn led dài ở khu vực bàn học nhấp nháy liên tục rồi tắt hẳn, cần thay bóng mới.", MaintenancePriority.Low),
            ("Điều hòa không mát, chảy nước ngược", "Điều hòa bật 18 độ nhưng chỉ có gió, không mát và thỉnh thoảng có nước rò rỉ chảy xuống tường.", MaintenancePriority.High),
            ("Cửa sổ bị kẹt chốt không khóa được", "Chốt cửa sổ phía bên phải bị cong vênh do gió giật, hiện tại không chốt chặt được cửa.", MaintenancePriority.Medium),
            ("Tắc bồn cầu vệ sinh", "Bồn cầu thoát nước rất chậm và thỉnh thoảng trào ngược, cần thợ thông tắc hỗ trợ.", MaintenancePriority.Urgent),
            ("Hỏng ổ cắm điện đầu giường", "Ổ cắm điện ở vị trí giường G02 bị lỏng chân cắm bên trong, cắm sạc điện thoại không ăn điện.", MaintenancePriority.High)
        };

        public static void Generate(SeedContext ctx)
        {
            var contracts = ctx.Contracts.ToList();
            var techStaff = ctx.Users.Where(u => u.Role == UserRole.TechnicalStaff).ToList();

            if (contracts.Count == 0 || techStaff.Count == 0) return;

            // Tạo yêu cầu bảo trì cho khoảng 25% số hợp đồng/sinh viên
            var count = (int)Math.Ceiling(contracts.Count * 0.25);
            var selectedContracts = ctx.Faker.Random.ListItems(contracts, count);

            foreach (var contract in selectedContracts)
            {
                var bed = ctx.Beds.FirstOrDefault(b => b.Id == contract.BedId);
                if (bed == null) continue;

                var template = ctx.Faker.PickRandom(MaintenanceTemplates);
                var status = ctx.Faker.PickRandom<MaintenanceStatus>();
                var createdDate = ctx.Faker.Date.Between(contract.StartDate, DateTime.Now);

                Guid? handlerId = null;
                DateTime? resolvedAt = null;

                if (status != MaintenanceStatus.Open)
                {
                    // Đã phân công hoặc hoàn thành thì có Handler
                    handlerId = ctx.Faker.PickRandom(techStaff).Id;
                }

                if (status == MaintenanceStatus.Resolved || status == MaintenanceStatus.Closed)
                {
                    // Đã hoàn thành thì có ngày xử lý xong
                    resolvedAt = createdDate.AddHours(ctx.Faker.Random.Number(2, 48));
                }

                ctx.MaintenanceRequests.Add(new MaintenanceRequest
                {
                    Id = Guid.NewGuid(),
                    RoomId = bed.RoomId,
                    RequesterId = contract.UserId ?? Guid.Empty,
                    HandlerId = handlerId,
                    Title = template.Title,
                    Description = template.Description,
                    Priority = template.Priority,
                    Status = status,
                    ResolvedAt = resolvedAt,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = createdDate
                });
            }
        }
    }
}
