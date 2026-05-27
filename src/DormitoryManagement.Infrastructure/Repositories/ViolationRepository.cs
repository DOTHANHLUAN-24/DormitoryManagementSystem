using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin vi phạm kỷ luật (Violation).
    /// </summary>
    public class ViolationRepository(ApplicationDbContext db) : BaseRepository<Violation>(db), IViolationRepository
    {

        /// <summary>
        /// Lấy thông tin vi phạm theo Id (chỉ lấy vi phạm chưa bị xóa).
        /// </summary>
        /// <param name="id">Id của vi phạm</param>
        /// <returns>Vi phạm nếu tìm thấy, ngược lại là null</returns>
        public override async Task<Violation?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        /// <summary>
        /// Lấy thông tin vi phạm dựa trên đường dẫn ảnh minh chứng.
        /// </summary>
        /// <param name="evidenceImage">Đường dẫn ảnh minh chứng</param>
        /// <returns>Vi phạm kèm thông tin Hợp đồng nếu tìm thấy, ngược lại là null</returns>
        public async Task<Violation?> GetByEvidenceImageAsync(string evidenceImage)
        {
            if (string.IsNullOrEmpty(evidenceImage))
            {
                return null;
            }
            return await _dbSet
                .AsNoTracking()
                .Include(v => v.Contract)
                .FirstOrDefaultAsync(v => !v.IsDeleted && v.EvidenceImage == evidenceImage);
        }

        /// <summary>
        /// Lấy danh sách vi phạm thuộc một hợp đồng thuê phòng.
        /// </summary>
        /// <param name="contractId">Id hợp đồng</param>
        /// <returns>Danh sách các vi phạm liên quan</returns>
        public async Task<IEnumerable<Violation>> GetViolationsByContractIdAsync(Guid contractId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(v => v.Contract)
                .Where(v => !v.IsDeleted && v.ContractId == contractId)
                .OrderByDescending(v => v.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Kiểm tra xem vi phạm đã được giải quyết / xử lý xong chưa.
        /// </summary>
        /// <param name="violationId">Id vi phạm</param>
        /// <returns>True nếu vi phạm ở trạng thái Resolved và chưa bị xóa, ngược lại là False</returns>
        public async Task<bool> IsViolationResolvedAsync(Guid violationId)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(v => v.Id == violationId && !v.IsDeleted && v.Status == ViolationStatus.Resolved);
        }

        /// <summary>
        /// Lọc danh sách vi phạm theo trạng thái xử lý.
        /// </summary>
        /// <param name="status">Trạng thái xử lý vi phạm</param>
        /// <returns>Danh sách vi phạm</returns>
        public async Task<IEnumerable<Violation>> GetViolationsByStatusAsync(ViolationStatus status)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(v => !v.IsDeleted && v.Status == status)
                .OrderByDescending(v => v.CreatedDate)
                .ToListAsync();
        }
    }
}
