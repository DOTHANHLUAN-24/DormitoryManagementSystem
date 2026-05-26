using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    public class VehicleController : BaseController
    {
        // GET: Vehicle
        public IActionResult Index()
        {
            Logger.LogInformation("Đang truy cập trang danh sách phương tiện.");
            // Giả lập danh sách để hiển thị giao diện
            return View();
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang đăng ký phương tiện mới.");
            return View();
        }

        // POST: Vehicle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(object vehicleDto)
        {
            Logger.LogInformation("Đang xử lý yêu cầu đăng ký phương tiện mới.");
            if (ModelState.IsValid)
            {
                // Gọi service lưu dữ liệu
                Logger.LogInformation("Đăng ký phương tiện thành công, chuyển hướng về Index.");
                return RedirectToAction(nameof(Index));
            }
            Logger.LogWarning("Dữ liệu đăng ký phương tiện không hợp lệ.");
            return View(vehicleDto);
        }

        // GET: Vehicle/Edit/5
        public IActionResult Edit(Guid id)
        {
            Logger.LogInformation("Đang truy cập trang chỉnh sửa phương tiện ID: {Id}", id);
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
        public IActionResult Edit(Guid id, object vehicleDto)
        {
            Logger.LogInformation("Đang xử lý yêu cầu cập nhật phương tiện ID: {Id}", id);
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Cập nhật phương tiện thành công!";
                Logger.LogInformation("Cập nhật phương tiện thành công, chuyển hướng về Index.");
                return RedirectToAction(nameof(Index));
            }
            Logger.LogWarning("Dữ liệu cập nhật phương tiện không hợp lệ.");
            return View(vehicleDto);
        }

        // API phục vụ tìm kiếm cho Select2
        [HttpGet]
        public IActionResult SearchUsers(string q)
        {
            Logger.LogInformation("Đang thực hiện tìm kiếm người dùng cho phương tiện với từ khóa: {Query}", q);
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
