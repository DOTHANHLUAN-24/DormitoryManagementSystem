using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Business
{
    public static class AssetDataGenerator
    {
        private static readonly (string Name, decimal Cost)[] AssetTemplates =
        {
            ("Điều hòa Panasonic 12000 BTU", 8500000m),
            ("Quạt trần Điện Cơ Thống Nhất", 800000m),
            ("Bàn học gỗ ép Hòa Phát", 1200000m),
            ("Ghế xoay Hòa Phát", 450000m),
            ("Tủ quần áo sắt 4 ngăn", 2500000m),
            ("Đèn tuýp LED Rạng Đông", 150000m),
            ("Bình nóng lạnh Rossi 20L", 3200000m),
            ("Vòi hoa sen tắm Inox 304", 600000m)
        };

        public static void Generate(SeedContext ctx)
        {
            foreach (var room in ctx.Rooms)
            {
                // Chọn ngẫu nhiên khoảng 5-8 tài sản cho mỗi phòng
                var count = ctx.Faker.Random.Number(5, AssetTemplates.Length);
                var selectedTemplates = ctx.Faker.Random.ListItems(AssetTemplates.ToList(), count);

                for (int i = 0; i < selectedTemplates.Count; i++)
                {
                    var template = selectedTemplates[i];

                    // 85% cơ bản là hoạt động tốt, còn lại là lỗi hoặc cần sửa chữa
                    var statusChance = ctx.Faker.Random.Double();
                    var status = statusChance switch
                    {
                        < 0.85 => AssetStatus.Good,
                        < 0.92 => AssetStatus.Broken,
                        < 0.97 => AssetStatus.UnderRepair,
                        _ => AssetStatus.Lost
                    };

                    ctx.Assets.Add(new Asset
                    {
                        Id = Guid.NewGuid(),
                        RoomId = room.Id,
                        AssetName = template.Name,
                        AssetCode = $"TS-{room.RoomNumber}-{i + 1:D2}",
                        Description = $"Tài sản trang bị cho phòng {room.RoomNumber}",
                        Status = status,
                        ReplacementCost = template.Cost,
                        IsActive = true,
                        IsDeleted = false
                    });
                }
            }
        }
    }
}
