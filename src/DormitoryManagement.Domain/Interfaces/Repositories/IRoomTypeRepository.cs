using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IRoomTypeRepository : IBaseRepository<RoomType>
    {
        /// <summary>
        /// Kiểm tra tên loại phòng đã tồn tại chưa?
        /// </summary>
        Task<bool> IsTypeNameDuplicateAsync(string typeName, Guid? excludeId = null);

        /// <summary>
        /// Lấy chi tiết loại phòng kèm danh sách các phòng thuộc loại đó.
        /// </summary>
        Task<RoomType?> GetRoomTypeWithRoomsAsync(Guid id);

        /// <summary>
        /// Thống kê số lượng phòng cho mỗi loại phòng (dùng cho dashboard)
        /// </summary>
        Task<Dictionary<string, int>> GetRoomCountByTypeAsync();
    }
}