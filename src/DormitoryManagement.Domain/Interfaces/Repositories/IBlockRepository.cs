using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBlockRepository : IBaseRepository<Block>
    {
        // Lấy thông tin Block kèm theo danh sách phòng (Rooms)
        Task<Block?> GetBlockWithRoomsAsync(Guid id);

        // Kiểm tra tên Block đã tồn tại chưa (để tránh trùng tên khi tạo mới/cập nhật)
        Task<bool> IsBlockNameExistsAsync(string blockName, Guid? excludeId = null);

        // Lấy danh sách Block kèm số lượng phòng hiện có (Ví dụ cho dashboard/thống kê)
        Task<IEnumerable<Block>> GetAllWithRoomCountAsync();

        // Tìm kiếm Block
        Task<PagedResult<Block>> SearchBlocksAsync(string searchTerm, int pageIndex, int pageSize);
    }
}
