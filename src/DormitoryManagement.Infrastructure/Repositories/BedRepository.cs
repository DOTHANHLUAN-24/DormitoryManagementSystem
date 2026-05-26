using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin giường (Bed).
    /// </summary>
    public class BedRepository(ApplicationDbContext db) : BaseRepository<Bed>(db), IBedRepository
    {

        /// <summary>
        /// Lấy thông tin giường theo Id kèm thông tin Phòng (Room).
        /// </summary>
        /// <param name="id">Id của giường</param>
        /// <returns>Giường nếu tìm thấy, ngược lại là null</returns>
        public override async Task<Bed?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(b => b.Room)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        /// <summary>
        /// Lấy tất cả giường kèm thông tin Phòng.
        /// </summary>
        /// <param name="includeDeleted">Có bao gồm giường đã bị xóa mềm không</param>
        /// <returns>Danh sách giường</returns>
        public override async Task<IEnumerable<Bed>> GetAllAsync(bool includeDeleted = false)
        {
            var query = _dbSet
                .Include(b => b.Room)
                .AsNoTracking();

            if (!includeDeleted)
            {
                query = query.Where(b => !b.IsDeleted);
            }

            return await query
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy giường theo số giường (BedNumber).
        /// </summary>
        /// <param name="bedNumber">Số giường cần tìm</param>
        /// <returns>Giường nếu tìm thấy, ngược lại là null</returns>
        public async Task<Bed?> GetByBedNumberAsync(string bedNumber)
        {
            if (string.IsNullOrEmpty(bedNumber))
            {
                return null;
            }

            return await _dbSet
                .AsNoTracking()
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.BedNumber == bedNumber);
        }

        /// <summary>
        /// Lấy danh sách giường trống thuộc về một phòng.
        /// </summary>
        /// <param name="roomId">Id của phòng cần kiểm tra</param>
        /// <returns>Danh sách giường trống</returns>
        public async Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(b => b.RoomId == roomId && !b.IsDeleted && b.IsActive)
                .ToListAsync();
        }

        /// <summary>
        /// Kiểm tra xem giường có còn trống (đang hoạt động và chưa bị xóa) hay không.
        /// </summary>
        /// <param name="bedId">Id của giường cần kiểm tra</param>
        /// <returns>True nếu giường còn trống, ngược lại là False</returns>
        public async Task<bool> IsBedAvailableAsync(Guid bedId)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(b => b.Id == bedId && !b.IsDeleted && b.IsActive);
        }
    }
}