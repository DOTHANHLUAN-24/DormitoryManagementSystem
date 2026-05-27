using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin tài sản (Asset).
    /// </summary>
    public class AssetRepository(ApplicationDbContext db) : BaseRepository<Asset>(db), IAssetRepository
    {

        /// <summary>
        /// Lấy thông tin tài sản theo Id kèm thông tin Phòng (Room) và Tòa (Block).
        /// </summary>
        /// <param name="id">Id của tài sản</param>
        /// <returns>Tài sản kèm thông tin chi tiết liên kết nếu tìm thấy, ngược lại là null</returns>
        public override async Task<Asset?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Room)
                    .ThenInclude(r => r.Block)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        /// <summary>
        /// Lấy thông tin tài sản theo mã tài sản (AssetCode) kèm thông tin Phòng và Tòa.
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần tìm</param>
        /// <returns>Tài sản nếu tìm thấy, ngược lại là null</returns>
        public async Task<Asset?> GetByAssetCodeAsync(string assetCode)
        {
            if (string.IsNullOrWhiteSpace(assetCode))
            {
                return null;
            }

            return await _dbSet
                .AsNoTracking()
                .Include(a => a.Room)
                    .ThenInclude(r => r.Block)
                .FirstOrDefaultAsync(a => !a.IsDeleted && a.AssetCode == assetCode);
        }

        /// <summary>
        /// Lấy danh sách tài sản đang hoạt động thuộc về một phòng.
        /// </summary>
        /// <param name="roomId">Id của phòng cần kiểm tra</param>
        /// <returns>Danh sách tài sản</returns>
        public async Task<IEnumerable<Asset>> GetActiveAssetsByRoomIdAsync(Guid roomId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(a => a.RoomId == roomId && a.IsActive && !a.IsDeleted)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Kiểm tra xem tài sản có đang hoạt động và chưa bị xóa không.
        /// </summary>
        /// <param name="assetId">Id của tài sản</param>
        /// <returns>True nếu tài sản đang hoạt động và chưa bị xóa, ngược lại là False</returns>
        public async Task<bool> IsAssetActiveAsync(Guid assetId)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(a => a.Id == assetId && a.IsActive && !a.IsDeleted);
        }

        /// <summary>
        /// Lọc danh sách tài sản theo trạng thái tài sản (Tốt, Hỏng, Đang sửa, Mất).
        /// </summary>
        /// <param name="status">Trạng thái tài sản cần lọc</param>
        /// <returns>Danh sách tài sản</returns>
        public async Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.Status == status)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Phân trang danh sách tài sản kèm theo thông tin phòng (Room) và tòa nhà (Block), hỗ trợ tìm kiếm và lọc.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm theo tên hoặc mã tài sản (tùy chọn)</param>
        /// <param name="status">Lọc theo trạng thái tài sản (tùy chọn)</param>
        /// <param name="roomId">Lọc theo phòng (tùy chọn)</param>
        /// <returns>Kết quả phân trang của danh sách tài sản</returns>
        public async Task<PagedResult<Asset>> GetAssetsWithDetailsPagedAsync(
            int pageIndex, int pageSize, string? searchTerm = null, AssetStatus? status = null, Guid? roomId = null)
        {
            var query = _dbSet.AsNoTracking()
                .Include(a => a.Room)
                    .ThenInclude(r => r.Block)
                .Where(a => !a.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a => a.AssetName.Contains(searchTerm) || a.AssetCode.Contains(searchTerm));
            }

            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }

            if (roomId.HasValue && roomId != Guid.Empty)
            {
                query = query.Where(a => a.RoomId == roomId.Value);
            }

            int totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(a => a.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Asset>(items, totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// Kiểm tra trùng mã tài sản trong hệ thống (tránh trùng lặp khi thêm/sửa).
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần kiểm tra</param>
        /// <param name="excludeId">Id tài sản loại trừ khi kiểm tra (dùng cho cập nhật, tùy chọn)</param>
        /// <returns>True nếu trùng mã tài sản khác, ngược lại là False</returns>
        public async Task<bool> IsAssetCodeDuplicateAsync(string assetCode, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(assetCode)) return false;

            return await _dbSet
                .AsNoTracking()
                .AnyAsync(a => a.AssetCode.ToLower() == assetCode.ToLower() &&
                               a.Id != (excludeId ?? Guid.Empty) &&
                               !a.IsDeleted);
        }

        /// <summary>
        /// Phân trang danh sách tài sản đã bị xóa mềm (thùng rác) kèm chi tiết phòng và tòa.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm theo tên hoặc mã (tùy chọn)</param>
        /// <returns>Kết quả phân trang của tài sản đã xóa</returns>
        public async Task<PagedResult<Asset>> GetDeletedAssetsWithDetailsPagedAsync(
            int pageIndex, int pageSize, string? searchTerm = null)
        {
            var query = _dbSet.IgnoreQueryFilters()
                .AsNoTracking()
                .Include(a => a.Room)
                    .ThenInclude(r => r.Block)
                .Where(a => a.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a => a.AssetName.Contains(searchTerm) || a.AssetCode.Contains(searchTerm));
            }

            int totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(a => a.LastModified)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Asset>(items, totalCount, pageIndex, pageSize);
        }
    }
}
