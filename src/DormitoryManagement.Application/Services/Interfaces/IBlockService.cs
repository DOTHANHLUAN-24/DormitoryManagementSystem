using DormitoryManagement.Application.Dtos.Requests.Blocks;
using DormitoryManagement.Application.Dtos.Responses.Blocks;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý nghiệp vụ tòa nhà (Block).
    /// </summary>
    public interface IBlockService
    {
        /// <summary>
        /// Lấy danh sách tòa nhà đang hoạt động có phân trang và tìm kiếm.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm theo tên tòa</param>
        /// <returns>Kết quả phân trang danh sách tòa nhà</returns>
        Task<PagedResult<BlockResponseDto>> GetActiveBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm);

        /// <summary>
        /// Lấy danh sách tòa nhà đã xóa mềm (thùng rác) có phân trang và tìm kiếm.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm</param>
        /// <returns>Kết quả phân trang danh sách tòa nhà đã bị xóa mềm</returns>
        Task<PagedResult<BlockResponseDto>> GetDeletedBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm);

        /// <summary>
        /// Lấy danh sách tòa nhà đang tạm ngưng hoạt động có phân trang và tìm kiếm.
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="searchTerm">Từ khóa tìm kiếm</param>
        /// <returns>Kết quả phân trang danh sách tòa nhà tạm ngưng</returns>
        Task<PagedResult<BlockResponseDto>> GetSuspendedBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm);

        /// <summary>
        /// Lấy chi tiết thông tin tòa nhà theo Id.
        /// </summary>
        /// <param name="id">Id của tòa nhà</param>
        /// <returns>Thông tin tòa nhà hoặc null nếu không tìm thấy</returns>
        Task<BlockResponseDto?> GetBlockByIdAsync(Guid id);

        /// <summary>
        /// Lấy toàn bộ danh sách tòa nhà (thường dùng cho dropdowns).
        /// </summary>
        /// <returns>Danh sách tất cả tòa nhà</returns>
        Task<IEnumerable<BlockResponseDto>> GetAllBlocksAsync();

        /// <summary>
        /// Tạo mới một tòa nhà.
        /// </summary>
        /// <param name="request">Thông tin yêu cầu tạo tòa nhà</param>
        /// <returns>True nếu tạo thành công, ngược lại là False</returns>
        Task<bool> CreateBlockAsync(BlockRequestDto request);

        /// <summary>
        /// Cập nhật thông tin tòa nhà hiện tại.
        /// </summary>
        /// <param name="id">Id tòa nhà cần sửa</param>
        /// <param name="request">Thông tin cập nhật mới</param>
        /// <returns>True nếu cập nhật thành công, ngược lại là False</returns>
        Task<bool> UpdateBlockAsync(Guid id, BlockRequestDto request);

        /// <summary>
        /// Xóa mềm một tòa nhà (chuyển vào thùng rác).
        /// </summary>
        /// <param name="id">Id tòa nhà</param>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        Task<bool> SoftDeleteBlockAsync(Guid id);

        /// <summary>
        /// Khôi phục tòa nhà bị xóa mềm về trạng thái hoạt động bình thường.
        /// </summary>
        /// <param name="id">Id tòa nhà</param>
        /// <returns>True nếu khôi phục thành công, ngược lại là False</returns>
        Task<bool> RestoreBlockAsync(Guid id);

        /// <summary>
        /// Xóa vĩnh viễn tòa nhà ra khỏi cơ sở dữ liệu.
        /// </summary>
        /// <param name="id">Id tòa nhà</param>
        /// <returns>True nếu xóa vĩnh viễn thành công, ngược lại là False</returns>
        Task<bool> DeletePermanentlyAsync(Guid id);

        /// <summary>
        /// Thay đổi (bật/tắt) trạng thái hoạt động (Active) của tòa nhà.
        /// </summary>
        /// <param name="id">Id tòa nhà</param>
        /// <returns>True nếu chuyển đổi thành công, ngược lại là False</returns>
        Task<bool> ToggleBlockStatusAsync(Guid id);
    }
}
