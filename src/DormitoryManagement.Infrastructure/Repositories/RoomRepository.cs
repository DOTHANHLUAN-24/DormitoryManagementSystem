using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext db) : base(db) { }

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

        public async Task<Room?> GetRoomWithFullDetailsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(r => r.Block)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<bool> IsRoomNumberDuplicateAsync(string roomNumber, Guid blockId, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(r =>
                r.RoomNumber.ToLower() == roomNumber.ToLower() &&
                r.BlockId == blockId &&
                r.Id != (excludeId ?? Guid.Empty) &&
                !r.IsDeleted);
        }

        public async Task<IEnumerable<Room>> GetRoomsByBlockAsync(Guid blockId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(r => r.BlockId == blockId && !r.IsDeleted)
                .ToListAsync();
        }

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