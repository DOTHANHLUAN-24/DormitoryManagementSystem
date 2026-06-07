using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin loại phòng (RoomType).
    /// </summary>
    public class RoomTypeRepository(ApplicationDbContext db) : BaseRepository<RoomType>(db), IRoomTypeRepository
    {

        /// <summary>
        /// Kiểm tra tên loại phòng đã tồn tại chưa (tránh trùng lặp khi tạo/sửa).
        /// </summary>
        /// <param name="typeName">Tên loại phòng cần kiểm tra</param>
        /// <param name="excludeId">Id loại phòng loại trừ khi kiểm tra (dùng cho cập nhật, tùy chọn)</param>
        /// <returns>True nếu đã tồn tại tên loại phòng này, ngược lại là False</returns>
        public async Task<bool> IsTypeNameDuplicateAsync(string typeName, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(rt =>
                rt.TypeName.ToLower() == typeName.ToLower() &&
                rt.Id != excludeId &&
                !rt.IsDeleted);
        }

        /// <summary>
        /// Lấy chi tiết loại phòng kèm danh sách các phòng thuộc loại đó.
        /// </summary>
        /// <param name="id">Id của loại phòng</param>
        /// <returns>Loại phòng kèm danh sách phòng, ngược lại là null</returns>
        public async Task<RoomType?> GetRoomTypeWithRoomsAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(rt => rt.Rooms.Where(r => !r.IsDeleted))
                    .ThenInclude(r => r.Beds)
                .Include(rt => rt.Rooms.Where(r => !r.IsDeleted))
                    .ThenInclude(r => r.Block)
                .FirstOrDefaultAsync(rt => rt.Id == id && !rt.IsDeleted);
        }

        /// <summary>
        /// Thống kê số lượng phòng cho mỗi loại phòng (dùng cho dashboard).
        /// </summary>
        /// <returns>Từ điển chứa tên loại phòng và số lượng phòng tương ứng</returns>
        public async Task<Dictionary<string, int>> GetRoomCountByTypeAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(rt => !rt.IsDeleted)
                .Select(rt => new
                {
                    rt.TypeName,
                    Count = rt.Rooms.Count(r => !r.IsDeleted)
                })
                .ToDictionaryAsync(x => x.TypeName, x => x.Count);
        }
    }
}