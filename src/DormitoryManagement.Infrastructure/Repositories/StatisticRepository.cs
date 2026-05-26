using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository xử lý các truy vấn thống kê dữ liệu trực tiếp từ cơ sở dữ liệu
    /// </summary>
    public class StatisticRepository(ApplicationDbContext context) : IStatisticRepository
    {
        private readonly ApplicationDbContext _context = context;

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

        public async Task<MaintenanceStatsModel> GetMaintenanceStatsAsync()
        {
            var stats = await _context.MaintenanceRequests
                .Where(m => !m.IsDeleted)
                .GroupBy(m => m.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return new MaintenanceStatsModel
            {
                OpenCount = stats.FirstOrDefault(x => x.Status == MaintenanceStatus.Open)?.Count ?? 0,
                InProgressCount = stats.FirstOrDefault(x => x.Status == MaintenanceStatus.InProgress)?.Count ?? 0,
                ResolvedCount = stats.FirstOrDefault(x => x.Status == MaintenanceStatus.Resolved)?.Count ?? 0,
                ClosedCount = stats.FirstOrDefault(x => x.Status == MaintenanceStatus.Closed)?.Count ?? 0
            };
        }

        public async Task<ContractStatsModel> GetContractStatsAsync()
        {
            var stats = await _context.Contracts
                .Where(c => !c.IsDeleted)
                .GroupBy(c => c.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return new ContractStatsModel
            {
                ActiveCount = stats.FirstOrDefault(x => x.Status == ContractStatus.Active)?.Count ?? 0,
                ExpiredCount = stats.FirstOrDefault(x => x.Status == ContractStatus.Expired)?.Count ?? 0,
                PendingCount = stats.FirstOrDefault(x => x.Status == ContractStatus.Pending)?.Count ?? 0
            };
        }

        public async Task<BedOccupancyModel> GetBedOccupancyStatsAsync()
        {
            var beds = await _context.Beds
                .Where(b => !b.IsDeleted)
                .GroupBy(b => b.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            var totalBeds = beds.Sum(x => x.Count);
            var occupiedBeds = beds.FirstOrDefault(x => x.Status == BedStatus.Occupied)?.Count ?? 0;

            return new BedOccupancyModel
            {
                TotalBeds = totalBeds,
                OccupiedBeds = occupiedBeds
            };
        }

        public async Task<List<MonthlyViolationModel>> GetLast6MonthsViolationsAsync()
        {
            var today = DateTime.Today;
            var startDate = new DateTime(today.Year, today.Month, 1).AddMonths(-5);

            var violations = await _context.Violations
                .Where(v => !v.IsDeleted && v.ViolationDate >= startDate)
                .ToListAsync();

            var result = new List<MonthlyViolationModel>();
            for (int i = 5; i >= 0; i--)
            {
                var d = today.AddMonths(-i);
                var count = violations.Count(v => v.ViolationDate.Year == d.Year && v.ViolationDate.Month == d.Month);

                result.Add(new MonthlyViolationModel
                {
                    Year = d.Year,
                    Month = d.Month,
                    MonthLabel = $"Tháng {d.Month}",
                    ViolationCount = count
                });
            }

            return result;
        }
    }
}
