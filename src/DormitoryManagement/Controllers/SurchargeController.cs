using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DormitoryManagement.Controllers
{
    // Giới hạn đăng nhập hệ thống mới được vào phân hệ Phụ phí
    [Authorize]
    public class SurchargeController(ILogger<SurchargeController> logger) : Controller
    {
        private readonly ILogger<SurchargeController> _logger = logger;

        /// <summary>
        /// GET: Surcharge/Index
        /// Hiển thị danh sách phụ phí kèm theo (Mọi user đăng nhập hợp lệ đều xem được)
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            // View này sử dụng Mock Data danh sách cụ thể đã được nhúng sẵn ở phía giao diện frontend
            return View();
        }

        /// <summary>
        /// GET: Surcharge/Create
        /// Giao diện thêm mới phụ phí (Chỉ Admin và các cấp Quản lý có quyền)
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            if (!User.IsInRole("Admin") && 
                !User.IsInRole("ManagementStaff") && 
                !User.IsInRole("ManagerStaff") && 
                !User.IsInRole("Manager"))
            {
                return Forbid(); // Trả về trang 403 nếu cố tình truy cập lậu
            }

            return View();
        }

        /// <summary>
        /// POST: Surcharge/Create
        /// Xử lý tiếp nhận luồng dữ liệu submit từ Form thêm mới gửi lên
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                // TODO: Ánh xạ dữ liệu từ Form (collection) vào DTO/Entity để lưu cơ sở dữ liệu
                // Ví dụ: var name = collection["Name"];
                
                // Sau khi lưu thành công, quay về trang danh sách
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xảy ra khi thêm mới phụ phí.");
                ModelState.AddModelError("", "Đã có lỗi xảy ra hệ thống. Vui lòng thử lại.");
                return View();
            }
        }

        /// <summary>
        /// GET: Surcharge/Edit/{id}
        /// Giao diện chỉnh sửa phụ phí theo Mã định danh (Chỉ dành cho Admin/Manager)
        /// </summary>
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!User.IsInRole("Admin") && 
                !User.IsInRole("ManagementStaff") && 
                !User.IsInRole("ManagerStaff") && 
                !User.IsInRole("Manager"))
            {
                return Forbid();
            }

            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // TODO: Truy vấn database lấy thông tin phụ phí cũ theo `id` (Ví dụ: SUR001) để truyền vào View
            // Hiện tại View Edit đang tự nạp Mock Data mẫu cho code minh họa.
            
            return View();
        }

        /// <summary>
        /// POST: Surcharge/Edit/{id}
        /// Xử lý cập nhật thông tin phụ phí sau khi chỉnh sửa
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Xử lý logic cập nhật database tại đây
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi xảy ra khi cập nhật phụ phí mã {id}.");
                ModelState.AddModelError("", "Không thể lưu các thay đổi. Hãy kiểm tra lại.");
                return View();
            }
        }

        /// <summary>
        /// POST: Surcharge/Delete/{id}
        /// Xử lý xóa phụ phí thông qua nút xóa trên bảng danh sách
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!User.IsInRole("Admin") && 
                !User.IsInRole("ManagementStaff") && 
                !User.IsInRole("ManagerStaff") && 
                !User.IsInRole("Manager"))
            {
                return Forbid();
            }

            try
            {
                // TODO: Xử lý xóa cứng hoặc xóa mềm (IsDeleted = true) trong DB phụ phí tại đây
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa mục phụ phí {id}.");
                return BadRequest("Không thể xóa danh mục này.");
            }
        }
    }
}