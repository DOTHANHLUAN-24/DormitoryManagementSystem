using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository xử lý các truy vấn thống kê dữ liệu trực tiếp từ cơ sở dữ liệu
    /// </summary>
    public class StatisticRepository : IStatisticRepository
    {
        private readonly ApplicationDbContext _context;

        public StatisticRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy tổng số lượng sinh viên đang nội trú (không bị xóa mềm)
        /// </summary>
        public async Task<int> GetTotalStudentsCountAsync()
        {
            return await _context.Users
                .CountAsync(u => u.Role == UserRole.Student && !u.IsDeleted);
        }

        /// <summary>
        /// Lấy số lượng phòng đang trống (Available)
        /// </summary>
        public async Task<int> GetEmptyRoomsCountAsync()
        {
            return await _context.Rooms
                .CountAsync(r => r.Status == RoomStatus.Available && !r.IsDeleted);
        }

        /// <summary>
        /// Lấy số lượng hóa đơn chưa thanh toán hoặc quá hạn (Unpaid, Overdue, PartiallyPaid)
        /// </summary>
        public async Task<int> GetUnpaidInvoicesCountAsync()
        {
            return await _context.Invoices
                .CountAsync(i => i.Status != InvoiceStatus.Paid && !i.IsDeleted);
        }

        /// <summary>
        /// Lấy số lượng biên bản vi phạm mới (Pending)
        /// </summary>
        public async Task<int> GetNewViolationsCountAsync()
        {
            return await _context.Violations
                .CountAsync(v => v.Status == ViolationStatus.Pending && !v.IsDeleted);
        }

        /// <summary>
        /// Lấy mảng đếm số lượng phòng theo trạng thái [Đã đầy, Còn trống, Bảo trì]
        /// </summary>
        public async Task<int[]> GetRoomStatusDataAsync()
        {
            var roomGroups = await _context.Rooms
                .Where(r => !r.IsDeleted)
                .GroupBy(r => r.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            int fullCount = roomGroups.FirstOrDefault(x => x.Status == RoomStatus.Full)?.Count ?? 0;

            // Còn trống: kết hợp Available và Reserved
            int availableCount = (roomGroups.FirstOrDefault(x => x.Status == RoomStatus.Available)?.Count ?? 0)
                               + (roomGroups.FirstOrDefault(x => x.Status == RoomStatus.Reserved)?.Count ?? 0);

            int maintenanceCount = roomGroups.FirstOrDefault(x => x.Status == RoomStatus.Maintenance)?.Count ?? 0;

            return new int[] { fullCount, availableCount, maintenanceCount };
        }

        /// <summary>
        /// Lấy doanh thu đã thu và còn nợ của 6 tháng gần nhất (đơn vị: Triệu VNĐ)
        /// </summary>
        public async Task<List<MonthlyRevenueModel>> GetLast6MonthsRevenueAsync()
        {
            var today = DateTime.Today;
            var months = new List<(int Year, int Month)>();

            // Tính toán khoảng 6 tháng gần nhất bao gồm cả tháng hiện tại
            for (int i = 5; i >= 0; i--)
            {
                var d = today.AddMonths(-i);
                months.Add((d.Year, d.Month));
            }

            var firstMonth = months.First();
            var lastMonth = months.Last();

            // Truy vấn hóa đơn trong khoảng thời gian trên
            var invoices = await _context.Invoices
                .Where(i => !i.IsDeleted &&
                           ((i.BillingYear > firstMonth.Year) || (i.BillingYear == firstMonth.Year && i.BillingMonth >= firstMonth.Month)) &&
                           ((i.BillingYear < lastMonth.Year) || (i.BillingYear == lastMonth.Year && i.BillingMonth <= lastMonth.Month)))
                .ToListAsync();

            var result = new List<MonthlyRevenueModel>();

            foreach (var m in months)
            {
                var monthlyInvoices = invoices
                    .Where(i => i.BillingYear == m.Year && i.BillingMonth == m.Month)
                    .ToList();

                // Tính tổng doanh thu đã trả và còn nợ
                // Quy đổi sang đơn vị Triệu VNĐ bằng cách chia cho 1,000,000
                decimal paidSum = monthlyInvoices
                    .Where(i => i.Status == InvoiceStatus.Paid)
                    .Sum(i => i.TotalAmount) / 1000000m;

                decimal unpaidSum = monthlyInvoices
                    .Where(i => i.Status != InvoiceStatus.Paid)
                    .Sum(i => i.TotalAmount) / 1000000m;

                result.Add(new MonthlyRevenueModel
                {
                    Year = m.Year,
                    Month = m.Month,
                    MonthLabel = $"Tháng {m.Month}",
                    RevenuePaid = Math.Round(paidSum, 2),
                    RevenueUnpaid = Math.Round(unpaidSum, 2)
                });
            }

            return result;
        }
    }
}
