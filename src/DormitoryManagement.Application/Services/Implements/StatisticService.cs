using DormitoryManagement.Application.Dtos.Responses.Statistics;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai Service cho các thống kê báo cáo
    /// </summary>
    public class StatisticService(IStatisticRepository statisticRepository) : IStatisticService
    {
        private readonly IStatisticRepository _statisticRepository = statisticRepository;

        /// <summary>
        /// Lấy toàn bộ thông tin tổng hợp cho trang Dashboard thống kê
        /// </summary>
        public async Task<StatisticSummaryResponse> GetStatisticSummaryAsync()
        {
            var totalStudents = await _statisticRepository.GetTotalStudentsCountAsync();
            var emptyRooms = await _statisticRepository.GetEmptyRoomsCountAsync();
            var unpaidInvoices = await _statisticRepository.GetUnpaidInvoicesCountAsync();
            var newViolations = await _statisticRepository.GetNewViolationsCountAsync();
            var roomStatusData = await _statisticRepository.GetRoomStatusDataAsync();
            var revenueData = await _statisticRepository.GetLast6MonthsRevenueAsync();

            var maintenanceStats = await _statisticRepository.GetMaintenanceStatsAsync();
            var contractStats = await _statisticRepository.GetContractStatsAsync();
            var bedOccupancyStats = await _statisticRepository.GetBedOccupancyStatsAsync();
            var violationData = await _statisticRepository.GetLast6MonthsViolationsAsync();

            return new StatisticSummaryResponse
            {
                TotalStudents = totalStudents,
                EmptyRooms = emptyRooms,
                UnpaidInvoices = unpaidInvoices,
                NewViolations = newViolations,
                RoomStatusData = roomStatusData,
                RevenueLabels = revenueData.Select(r => r.MonthLabel).ToArray(),
                RevenuePaid = revenueData.Select(r => r.RevenuePaid).ToArray(),
                RevenueUnpaid = revenueData.Select(r => r.RevenueUnpaid).ToArray(),

                MaintenanceStats = maintenanceStats,
                ContractStats = contractStats,
                BedOccupancyStats = bedOccupancyStats,
                ViolationLabels = violationData.Select(v => v.MonthLabel).ToArray(),
                ViolationData = violationData.Select(v => v.ViolationCount).ToArray()
            };
        }
    }
}
