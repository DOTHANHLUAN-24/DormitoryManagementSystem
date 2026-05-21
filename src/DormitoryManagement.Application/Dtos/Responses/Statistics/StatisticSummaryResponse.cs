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
    }
}
