namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository cho các câu truy vấn thống kê dữ liệu
    /// </summary>
    public interface IStatisticRepository
    {
        /// <summary>
        /// Lấy tổng số lượng sinh viên đang nội trú (không bị xóa mềm)
        /// </summary>
        Task<int> GetTotalStudentsCountAsync();

        /// <summary>
        /// Lấy số lượng phòng đang trống (Available)
        /// </summary>
        Task<int> GetEmptyRoomsCountAsync();

        /// <summary>
        /// Lấy số lượng hóa đơn chưa thanh toán hoặc quá hạn (Unpaid, Overdue, PartiallyPaid)
        /// </summary>
        Task<int> GetUnpaidInvoicesCountAsync();

        /// <summary>
        /// Lấy số lượng biên bản vi phạm mới (Pending)
        /// </summary>
        Task<int> GetNewViolationsCountAsync();

        /// <summary>
        /// Lấy mảng đếm số lượng phòng theo trạng thái [Đã đầy, Còn trống, Bảo trì]
        /// </summary>
        Task<int[]> GetRoomStatusDataAsync();

        /// <summary>
        /// Lấy doanh thu đã thu và còn nợ của 6 tháng gần nhất
        /// </summary>
        Task<List<MonthlyRevenueModel>> GetLast6MonthsRevenueAsync();

        /// <summary>
        /// Lấy thống kê trạng thái của Yêu cầu bảo trì
        /// </summary>
        Task<MaintenanceStatsModel> GetMaintenanceStatsAsync();

        /// <summary>
        /// Lấy thống kê các hợp đồng
        /// </summary>
        Task<ContractStatsModel> GetContractStatsAsync();

        /// <summary>
        /// Lấy thống kê tỷ lệ lấp đầy giường
        /// </summary>
        Task<BedOccupancyModel> GetBedOccupancyStatsAsync();

        /// <summary>
        /// Lấy thống kê vi phạm trong 6 tháng gần nhất
        /// </summary>
        Task<List<MonthlyViolationModel>> GetLast6MonthsViolationsAsync();
    }

    /// <summary>
    /// Model đại diện cho doanh thu theo từng tháng
    /// </summary>
    public class MonthlyRevenueModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthLabel { get; set; } = string.Empty;
        public decimal RevenuePaid { get; set; }   // Triệu VNĐ
        public decimal RevenueUnpaid { get; set; } // Triệu VNĐ
    }

    /// <summary>
    /// Model thống kê yêu cầu bảo trì
    /// </summary>
    public class MaintenanceStatsModel
    {
        public int OpenCount { get; set; }
        public int InProgressCount { get; set; }
        public int ResolvedCount { get; set; }
        public int ClosedCount { get; set; }
    }

    /// <summary>
    /// Model thống kê trạng thái hợp đồng
    /// </summary>
    public class ContractStatsModel
    {
        public int ActiveCount { get; set; }
        public int ExpiredCount { get; set; }
        public int PendingCount { get; set; }
    }

    /// <summary>
    /// Model thống kê tỷ lệ lấp đầy giường
    /// </summary>
    public class BedOccupancyModel
    {
        public int TotalBeds { get; set; }
        public int OccupiedBeds { get; set; }
        public double OccupancyRate => TotalBeds == 0 ? 0 : Math.Round((double)OccupiedBeds / TotalBeds * 100, 2);
    }

    /// <summary>
    /// Model đại diện cho thống kê vi phạm theo tháng
    /// </summary>
    public class MonthlyViolationModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthLabel { get; set; } = string.Empty;
        public int ViolationCount { get; set; }
    }
}
