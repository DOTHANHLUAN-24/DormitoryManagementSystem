using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin hợp đồng thuê phòng (Contract).
    /// </summary>
    public class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
        /// <summary>
        /// Khởi tạo ContractRepository.
        /// </summary>
        /// <param name="db">ApplicationDbContext kết nối database</param>
        public ContractRepository(ApplicationDbContext db) : base(db)
        {
        }

        /// <summary>
        /// Tìm kiếm hợp đồng theo mã hợp đồng (ContractCode).
        /// </summary>
        /// <param name="contractCode">Mã hợp đồng cần tìm</param>
        /// <returns>Hợp đồng kèm thông tin User và Bed nếu tìm thấy, ngược lại là null</returns>
        public async Task<Contract?> GetByContractCodeAsync(string contractCode)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.Bed)
                .FirstOrDefaultAsync(c => c.ContractCode == contractCode);
        }

        /// <summary>
        /// Lấy đối tượng truy vấn phân trang và tìm kiếm hợp đồng theo từ khóa.
        /// </summary>
        /// <param name="searchString">Từ khóa tìm kiếm</param>
        /// <returns>Đối tượng IQueryable chứa danh sách hợp đồng</returns>
        public IQueryable<Contract> GetPagingQuery(string searchString)
        {
            var query = _dbSet
                .Include(c => c.User)
                .Include(c => c.Bed)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(
                    c => c.ContractCode.Contains(searchString) ||
                         (c.User != null && c.User.UserName!.Contains(searchString))
                );
            }

            return query.OrderByDescending(c => c.CreatedDate);
        }

        /// <summary>
        /// Lấy danh sách hợp đồng của một người dùng (sinh viên).
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <returns>Danh sách hợp đồng</returns>
        public async Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.Bed)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Lấy hợp đồng đang hoạt động liên kết với một giường cụ thể.
        /// </summary>
        /// <param name="bedId">Id của giường cần kiểm tra</param>
        /// <returns>Thông tin hợp đồng hoặc thực thể hợp đồng mới nếu null</returns>
        public async Task<Contract> GetByBedIdAsync(Guid bedId)
        {
            var contract = await _dbSet
                .AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.Bed)
                .FirstOrDefaultAsync(c => c.BedId == bedId);

            // Interface yêu cầu trả về Task<Contract> chứ không phải Task<Contract?>
            // Vì vậy nếu null thì khởi tạo hợp đồng rỗng để tránh Warning/Error
            return contract ?? new Contract();
        }
    }
}
