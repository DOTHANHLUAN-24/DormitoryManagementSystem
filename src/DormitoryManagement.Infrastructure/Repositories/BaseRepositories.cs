using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Interfaces.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository cơ bản cung cấp các thao tác CRUD và truy vấn cho entity.
    /// Áp dụng cho các entity có hỗ trợ audit (IAuditableEntity).
    /// </summary>
    /// <typeparam name="T">Kiểu entity</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IAuditableEntity
    {
        /// <summary>
        /// DbContext dùng để truy cập database
        /// </summary>
        protected readonly ApplicationDbContext _db;

        /// <summary>
        /// DbSet tương ứng với entity T, giúp thao tác trực tiếp trên bảng dữ liệu của T
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Khởi tạo repository với DbContext
        /// </summary>
        /// <param name="db">ApplicationDbContext</param>
        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        /// <summary>
        /// Lấy IQueryable để thực hiện truy vấn tùy chỉnh (không tracking)
        /// </summary>
        /// <returns>IQueryable của entity</returns>
        public virtual IQueryable<T> GetQuery() => _dbSet.AsNoTracking();

        /// <summary>
        /// Lấy entity theo Id
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>Entity nếu tìm thấy, ngược lại null</returns>
        public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        /// <summary>
        /// Lấy tất cả entity
        /// </summary>
        /// <param name="includeDeleted">Có bao gồm dữ liệu đã xóa mềm hay không</param>
        /// <returns>Danh sách entity</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false)
        {
            if (includeDeleted) return await _dbSet.AsNoTracking().ToListAsync();
            return await _dbSet.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();
        }

        /// <summary>
        /// Tìm kiếm entity theo điều kiện
        /// </summary>
        /// <param name="predicate">Biểu thức điều kiện</param>
        /// <returns>Danh sách entity thỏa mãn điều kiện</returns>
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Kiểm tra có tồn tại entity thỏa mãn điều kiện hay không
        /// </summary>
        /// <param name="predicate">Biểu thức điều kiện</param>
        /// <returns>True nếu tồn tại, ngược lại False</returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        /// <summary>
        /// Lấy dữ liệu dưới dạng phân trang, hỗ trợ lọc và sắp xếp linh hoạt.
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại (bắt đầu từ 1)</param>
        /// <param name="pageSize">Số lượng bản ghi mỗi trang</param>
        /// <param name="predicate">Điều kiện lọc (optional)</param>
        /// <param name="orderBy">Hàm sắp xếp (optional)</param>
        /// <returns>Đối tượng PagedResult chứa dữ liệu và metadata</returns>
        public virtual async Task<PagedResult<T>> GetPagedAsync(
             int pageIndex,
             int pageSize,
             Expression<Func<T, bool>>? predicate = null,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            // Lọc theo điều kiện nếu có
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Đếm tổng số bản ghi
            int totalCount = await query.CountAsync();

            // Sắp xếp theo cái gì đó nếu có, nếu không thì mặc định sắp xếp theo CreatedDate giảm dần
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedDate);
            }

            // Phân trang và lấy dữ liệu
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Trả về object PagedResult chứa dữ liệu và thông tin phân trang
            return new PagedResult<T>(items, totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// Thêm entity mới
        /// </summary>
        /// <param name="entity">Entity cần thêm</param>
        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        /// <summary>
        /// Thêm nhiều entity
        /// </summary>
        /// <param name="entities">Danh sách entity cần thêm</param>
        public virtual async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);

        /// <summary>
        /// Cập nhật entity
        /// </summary>
        /// <param name="entity">Entity cần cập nhật</param>
        public virtual Task UpdateAsync(T entity)
        {
            entity.LastModified = DateTime.UtcNow;
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Xóa entity (soft delete hoặc hard delete)
        /// </summary>
        /// <param name="entity">Entity cần xóa</param>
        /// <param name="isSoftDelete">True: xóa mềm, False: xóa cứng</param>
        public virtual Task DeleteAsync(T entity, bool isSoftDelete = true)
        {
            if (isSoftDelete)
            {
                entity.IsDeleted = true;
                entity.LastModified = DateTime.UtcNow;
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Xóa nhiều entity (soft delete hoặc hard delete)
        /// </summary>
        /// <param name="entities">Danh sách entity</param>
        /// <param name="isSoftDelete">True: xóa mềm, False: xóa cứng</param>
        public virtual Task DeleteRangeAsync(IEnumerable<T> entities, bool isSoftDelete = true)
        {
            if (isSoftDelete)
            {
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                    entity.LastModified = DateTime.UtcNow;
                }
                _dbSet.UpdateRange(entities);
            }
            else
            {
                _dbSet.RemoveRange(entities);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Láy danh sách entity theo trạng thái IsActive và IsDeleted, hỗ trợ lọc theo từng trạng thái hoặc cả hai cùng lúc.
        /// </summary>
        /// <param name="isActive">Có được kích hoạt không</param>
        /// <param name="isDeleted">Có bị xóa mềm không</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetByStatusAsync(bool? isActive = null, bool? isDeleted = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            // Nếu truyền isActive (true/false) thì lọc theo IsActive
            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive.Value);
            }

            // Nếu truyền isDeleted (true/false) thì lọc theo IsDeleted
            if (isDeleted.HasValue)
            {
                query = query.Where(x => x.IsDeleted == isDeleted.Value);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Khôi phục một entity đã bị xóa mềm (IsDeleted = true) về trạng thái bình thường (IsDeleted = false).
        /// </summary>
        /// <param name="entity">Đối tượng cần khôi phục</param>
        /// <returns>Kết quả khi khôi phục</returns>
        public virtual async Task RestoreAsync(T entity)
        {
            entity.IsDeleted = false;
            entity.LastModified = DateTime.UtcNow;
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task<PagedResult<T>> GetByStatusPagedAsync(
            int pageIndex,
            int pageSize,
            bool? isActive = null,
            bool? isDeleted = null,
            Expression<Func<T, bool>>? predicate = null)
        {
            var query = _dbSet.AsNoTracking();

            if (isActive.HasValue) query = query.Where(x => x.IsActive == isActive.Value);
            if (isDeleted.HasValue) query = query.Where(x => x.IsDeleted == isDeleted.Value);
            if (predicate != null) query = query.Where(predicate);

            int totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(x => x.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalCount, pageIndex, pageSize);
        }
    }
}