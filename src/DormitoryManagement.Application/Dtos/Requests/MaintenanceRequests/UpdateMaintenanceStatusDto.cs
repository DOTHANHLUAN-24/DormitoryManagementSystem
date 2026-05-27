using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests
{
    /// <summary>
    /// DTO dùng để nhân viên kỹ thuật cập nhật trạng thái xử lý yêu cầu.
    /// </summary>
    public class UpdateMaintenanceStatusDto
    {
        /// <summary>
        /// Trạng thái mới của yêu cầu (Open, InProgress, Resolved, Closed).
        /// </summary>
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public MaintenanceStatus Status { get; set; }

        /// <summary>
        /// Id của nhân viên kỹ thuật đang xử lý (nếu có).
        /// </summary>
        public Guid? HandlerId { get; set; }
    }
}
