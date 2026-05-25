using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public async Task<IActionResult> Index()
        {
            // Giả lập danh sách để hiển thị giao diện
            return View();
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(object vehicleDto)
        {
            if (ModelState.IsValid)
            {
                // Gọi service lưu dữ liệu
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleDto);
        }

        // GET: Vehicle/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            // GIẢ LẬP: Tìm dữ liệu dựa trên ID (Trong thực tế là gọi Service)
            ViewBag.VehicleId = id;

            // Để demo không bị hiện sai thông tin, tôi gán dữ liệu mẫu linh hoạt
            ViewBag.OwnerName = "Lê Thị Cẩm Tú (SV2321050120)";
            ViewBag.LicensePlate = "88-AK 02486";
            ViewBag.VehicleType = "Xe máy";
            ViewBag.Note = "Phương tiện đã kiểm tra định kỳ.";

            // Load danh sách chủ sở hữu cho Dropdown
            ViewBag.Owners = new List<dynamic> {
                new { Id = Guid.NewGuid(), Name = "Đỗ Thành Luân (SV2221050048)" },
                new { Id = id, Name = "Lê Thị Cẩm Tú (SV2321050120)" },
                new { Id = Guid.NewGuid(), Name = "Đỗ Quang Huy (SV2221050047)" }
            };

            return View();
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, object vehicleDto)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Cập nhật phương tiện thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleDto);
        }

        // API phục vụ tìm kiếm cho Select2
        [HttpGet]
        public async Task<IActionResult> SearchUsers(string q)
        {
            // GIẢ LẬP: Danh sách người dùng trong hệ thống
            var allUsers = new List<dynamic> {
                new { id = "SV2221050048", text = "SV2221050048 - Đỗ Thành Luân" },
                new { id = "SV2321050120", text = "SV2321050120 - Lê Thị Cẩm Tú" },
                new { id = "SV2221050047", text = "SV2221050047 - Đỗ Quang Huy" },
                new { id = "SV2221050566", text = "SV2221050566 - Vũ Thị Kim Oanh" },
                new { id = "QL001", text = "QL001 - Quản lý KTX" }
            };

            if (string.IsNullOrWhiteSpace(q))
            {
                return Json(new { items = allUsers.Take(10) });
            }

            // Tìm kiếm không phân biệt hoa thường trong cả mã và tên
            var filtered = allUsers
                .Where(u => u.text.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Json(new { items = filtered });
        }
    }
}
