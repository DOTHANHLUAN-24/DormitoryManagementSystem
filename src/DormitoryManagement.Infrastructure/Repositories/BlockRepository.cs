using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin tòa nhà (Block).
    /// </summary>
    public class BlockRepository(ApplicationDbContext db) : BaseRepository<Block>(db), IBlockRepository
    {

        /// <summary>
        /// Lấy thông tin tòa nhà kèm theo danh sách phòng chưa bị xóa.
        /// </summary>
        /// <param name="id">Id của tòa nhà</param>
        /// <returns>Tòa nhà kèm danh sách phòng, ngược lại là null</returns>
        public async Task<Block?> GetBlockWithRoomsAsync(Guid id)
        {
            return await _dbSet
                .Include(b => b.Rooms.Where(r => !r.IsDeleted)) // Chỉ lấy các phòng chưa xóa
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        /// <summary>
        /// Kiểm tra tên tòa nhà đã tồn tại chưa (để tránh trùng lặp khi thêm/sửa).
        /// Chỉ kiểm tra các tòa nhà chưa bị xóa mềm.
        /// </summary>
        /// <param name="blockName">Tên tòa nhà cần kiểm tra</param>
        /// <param name="excludeId">Id tòa nhà loại trừ khi kiểm tra (dùng cho cập nhật, tùy chọn)</param>
        /// <returns>True nếu tên tòa nhà đã tồn tại, ngược lại là False</returns>
        public async Task<bool> IsBlockNameExistsAsync(string blockName, Guid? excludeId = null)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(x => !x.IsDeleted && x.BlockName.ToLower() == blockName.ToLower());

            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
