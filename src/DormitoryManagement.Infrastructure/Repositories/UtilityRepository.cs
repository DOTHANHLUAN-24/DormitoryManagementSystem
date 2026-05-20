using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin dịch vụ / tiện ích (Utility).
    /// </summary>
    public class UtilityRepository : BaseRepository<Utility>, IUtilityRepository
    {
        /// <summary>
        /// Khởi tạo UtilityRepository.
        /// </summary>
        /// <param name="db">ApplicationDbContext kết nối database</param>
        public UtilityRepository(ApplicationDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Lấy dịch vụ theo Id (chỉ lấy dịch vụ chưa bị xóa).
        /// </summary>
        /// <param name="id">Id của dịch vụ</param>
        /// <returns>Dịch vụ nếu tìm thấy, ngược lại là null</returns>
        public override async Task<Utility?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        /// <summary>
        /// Lấy dịch vụ theo tên tiện ích (UtilityName).
        /// </summary>
        /// <param name="utilityName">Tên tiện ích</param>
        /// <returns>Dịch vụ nếu tìm thấy, ngược lại là null</returns>
        public async Task<Utility?> GetByUtilityNameAsync(string utilityName)
        {
            if (string.IsNullOrWhiteSpace(utilityName))
            {
                return null;
            }

            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => !u.IsDeleted && u.UtilityName == utilityName);
        }

        /// <summary>
        /// Lấy danh sách dịch vụ đang hoạt động.
        /// </summary>
        /// <returns>Danh sách các tiện ích/dịch vụ</returns>
        public async Task<IEnumerable<Utility>> GetActiveUtilitiesAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(u => u.IsActive && !u.IsDeleted)
                .OrderByDescending(u => u.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Kiểm tra tiện ích có đang hoạt động hay không.
        /// </summary>
        /// <param name="utilityId">Id tiện ích</param>
        /// <returns>True nếu hoạt động và chưa bị xóa, ngược lại là False</returns>
        public async Task<bool> IsUtilityActiveAsync(Guid utilityId)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(u => u.Id == utilityId && u.IsActive && !u.IsDeleted);
        }
    }
}
