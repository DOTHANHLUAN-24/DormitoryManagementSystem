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
}
