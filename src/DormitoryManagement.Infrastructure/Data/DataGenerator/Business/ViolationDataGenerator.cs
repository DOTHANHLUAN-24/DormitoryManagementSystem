using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Business
{
    public static class ViolationDataGenerator
    {
        private static readonly (string Description, decimal Fine)[] ViolationTemplates =
        {
            ("Sử dụng bếp từ công suất lớn nấu ăn trong phòng gây nguy cơ quá tải điện", 200000m),
            ("Trở về ký túc xá muộn sau giờ giới nghiêm (sau 23:00) không có lý do chính đáng", 100000m),
            ("Gây ồn ào mất trật tự trong giờ tự học (sau 22:00) ảnh hưởng đến phòng xung quanh", 50000m),
            ("Không tham gia buổi dọn dẹp vệ sinh chung của khu nhà theo phân công", 50000m),
            ("Để phương tiện sai nơi quy định trong nhà xe", 50000m),
            ("Tự ý cho người ngoài ngủ qua đêm trong phòng mà không khai báo với ban quản lý", 300000m)
        };

        public static void Generate(SeedContext ctx)
        {
            var contracts = ctx.Contracts.ToList();
            if (!contracts.Any()) return;

            // Seed vi phạm cho khoảng 20% hợp đồng
            var count = (int)Math.Ceiling(contracts.Count * 0.2);
            var selectedContracts = ctx.Faker.Random.ListItems(contracts, count);

            foreach (var contract in selectedContracts)
            {
                var template = ctx.Faker.PickRandom(ViolationTemplates);
                var status = ctx.Faker.PickRandom<ViolationStatus>();
                var date = ctx.Faker.Date.Between(contract.StartDate, DateTime.Now);

                ctx.Violations.Add(new Violation
                {
                    Id = Guid.NewGuid(),
                    ContractId = contract.Id,
                    Description = template.Description,
                    FineAmount = template.Fine,
                    ViolationDate = date,
                    Status = status,
                    EvidenceImage = "/images/violations/default_evidence.jpg", // Đường dẫn ảnh mẫu
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = date
                });
            }
        }
    }
}
