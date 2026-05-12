using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IRoomTypeRepository : IBaseRepository<RoomType>
    {
        // Kiểm tra tên loại phòng đã tồn tại chưa (để tránh trùng lặp)
        Task<bool> IsTypeNameDuplicateAsync(string typeName, Guid? excludeId = null);

        // Lấy chi tiết loại phòng bao gồm danh sách các phòng thuộc loại đó
        Task<RoomType?> GetRoomTypeWithRoomsAsync(Guid id);

        // Thống kê số lượng phòng theo từng loại
        Task<Dictionary<string, int>> GetRoomCountByTypeAsync();
    }
}