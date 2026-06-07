using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Responses.MaintenanceRequests
{
    /// <summary>
    /// DTO trả về thông tin chi tiết của một yêu cầu bảo trì.
    /// </summary>
    public class MaintenanceRequestResponseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime? ResolvedAt { get; set; }

        // Thông tin phòng
        public Guid RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string BlockName { get; set; } = string.Empty;

        // Thông tin người yêu cầu (Sinh viên)
        public Guid RequesterId { get; set; }
        public string RequesterName { get; set; } = string.Empty;
        public string RequesterCode { get; set; } = string.Empty;

        // Thông tin người xử lý (Kỹ thuật viên)
        public Guid? HandlerId { get; set; }
        public string HandlerName { get; set; } = string.Empty;

        // Ghi chú thêm
        public string? Notes { get; set; }
    }
}
