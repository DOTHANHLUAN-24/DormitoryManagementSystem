using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests
{
    public class MaintenanceRequestFilterRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public MaintenanceStatus? Status { get; set; }
        public MaintenancePriority? Priority { get; set; }
    }
}
