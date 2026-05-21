using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Core
{
    public static class RoomTypeDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            ctx.RoomTypes = new List<RoomType>
            {
                new RoomType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Phòng 2 sinh viên",
                    BasePrice = 1575000m,
                    MaxOccupants = 2,
                    Description = "Phòng tiêu chuẩn dịch vụ cao cấp dành cho 2 người, trang bị đầy đủ tiện nghi, máy lạnh và tủ cá nhân.",
                    IsActive = true
                },
                new RoomType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Phòng 4 sinh viên",
                    BasePrice = 800000m,
                    MaxOccupants = 4,
                    Description = "Phòng tiêu chuẩn trung bình dành cho 4 người, thoáng mát, yên tĩnh, có điều hòa.",
                    IsActive = true
                },
                new RoomType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Phòng 6 sinh viên",
                    BasePrice = 275000m,
                    MaxOccupants = 6,
                    Description = "Phòng tiêu chuẩn tiết kiệm dành cho 6 người, tối ưu hóa không gian sinh hoạt học tập.",
                    IsActive = true
                }, 
                new RoomType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Phòng 8 sinh viên",
                    BasePrice = 205000m,
                    MaxOccupants = 8,
                    Description = "Phòng phổ thông giá rẻ nhất dành cho 8 người, phù hợp với sinh viên muốn tối ưu chi phí sinh hoạt.",
                    IsActive = true
                }
            };
        }
    }
}
