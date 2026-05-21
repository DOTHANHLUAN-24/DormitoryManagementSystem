using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin tài sản (Asset).
    /// </summary>
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        /// <summary>
        /// Lấy thông tin tài sản theo mã tài sản (AssetCode).
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần tìm</param>
        /// <returns>Tài sản nếu tìm thấy, ngược lại là null</returns>
        Task<Asset?> GetByAssetCodeAsync(string assetCode);

        /// <summary>
        /// Lấy danh sách tài sản đang hoạt động thuộc về một phòng.
        /// </summary>
        /// <param name="roomId">Id của phòng cần kiểm tra</param>
        /// <returns>Danh sách tài sản</returns>
        Task<IEnumerable<Asset>> GetActiveAssetsByRoomIdAsync(Guid roomId);

        /// <summary>
        /// Kiểm tra xem tài sản có đang hoạt động và chưa bị xóa không.
        /// </summary>
        /// <param name="assetId">Id của tài sản</param>
        /// <returns>True nếu tài sản đang hoạt động và chưa bị xóa, ngược lại là False</returns>
        Task<bool> IsAssetActiveAsync(Guid assetId);

        /// <summary>
        /// Lọc danh sách tài sản theo trạng thái tài sản (Tốt, Hỏng, Đang sửa, Mất).
        /// </summary>
        /// <param name="status">Trạng thái tài sản cần lọc</param>
        /// <returns>Danh sách tài sản</returns>
        Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status);

        /// <summary>
        /// Phân trang danh sách tài sản kèm theo thông tin phòng (Room) và tòa nhà (Block), hỗ trợ tìm kiếm và lọc.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm theo tên hoặc mã tài sản (tùy chọn)</param>
        /// <param name="status">Lọc theo trạng thái tài sản (tùy chọn)</param>
        /// <param name="roomId">Lọc theo phòng (tùy chọn)</param>
        /// <returns>Kết quả phân trang của danh sách tài sản</returns>
        Task<PagedResult<Asset>> GetAssetsWithDetailsPagedAsync(
            int pageIndex, int pageSize, string? searchTerm = null, AssetStatus? status = null, Guid? roomId = null);

        /// <summary>
        /// Kiểm tra trùng mã tài sản trong hệ thống.
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần kiểm tra</param>
        /// <param name="excludeId">Id tài sản loại trừ khi kiểm tra (dùng cho cập nhật, tùy chọn)</param>
        /// <returns>True nếu trùng mã tài sản khác, ngược lại là False</returns>
        Task<bool> IsAssetCodeDuplicateAsync(string assetCode, Guid? excludeId = null);
    
        /// <summary>
        /// Phân trang danh sách tài sản đã bị xóa mềm (thùng rác) kèm chi tiết phòng và tòa.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm theo tên hoặc mã (tùy chọn)</param>
        /// <returns>Kết quả phân trang của tài sản đã xóa</returns>
        Task<PagedResult<Asset>> GetDeletedAssetsWithDetailsPagedAsync(
            int pageIndex, int pageSize, string? searchTerm = null);
    }
}
