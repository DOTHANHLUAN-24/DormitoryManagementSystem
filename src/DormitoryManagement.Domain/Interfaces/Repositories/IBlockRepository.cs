using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBlockRepository : IBaseRepository<Block>
    {
        /// <summary>
        /// Lấy thông tin tòa nhà kèm theo danh sách phòng chưa bị xóa
        /// </summary>
        Task<Block?> GetBlockWithRoomsAsync(Guid id);

        /// <summary>
        /// Kiểm tra tên tòa nhà đã tồn tại chưa (để tránh trùng lặp khi thêm/sửa)
        /// </summary>
        Task<bool> IsBlockNameExistsAsync(string blockName, Guid? excludeId = null);

        /// <summary>
        /// Tìm kiếm tòa nhà theo tên hoặc mô tả, hỗ trợ phân trang
        /// </summary>
        Task<PagedResult<Block>> SearchBlocksAsync(string searchTerm, int pageIndex, int pageSize);
    }
}
