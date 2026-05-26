using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DormitoryManagement.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace DormitoryManagement.Controllers
{
    /// <summary>
    /// Controller xử lý các logic liên quan đến Báo cáo và Thống kê
    /// </summary>
    [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
    [Route("Statistic")]
    public class StatisticController(IStatisticService statisticService) : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var stats = await statisticService.GetStatisticSummaryAsync();

            // Gán dữ liệu cho các thẻ tóm tắt (Summary Cards)
            ViewBag.TotalStudents = stats.TotalStudents;
            ViewBag.EmptyRooms = stats.EmptyRooms;
            ViewBag.UnpaidInvoices = stats.UnpaidInvoices;
            ViewBag.NewViolations = stats.NewViolations;

            // Gán dữ liệu cho Biểu đồ Tình trạng phòng (Doughnut Chart)
            ViewBag.RoomStatusData = stats.RoomStatusData;

            // Gán dữ liệu cho Biểu đồ Doanh thu (Bar Chart)
            ViewBag.RevenueLabels = stats.RevenueLabels;
            ViewBag.RevenuePaid = stats.RevenuePaid;
            ViewBag.RevenueUnpaid = stats.RevenueUnpaid;

            // Các dữ liệu Thống kê Mở rộng
            ViewBag.MaintenanceStats = stats.MaintenanceStats;
            ViewBag.ContractStats = stats.ContractStats;
            ViewBag.BedOccupancyStats = stats.BedOccupancyStats;

            // Dữ liệu Biểu đồ Vi phạm (Line Chart)
            ViewBag.ViolationLabels = stats.ViolationLabels;
            ViewBag.ViolationData = stats.ViolationData;

            return View();
        }
    }
}
