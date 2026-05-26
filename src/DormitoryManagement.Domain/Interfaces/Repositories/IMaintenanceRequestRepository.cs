using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin yêu cầu bảo trì (MaintenanceRequest).
    /// </summary>
    public interface IMaintenanceRequestRepository : IBaseRepository<MaintenanceRequest>
    {
        /// <summary>
        /// Lấy danh sách yêu cầu bảo trì thuộc một phòng cụ thể.
        /// </summary>
        Task<IEnumerable<MaintenanceRequest>> GetRequestsByRoomIdAsync(Guid roomId);

        /// <summary>
        /// Lấy danh sách yêu cầu bảo trì do một người dùng yêu cầu.
        /// </summary>
        Task<IEnumerable<MaintenanceRequest>> GetRequestsByRequesterIdAsync(Guid requesterId);

        /// <summary>
        /// Lấy danh sách yêu cầu bảo trì đang được xử lý bởi một nhân viên cụ thể.
        /// </summary>
        Task<IEnumerable<MaintenanceRequest>> GetRequestsByHandlerIdAsync(Guid handlerId);

        /// <summary>
        /// Lấy danh sách yêu cầu bảo trì theo trạng thái.
        /// </summary>
        Task<IEnumerable<MaintenanceRequest>> GetRequestsByStatusAsync(MaintenanceStatus status);

        /// <summary>
        /// Lấy danh sách yêu cầu bảo trì theo mức độ ưu tiên.
        /// </summary>
        Task<IEnumerable<MaintenanceRequest>> GetRequestsByPriorityAsync(MaintenancePriority priority);
    }
}
