using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests
{
    /// <summary>
    /// DTO dùng để sinh viên gửi yêu cầu bảo trì/sửa chữa mới.
    /// </summary>
    public class CreateMaintenanceRequestDto
    {
        /// <summary>
        /// Tiêu đề của yêu cầu sửa chữa (Ví dụ: Hỏng bóng đèn, Điều hòa không mát).
        /// </summary>
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Mô tả chi tiết tình trạng hư hỏng.
        /// </summary>
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Mức độ ưu tiên của yêu cầu (Low, Medium, High, Urgent).
        /// </summary>
        public MaintenancePriority Priority { get; set; } = MaintenancePriority.Medium;

        /// <summary>
        /// Id của phòng cần sửa chữa.
        /// </summary>
        [Required(ErrorMessage = "Phòng không được để trống")]
        public Guid RoomId { get; set; }

        /// <summary>
        /// Ghi chú thêm (nếu có).
        /// </summary>
        public string? Notes { get; set; }
    }
}
