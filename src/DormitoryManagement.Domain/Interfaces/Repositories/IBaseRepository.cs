using System.Linq.Expressions;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository cơ sở chứa các phương thức CRUD và truy vấn chung.
    /// </summary>
    /// <typeparam name="T">Kiểu thực thể (Entity)</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Lấy thực thể theo khóa chính (Id).
        /// </summary>
        /// <param name="id">Id của thực thể cần lấy</param>
        /// <returns>Thực thể nếu tìm thấy, ngược lại là null</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy tất cả thực thể trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="includeDeleted">Có bao gồm thực thể đã bị xóa mềm không</param>
        /// <returns>Danh sách tất cả các thực thể</returns>
        Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false);

        /// <summary>
        /// Tìm kiếm các thực thể theo điều kiện lọc cụ thể.
        /// </summary>
        /// <param name="predicate">Biểu thức điều kiện lọc</param>
        /// <returns>Danh sách các thực thể thỏa mãn điều kiện</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Lấy danh sách thực thể có phân trang kèm theo điều kiện lọc và sắp xếp.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại (bắt đầu từ 1)</param>
        /// <param name="pageSize">Số lượng phần tử trên một trang</param>
        /// <param name="predicate">Biểu thức lọc điều kiện (tùy chọn)</param>
        /// <param name="orderBy">Hàm sắp xếp kết quả (tùy chọn)</param>
        /// <returns>Kết quả phân trang của thực thể</returns>
        Task<PagedResult<T>> GetPagedAsync(
           int pageIndex,
           int pageSize,
           Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        /// <summary>
        /// Kiểm tra xem có thực thể nào thỏa mãn điều kiện lọc không.
        /// </summary>
        /// <param name="predicate">Biểu thức lọc điều kiện</param>
        /// <returns>True nếu tồn tại ít nhất một thực thể thỏa mãn, ngược lại là False</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Thêm mới một thực thể vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="entity">Thực thể cần thêm mới</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Thêm mới danh sách thực thể vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="entities">Danh sách thực thể cần thêm mới</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Cập nhật thông tin một thực thể hiện tại.
        /// </summary>
        /// <param name="entity">Thực thể cần cập nhật</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Xóa một thực thể (hỗ trợ xóa mềm hoặc xóa cứng).
        /// </summary>
        /// <param name="entity">Thực thể cần xóa</param>
        /// <param name="isSoftDelete">True để xóa mềm (đặt cờ IsDeleted = true), False để xóa cứng (xóa hẳn khỏi DB)</param>
        Task DeleteAsync(T entity, bool isSoftDelete = true);

        /// <summary>
        /// Xóa danh sách thực thể (hỗ trợ xóa mềm hoặc xóa cứng).
        /// </summary>
        /// <param name="entities">Danh sách thực thể cần xóa</param>
        /// <param name="isSoftDelete">True để xóa mềm, False để xóa cứng</param>
        Task DeleteRangeAsync(IEnumerable<T> entities, bool isSoftDelete = true);

        /// <summary>
        /// Lấy đối tượng IQueryable của thực thể để xây dựng các câu truy vấn phức tạp hơn ở lớp trên.
        /// </summary>
        /// <returns>Đối tượng IQueryable của thực thể</returns>
        IQueryable<T> GetQuery();

        /// <summary>
        /// Lấy danh sách thực thể lọc theo trạng thái hoạt động và trạng thái xóa.
        /// </summary>
        /// <param name="isActive">Trạng thái hoạt động (True/False/Null)</param>
        /// <param name="isDeleted">Trạng thái xóa mềm (True/False/Null)</param>
        /// <returns>Danh sách thực thể thỏa mãn điều kiện</returns>
        Task<IEnumerable<T>> GetByStatusAsync(bool? isActive = null, bool? isDeleted = null);

        /// <summary>
        /// Khôi phục một thực thể đã bị xóa mềm trước đó.
        /// </summary>
        /// <param name="entity">Thực thể cần khôi phục</param>
        Task RestoreAsync(T entity);

        /// <summary>
        /// Lấy danh sách thực thể phân trang theo trạng thái hoạt động, trạng thái xóa, kèm theo bộ lọc và nạp các thuộc tính liên kết.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại (bắt đầu từ 1)</param>
        /// <param name="pageSize">Số lượng phần tử trên một trang</param>
        /// <param name="isActive">Trạng thái hoạt động (True/False/Null)</param>
        /// <param name="isDeleted">Trạng thái xóa mềm (True/False/Null)</param>
        /// <param name="predicate">Biểu thức lọc điều kiện bổ sung (tùy chọn)</param>
        /// <param name="includeProperties">Mảng các biểu thức lambda chỉ định các thuộc tính liên kết cần nạp (Eager Loading)</param>
        /// <returns>Kết quả phân trang của thực thể</returns>
        Task<PagedResult<T>> GetByStatusPagedAsync(
            int pageIndex,
            int pageSize,
            bool? isActive = null,
            bool? isDeleted = null,
            Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includeProperties);
    }
}