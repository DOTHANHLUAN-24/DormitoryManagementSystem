using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    [Route("Regulation")]
    public class RegulationController : Controller
    {
        private readonly string _filePath;

        public RegulationController(IWebHostEnvironment env)
        {
            // Tự động trỏ vào đúng thư mục wwwroot bất kể chạy ở máy nào
            _filePath = Path.Combine(env.WebRootPath, "data", "regulation.html");
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            string content = "";
            if (System.IO.File.Exists(_filePath))
            {
                content = System.IO.File.ReadAllText(_filePath);
            }

            ViewBag.Content = content;
            return View();
        }

        [HttpGet("Edit")]
        public IActionResult Edit()
        {
            string content = "Nhập nội quy ở đây...";
            if (System.IO.File.Exists(_filePath))
            {
                content = System.IO.File.ReadAllText(_filePath);
            }

            ViewBag.CurrentContent = content;
            return View();
        }

        [HttpPost("Edit/{Content}")]
        public IActionResult Edit(string Content)
        {
            try
            {
                var directory = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                // Ghi đè file. Nếu Content truyền lên rỗng thì cũng lưu rỗng
                System.IO.File.WriteAllText(_filePath, Content ?? "");

                TempData["Success"] = "Cập nhật Nội quy KTX thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi lưu file: " + ex.Message;
                return RedirectToAction(nameof(Edit));
            }
        }
    }
}