using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DormitoryManagement.Controllers
{
    // Giới hạn đăng nhập hệ thống mới được vào phân hệ Phụ phí
    [Authorize]
    public class SurchargeController : BaseController
    {
        /// <summary>
        /// GET: Surcharge/Index
        /// Hiển thị danh sách phụ phí kèm theo (Mọi user đăng nhập hợp lệ đều xem được)
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            Logger.LogInformation("Đang truy cập trang danh sách phụ phí.");
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
            Logger.LogInformation("Đang truy cập trang thêm mới phụ phí.");
            if (!User.IsInRole("Admin") && 
                !User.IsInRole("ManagementStaff") && 
                !User.IsInRole("ManagerStaff") && 
                !User.IsInRole("Manager"))
            {
                Logger.LogWarning("Truy cập trang thêm mới phụ phí bị từ chối do không đủ quyền.");
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
        public IActionResult Create(IFormCollection collection)
        {
            Logger.LogInformation("Đang tiếp nhận dữ liệu submit thêm mới phụ phí.");
            try
            {
                // TODO: Ánh xạ dữ liệu từ Form (collection) vào DTO/Entity để lưu cơ sở dữ liệu
                // Ví dụ: var name = collection["Name"];
                
                // Sau khi lưu thành công, quay về trang danh sách
                Logger.LogInformation("Thêm mới phụ phí thành công, chuyển hướng về Index.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thêm mới phụ phí.");
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
            Logger.LogInformation("Đang truy cập giao diện chỉnh sửa phụ phí với mã ID: {Id}", id);
            if (!User.IsInRole("Admin") && 
                !User.IsInRole("ManagementStaff") && 
                !User.IsInRole("ManagerStaff") && 
                !User.IsInRole("Manager"))
            {
                Logger.LogWarning("Truy cập chỉnh sửa bị từ chối cho người dùng hiện tại.");
                return Forbid();
            }

            if (string.IsNullOrEmpty(id))
            {
                Logger.LogWarning("Yêu cầu chỉnh sửa phụ phí thất bại do thiếu ID.");
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
        public IActionResult Edit(string id, IFormCollection collection)
        {
            Logger.LogInformation("Đang xử lý yêu cầu cập nhật thông tin phụ phí ID: {Id}", id);
            try
            {
                // TODO: Xử lý logic cập nhật database tại đây
                Logger.LogInformation("Cập nhật thông tin phụ phí thành công, chuyển hướng về Index.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi cập nhật phụ phí mã {Id}.", id);
                ModelState.AddModelError("", "Không thể lưu các thay đổi. Hãy kiểm tra lại.");
                return View();
            }
        }

        /// <summary>
        /// POST: Surcharge/Delete/{id}
        /// Xử lý xóa phụ phí thông qua nút xóa trên bảng danh sách
        /// </summary>
        [HttpPost]
        public IActionResult Delete(string id)
        {
            Logger.LogInformation("Đang xử lý yêu cầu xóa phụ phí ID: {Id}", id);
            if (!User.IsInRole("Admin") && 
                !User.IsInRole("ManagementStaff") && 
                !User.IsInRole("ManagerStaff") && 
                !User.IsInRole("Manager"))
            {
                Logger.LogWarning("Yêu cầu xóa bị từ chối do không đủ quyền.");
                return Forbid();
            }

            try
            {
                // TODO: Xử lý xóa cứng hoặc xóa mềm (IsDeleted = true) trong DB phụ phí tại đây
                Logger.LogInformation("Xóa phụ phí ID: {Id} thành công.", id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi khi xóa mục phụ phí {Id}.", id);
                return BadRequest("Không thể xóa danh mục này.");
            }
        }
    }
}