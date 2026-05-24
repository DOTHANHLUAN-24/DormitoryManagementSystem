using System.Linq.Expressions;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin phương tiện (Vehicle).
    /// </summary>
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        /// <summary>
        /// Khởi tạo VehicleRepository.
        /// </summary>
        /// <param name="db">ApplicationDbContext kết nối database</param>
        public VehicleRepository(ApplicationDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Lấy phương tiện theo Id kèm thông tin chủ sở hữu (Owner) nếu tìm thấy.
        /// </summary>
        public override async Task<Vehicle?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);
        }

        /// <summary>
        /// Lấy phương tiện theo biển số (LicensePlate).
        /// </summary>
        public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                return null;
            }

            return await _dbSet
                .AsNoTracking()
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(v => !v.IsDeleted && v.LicensePlate == licensePlate);
        }

        /// <summary>
        /// Lấy danh sách phương tiện đang hoạt động thuộc về chủ sở hữu (OwnerId).
        /// </summary>
        public async Task<IEnumerable<Vehicle>> GetActiveVehiclesByOwnerIdAsync(Guid ownerId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(v => v.Owner)
                .Where(v => v.OwnerId == ownerId && v.IsActive && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Kiểm tra phương tiện có đang hoạt động và chưa bị xóa không.
        /// </summary>
        public async Task<bool> IsVehicleActiveAsync(Guid vehicleId)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(v => v.Id == vehicleId && v.IsActive && !v.IsDeleted);
        }

        /// <summary>
        /// Kiểm tra trùng biển số trong hệ thống (tránh trùng lặp khi thêm/sửa).
        /// </summary>
        public async Task<bool> IsLicensePlateDuplicateAsync(string licensePlate, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                return false;
            }

            var normalized = licensePlate.Trim().ToLower();

            var exclude = excludeId ?? Guid.Empty;

            return await _dbSet
                .AsNoTracking()
                .AnyAsync(v =>
                    v.LicensePlate.ToLower() == normalized &&
                    v.Id != exclude &&
                    !v.IsDeleted);
        }
    }
}
