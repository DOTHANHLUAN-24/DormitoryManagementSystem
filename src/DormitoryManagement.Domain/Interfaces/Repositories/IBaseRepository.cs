using System.Linq.Expressions;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        // Cơ bản cần có
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false);

        // Tìm kiếm linh hoạt
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Phân trang dùng chung (Sẽ giải quyết được vấn đề GetActiveUsersPagedAsync)
        Task<PagedResult<T>> GetPagedAsync(
           int pageIndex,
           int pageSize,
           Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        // Kiểm tra tồn tại
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // CRUD
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity, bool isSoftDelete = true); // Hỗ trợ cả xóa cứng và xóa mềm
        Task DeleteRangeAsync(IEnumerable<T> entities, bool isSoftDelete = true);

        // Để kế thừa cho các query phức tạp ở lớp trên
        IQueryable<T> GetQuery();

        Task<IEnumerable<T>> GetByStatusAsync(bool? isActive = null, bool? isDeleted = null);

        // Khôi phục một Entity đã bị xóa mềm
        Task RestoreAsync(T entity);

        // Lấy danh sách phân trang theo trạng thái (Dùng cho cả Active, Ban, Deleted)
        Task<PagedResult<T>> GetByStatusPagedAsync(
            int pageIndex,
            int pageSize,
            bool? isActive = null,
            bool? isDeleted = null,
            Expression<Func<T, bool>>? predicate = null);
    }
}
