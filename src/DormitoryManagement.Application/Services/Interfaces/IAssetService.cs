using DormitoryManagement.Application.Dtos.Requests.Assets;
using DormitoryManagement.Application.Dtos.Responses.Assets;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý nghiệp vụ tài sản (Asset).
    /// </summary>
    public interface IAssetService
    {
        /// <summary>
        /// Lấy danh sách tài sản phân trang, hỗ trợ tìm kiếm theo tên/mã tài sản, lọc theo trạng thái và phòng.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm (tên hoặc mã)</param>
        /// <param name="status">Trạng thái tài sản cần lọc (tùy chọn)</param>
        /// <param name="roomId">Id phòng cần lọc (tùy chọn)</param>
        /// <returns>Kết quả phân trang danh sách tài sản DTO</returns>
        Task<PagedResult<AssetResponse>> GetPagedAssetsAsync(int pageIndex, int pageSize, string? searchTerm = null, AssetStatus? status = null, Guid? roomId = null);

        /// <summary>
        /// Lấy danh sách tài sản đã bị xóa mềm (thùng rác) phân trang.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trên mỗi trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm (tùy chọn)</param>
        /// <returns>Kết quả phân trang danh sách tài sản đã xóa mềm DTO</returns>
        Task<PagedResult<AssetResponse>> GetDeletedAssetsPagedAsync(int pageIndex, int pageSize, string? searchTerm = null);

        /// <summary>
        /// Lấy thông tin chi tiết của tài sản theo Id.
        /// </summary>
        /// <param name="id">Id của tài sản</param>
        /// <returns>Tài sản DTO nếu tìm thấy, ngược lại là null</returns>
        Task<AssetResponse?> GetAssetByIdAsync(Guid id);

        /// <summary>
        /// Lấy thông tin tài sản theo mã tài sản (AssetCode).
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần tìm</param>
        /// <returns>Tài sản DTO nếu tìm thấy, ngược lại là null</returns>
        Task<AssetResponse?> GetAssetByCodeAsync(string assetCode);

        /// <summary>
        /// Lấy danh sách tài sản đang hoạt động của một phòng cụ thể.
        /// </summary>
        /// <param name="roomId">Id của phòng</param>
        /// <returns>Danh sách tài sản DTO</returns>
        Task<IEnumerable<AssetResponse>> GetActiveAssetsByRoomIdAsync(Guid roomId);

        /// <summary>
        /// Tạo mới một tài sản vào hệ thống.
        /// </summary>
        /// <param name="request">Thông tin yêu cầu tạo tài sản</param>
        /// <returns>True nếu tạo thành công, ngược lại là False</returns>
        Task<bool> CreateAssetAsync(CreateAssetRequest request);

        /// <summary>
        /// Cập nhật thông tin tài sản hiện tại.
        /// </summary>
        /// <param name="id">Id tài sản cần sửa</param>
        /// <param name="request">Thông tin cập nhật tài sản</param>
        /// <returns>True nếu cập nhật thành công, ngược lại là False</returns>
        Task<bool> UpdateAssetAsync(Guid id, UpdateAssetRequest request);

        /// <summary>
        /// Xóa mềm tài sản (chuyển vào thùng rác).
        /// </summary>
        /// <param name="id">Id của tài sản cần xóa mềm</param>
        /// <returns>True nếu xóa mềm thành công, ngược lại là False</returns>
        Task<bool> SoftDeleteAssetAsync(Guid id);

        /// <summary>
        /// Khôi phục tài sản bị xóa mềm từ thùng rác.
        /// </summary>
        /// <param name="id">Id tài sản cần khôi phục</param>
        /// <returns>True nếu khôi phục thành công, ngược lại là False</returns>
        Task<bool> RestoreAssetAsync(Guid id);

        /// <summary>
        /// Xóa vĩnh viễn tài sản ra khỏi cơ sở dữ liệu.
        /// </summary>
        /// <param name="id">Id tài sản cần xóa vĩnh viễn</param>
        /// <returns>True nếu xóa thành công, ngược lại là False</returns>
        Task<bool> DeletePermanentlyAsync(Guid id);

        /// <summary>
        /// Kích hoạt hoặc hủy kích hoạt (Toggle) trạng thái hoạt động của tài sản.
        /// </summary>
        /// <param name="id">Id của tài sản</param>
        /// <returns>True nếu chuyển đổi thành công, ngược lại là False</returns>
        Task<bool> ToggleAssetStatusAsync(Guid id);

        /// <summary>
        /// Kiểm tra trùng mã tài sản trong hệ thống (tránh trùng khi thêm mới hoặc cập nhật).
        /// </summary>
        /// <param name="assetCode">Mã tài sản</param>
        /// <param name="excludeId">Id tài sản loại trừ (tùy chọn)</param>
        /// <returns>True nếu bị trùng lặp, ngược lại là False</returns>
        Task<bool> IsAssetCodeDuplicateAsync(string assetCode, Guid? excludeId = null);
    }
}
