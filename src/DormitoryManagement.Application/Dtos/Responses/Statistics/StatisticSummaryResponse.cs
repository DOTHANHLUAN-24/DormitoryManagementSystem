using System;

namespace DormitoryManagement.Application.Dtos.Responses.Statistics
{
    /// <summary>
    /// Đối tượng vận chuyển dữ liệu chứa toàn bộ các thông tin thống kê báo cáo
    /// </summary>
    public class StatisticSummaryResponse
    {
        public int TotalStudents { get; set; }
        
        public int EmptyRooms { get; set; }
        
        public int UnpaidInvoices { get; set; }
        
        public int NewViolations { get; set; }
        
        public int[] RoomStatusData { get; set; } = Array.Empty<int>();
        
        public string[] RevenueLabels { get; set; } = Array.Empty<string>();
        
        public decimal[] RevenuePaid { get; set; } = Array.Empty<decimal>();
        
        public decimal[] RevenueUnpaid { get; set; } = Array.Empty<decimal>();

        // New properties for Extended Statistics
        public DormitoryManagement.Domain.Interfaces.Repositories.MaintenanceStatsModel MaintenanceStats { get; set; } = new();
        public DormitoryManagement.Domain.Interfaces.Repositories.ContractStatsModel ContractStats { get; set; } = new();
        public DormitoryManagement.Domain.Interfaces.Repositories.BedOccupancyModel BedOccupancyStats { get; set; } = new();

        public string[] ViolationLabels { get; set; } = Array.Empty<string>();
        public int[] ViolationData { get; set; } = Array.Empty<int>();
    }
}
