using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface repository cho entity Room (Phòng)
    /// </summary>
    public interface IRoomRepository : IBaseRepository<Room>
    {
        /// <summary>
        /// Tìm kiếm nâng cao: lọc theo tên, tòa nhà, loại phòng, trạng thái và KHOẢNG GIÁ.
        /// </summary>
        Task<PagedResult<Room>> SearchRoomsAdvancedAsync(
            string? searchTerm,
            Guid? blockId,
            Guid? roomTypeId,
            RoomStatus? status,
            decimal? minPrice,
            decimal? maxPrice,
            int pageIndex,
            int pageSize);

        /// <summary>
        /// Lấy chi tiết phòng bao gồm đầy đủ thông tin Tòa nhà, Loại phòng, Giường và Tài sản.
        /// </summary>
        Task<Room?> GetRoomWithFullDetailsAsync(Guid id);

        /// <summary>
        /// Kiểm tra trùng số phòng trong cùng một tòa nhà (trừ một Id cụ thể khi cập nhật).
        /// </summary>
        Task<bool> IsRoomNumberDuplicateAsync(string roomNumber, Guid blockId, Guid? excludeId = null);

        /// <summary>
        /// Lấy danh sách phòng thuộc một tòa nhà cụ thể (Dùng cho dropdown/cascading).
        /// </summary>
        Task<IEnumerable<Room>> GetRoomsByBlockAsync(Guid blockId);

        /// <summary>
        /// Lấy danh sách phòng trong thùng rác kèm theo thông tin Tòa nhà và Loại phòng để hiển thị.
        /// </summary>
        Task<PagedResult<Room>> GetDeletedRoomsWithDetailsPagedAsync(
            string? searchTerm,
            int pageIndex,
            int pageSize);
    }
}