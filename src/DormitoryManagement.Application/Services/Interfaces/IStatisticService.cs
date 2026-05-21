using DormitoryManagement.Application.Dtos.Responses.Statistics;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện Service cung cấp các hoạt động liên quan đến báo cáo thống kê
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Lấy toàn bộ thông tin tổng hợp cho trang Dashboard thống kê
        /// </summary>
        Task<StatisticSummaryResponse> GetStatisticSummaryAsync();
    }
}
