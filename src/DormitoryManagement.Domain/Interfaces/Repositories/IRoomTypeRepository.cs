using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin loại phòng (RoomType).
    /// </summary>
    public interface IRoomTypeRepository : IBaseRepository<RoomType>
    {
        /// <summary>
        /// Kiểm tra tên loại phòng đã tồn tại chưa (tránh trùng lặp khi tạo/sửa).
        /// </summary>
        /// <param name="typeName">Tên loại phòng cần kiểm tra</param>
        /// <param name="excludeId">Id loại phòng loại trừ khi kiểm tra (dùng cho cập nhật, tùy chọn)</param>
        /// <returns>True nếu đã tồn tại tên loại phòng này, ngược lại là False</returns>
        Task<bool> IsTypeNameDuplicateAsync(string typeName, Guid? excludeId = null);

        /// <summary>
        /// Lấy chi tiết loại phòng kèm danh sách các phòng thuộc loại đó.
        /// </summary>
        /// <param name="id">Id của loại phòng</param>
        /// <returns>Loại phòng kèm danh sách phòng, ngược lại là null</returns>
        Task<RoomType?> GetRoomTypeWithRoomsAsync(Guid id);

        /// <summary>
        /// Thống kê số lượng phòng cho mỗi loại phòng (dùng cho dashboard).
        /// </summary>
        /// <returns>Từ điển chứa tên loại phòng và số lượng phòng tương ứng</returns>
        Task<Dictionary<string, int>> GetRoomCountByTypeAsync();
    }
}