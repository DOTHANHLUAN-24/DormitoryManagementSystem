using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Route("Regulation")]
    public class RegulationController(IWebHostEnvironment env) : BaseController
    {
        private readonly string _filePath = Path.Combine(env.WebRootPath, "data", "regulation.html");

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            Logger.LogInformation("Đang truy cập trang hiển thị nội quy KTX.");
            string content = "";
            if (System.IO.File.Exists(_filePath))
            {
                content = await System.IO.File.ReadAllTextAsync(_filePath);
            }
            else
            {
                Logger.LogWarning("Không tìm thấy tệp tin nội quy tại đường dẫn: {FilePath}", _filePath);
            }

            ViewBag.Content = content;
            return View();
        }

        [HttpGet("Edit")]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        public async Task<IActionResult> Edit()
        {
            Logger.LogInformation("Đang truy cập trang chỉnh sửa nội quy KTX.");
            string content = "Nhập nội quy ở đây...";
            if (System.IO.File.Exists(_filePath))
            {
                content = await System.IO.File.ReadAllTextAsync(_filePath);
            }

            ViewBag.CurrentContent = content;
            return View();
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagementStaff,ManagerStaff,Manager")]
        public async Task<IActionResult> Edit(string content)
        {
            Logger.LogInformation("Đang xử lý yêu cầu cập nhật nội quy KTX.");
            try
            {
                var directory = Path.GetDirectoryName(_filePath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Ghi đè file. Nếu content truyền lên rỗng thì cũng lưu rỗng
                await System.IO.File.WriteAllTextAsync(_filePath, content ?? "");
                Logger.LogInformation("Cập nhật nội quy KTX thành công.");

                TempData["Success"] = "Cập nhật Nội quy KTX thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi lưu nội quy KTX vào file: {FilePath}", _filePath);
                ModelState.AddModelError(string.Empty, "Lỗi khi lưu file: " + ex.Message);
                ViewBag.CurrentContent = content; // Giữ lại nội dung người dùng vừa nhập để không bị mất
                return View();
            }
        }
    }
}