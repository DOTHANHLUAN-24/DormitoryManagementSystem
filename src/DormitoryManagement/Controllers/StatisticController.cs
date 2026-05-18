using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DormitoryManagement.Controllers
{
    /// <summary>
    /// Controller xử lý các logic liên quan đến Báo cáo và Thống kê
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [Route("Statistic")]
    public class StatisticController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            // Mock dữ liệu cho các thẻ tóm tắt (Summary Cards)
            ViewBag.TotalStudents = 1250;
            ViewBag.EmptyRooms = 45;
            ViewBag.UnpaidInvoices = 12;
            ViewBag.NewViolations = 8;

            // Mock dữ liệu cho Biểu đồ Tình trạng phòng (Doughnut Chart)
            // Thứ tự: [Đã đầy, Còn trống, Bảo trì]
            ViewBag.RoomStatusData = new int[] { 185, 40, 15 };

            // Mock dữ liệu cho Biểu đồ Doanh thu (Bar Chart)
            ViewBag.RevenueLabels = new string[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6" };
            ViewBag.RevenuePaid = new int[] { 150, 180, 165, 200, 210, 195 }; // Triệu VNĐ
            ViewBag.RevenueUnpaid = new int[] { 15, 10, 25, 5, 12, 18 };    // Triệu VNĐ

            return View();
        }
    }
}
