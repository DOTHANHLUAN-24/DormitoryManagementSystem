using DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests;
using DormitoryManagement.Application.Dtos.Responses.MaintenanceRequests;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện Service xử lý nghiệp vụ cho tính năng Yêu cầu bảo trì/sửa chữa.
    /// </summary>
    public interface IMaintenanceRequestService
    {
        /// <summary>
        /// Tạo một yêu cầu bảo trì mới (Dành cho Sinh viên).
        /// </summary>
        /// <param name="dto">Thông tin yêu cầu</param>
        /// <param name="requesterId">Id của sinh viên yêu cầu</param>
        /// <returns>Đối tượng yêu cầu vừa tạo</returns>
        Task<MaintenanceRequestResponseDto> CreateAsync(CreateMaintenanceRequestDto dto, Guid requesterId);

        /// <summary>
        /// Lấy chi tiết một yêu cầu bảo trì.
        /// </summary>
        Task<MaintenanceRequestResponseDto?> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy danh sách phân trang các yêu cầu bảo trì của hệ thống (Dành cho Admin/Kỹ thuật).
        /// </summary>
        Task<PagedResult<MaintenanceRequestResponseDto>> GetAllPagedAsync(int pageIndex, int pageSize, string? searchTerm = null, MaintenanceStatus? status = null, MaintenancePriority? priority = null);

        /// <summary>
        /// Lấy danh sách yêu cầu của một sinh viên cụ thể.
        /// </summary>
        Task<IEnumerable<MaintenanceRequestResponseDto>> GetByRequesterIdAsync(Guid requesterId);

        /// <summary>
        /// Nhân viên kỹ thuật cập nhật trạng thái của yêu cầu bảo trì.
        /// </summary>
        /// <param name="id">Id của yêu cầu</param>
        /// <param name="dto">Trạng thái mới</param>
        /// <param name="handlerId">Id của nhân viên kỹ thuật (nếu có)</param>
        /// <returns>True nếu thành công, False nếu thất bại</returns>
        Task<bool> UpdateStatusAsync(Guid id, UpdateMaintenanceStatusDto dto, Guid? handlerId = null);

        /// <summary>
        /// Xóa một yêu cầu bảo trì.
        /// </summary>
        Task<bool> DeleteAsync(Guid id);
    }
}
