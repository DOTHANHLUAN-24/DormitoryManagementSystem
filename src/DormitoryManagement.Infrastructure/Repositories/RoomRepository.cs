using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin phòng (Room).
    /// </summary>
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        /// <summary>
        /// Khởi tạo RoomRepository.
        /// </summary>
        /// <param name="db">ApplicationDbContext kết nối database</param>
        public RoomRepository(ApplicationDbContext db) : base(db) { }

        /// <summary>
        /// Tìm kiếm nâng cao: lọc theo tên, tòa nhà, loại phòng, trạng thái và khoảng giá có phân trang.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm theo số phòng</param>
        /// <param name="blockId">Id của tòa nhà (tùy chọn)</param>
        /// <param name="roomTypeId">Id của loại phòng (tùy chọn)</param>
        /// <param name="status">Trạng thái phòng (tùy chọn)</param>
        /// <param name="minPrice">Giá thuê nhỏ nhất (tùy chọn)</param>
        /// <param name="maxPrice">Giá thuê lớn nhất (tùy chọn)</param>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <returns>Kết quả phân trang danh sách phòng</returns>
        public async Task<PagedResult<Room>> SearchRoomsAdvancedAsync(
            string? searchTerm, Guid? blockId, Guid? roomTypeId,
            RoomStatus? status, decimal? minPrice, decimal? maxPrice,
            int pageIndex, int pageSize)
        {
            // Bắt buộc Include RoomType để có thể lọc theo BasePrice
            var query = _dbSet.AsNoTracking()
                .Include(r => r.Block)
                .Include(r => r.RoomType)
                .Where(r => !r.IsDeleted);

            // Lọc theo tên/số phòng
            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(r => r.RoomNumber.Contains(searchTerm));

            // Lọc theo tòa nhà
            if (blockId.HasValue && blockId != Guid.Empty)
                query = query.Where(r => r.BlockId == blockId.Value);

            // Lọc theo loại phòng
            if (roomTypeId.HasValue && roomTypeId != Guid.Empty)
                query = query.Where(r => r.RoomTypeId == roomTypeId.Value);

            // Lọc theo trạng thái
            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            // Lọc theo khoảng giá (Lấy từ bảng RoomType liên kết)
            if (minPrice.HasValue)
                query = query.Where(r => r.RoomType.BasePrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(r => r.RoomType.BasePrice <= maxPrice.Value);

            int totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(r => r.Block.BlockName)
                .ThenBy(r => r.RoomNumber)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Room>(items, totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// Lấy chi tiết phòng bao gồm đầy đủ thông tin Tòa nhà, Loại phòng.
        /// </summary>
        /// <param name="id">Id của phòng</param>
        /// <returns>Thông tin phòng kèm chi tiết liên kết</returns>
        public async Task<Room?> GetRoomWithFullDetailsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(r => r.Block)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        /// <summary>
        /// Kiểm tra trùng số phòng trong cùng một tòa nhà (trừ một Id cụ thể khi cập nhật).
        /// </summary>
        /// <param name="roomNumber">Số phòng cần kiểm tra</param>
        /// <param name="blockId">Id của tòa nhà</param>
        /// <param name="excludeId">Id phòng loại trừ (tùy chọn)</param>
        /// <returns>True nếu trùng số phòng khác, ngược lại là False</returns>
        public async Task<bool> IsRoomNumberDuplicateAsync(string roomNumber, Guid blockId, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(r =>
                r.RoomNumber.ToLower() == roomNumber.ToLower() &&
                r.BlockId == blockId &&
                r.Id != (excludeId ?? Guid.Empty) &&
                !r.IsDeleted);
        }

        /// <summary>
        /// Lấy danh sách phòng thuộc một tòa nhà cụ thể (Dùng cho dropdown/cascading).
        /// </summary>
        /// <param name="blockId">Id của tòa nhà</param>
        /// <returns>Danh sách phòng của tòa nhà đó</returns>
        public async Task<IEnumerable<Room>> GetRoomsByBlockAsync(Guid blockId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(r => r.BlockId == blockId && !r.IsDeleted)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy danh sách phòng trong thùng rác kèm theo thông tin Tòa nhà và Loại phòng để hiển thị.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm theo số phòng (tùy chọn)</param>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <returns>Kết quả phân trang của phòng đã bị xóa mềm</returns>
        public async Task<PagedResult<Room>> GetDeletedRoomsWithDetailsPagedAsync(string? searchTerm, int pageIndex, int pageSize)
        {
            // Sử dụng IgnoreQueryFilters để nhìn thấy dữ liệu trong thùng rác
            var query = _dbSet.IgnoreQueryFilters()
                .AsNoTracking()
                .Include(r => r.Block)
                .Include(r => r.RoomType)
                .Where(r => r.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(r => r.RoomNumber.Contains(searchTerm));

            int totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(r => r.LastModified)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Room>(items, totalCount, pageIndex, pageSize);
        }
    }
}