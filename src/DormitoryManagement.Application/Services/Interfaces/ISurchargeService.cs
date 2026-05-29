using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý phụ phí (Surcharge).
    /// </summary>
    public interface ISurchargeService
    {
        /// <summary>
        /// Lấy danh sách phụ phí theo Id hóa đơn (InvoiceId).
        /// </summary>
        Task<IEnumerable<Surcharge>> GetByInvoiceIdAsync(Guid invoiceId);

        /// <summary>
        /// Lấy danh sách phụ phí đang hoạt động (IsActive = true, IsDeleted = false).
        /// </summary>
        Task<IEnumerable<Surcharge>> GetActiveAsync();

        /// <summary>
        /// Tạo phụ phí mới.
        /// </summary>
        Task<bool> CreateSurchargeAsync(Guid invoiceId, string surchargeName, decimal amount, bool isActive = true);

        /// <summary>
        /// Cập nhật phụ phí theo Id.
        /// </summary>
        Task<bool> UpdateSurchargeAsync(Guid id, string surchargeName, decimal amount, bool isActive);

        /// <summary>
        /// Xóa mềm phụ phí theo Id.
        /// </summary>
        Task<bool> SoftDeleteSurchargeAsync(Guid id);

        /// <summary>
        /// Khôi phục phụ phí đã bị xóa mềm.
        /// </summary>
        Task<bool> RestoreSurchargeAsync(Guid id);

        /// <summary>
        /// Lấy danh sách phụ phí phân trang kèm theo bộ lọc tìm kiếm.
        /// </summary>
        Task<DormitoryManagement.Domain.Common.PagedResult<Surcharge>> GetPagedSurchargesAsync(int pageIndex, int pageSize, string? searchString, bool? isActive = null, bool? isDeleted = false);

        /// <summary>
        /// Lấy danh sách phụ phí phân trang của sinh viên theo UserId.
        /// </summary>
        Task<DormitoryManagement.Domain.Common.PagedResult<Surcharge>> GetPagedSurchargesByUserIdAsync(Guid userId, int pageIndex, int pageSize, string? searchString, bool? isActive = null);
    }
}
