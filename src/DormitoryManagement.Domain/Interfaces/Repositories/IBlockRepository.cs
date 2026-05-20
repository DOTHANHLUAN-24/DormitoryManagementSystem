using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin tòa nhà (Block).
    /// </summary>
    public interface IBlockRepository : IBaseRepository<Block>
    {
        /// <summary>
        /// Lấy thông tin tòa nhà kèm theo danh sách phòng chưa bị xóa.
        /// </summary>
        /// <param name="id">Id của tòa nhà</param>
        /// <returns>Tòa nhà kèm danh sách phòng, ngược lại là null</returns>
        Task<Block?> GetBlockWithRoomsAsync(Guid id);

        /// <summary>
        /// Kiểm tra tên tòa nhà đã tồn tại chưa (để tránh trùng lặp khi thêm/sửa).
        /// Chỉ kiểm tra các tòa nhà chưa bị xóa mềm.
        /// </summary>
        /// <param name="blockName">Tên tòa nhà cần kiểm tra</param>
        /// <param name="excludeId">Id tòa nhà loại trừ khi kiểm tra (dùng cho cập nhật, tùy chọn)</param>
        /// <returns>True nếu tên tòa nhà đã tồn tại, ngược lại là False</returns>
        Task<bool> IsBlockNameExistsAsync(string blockName, Guid? excludeId = null);
    }
}
