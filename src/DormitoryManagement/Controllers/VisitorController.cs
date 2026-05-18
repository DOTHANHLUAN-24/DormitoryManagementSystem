using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("Visitor")] // Sửa lỗi thiếu dấu ']' và rút gọn đường dẫn thành /Visitor
    public class VisitorController : Controller
    {
        // 1. Trang danh sách khách đến thăm (http://localhost:xxxx/Visitor)
        [HttpGet]
        [Route("")] // Đường dẫn mặc định khi vào /Visitor
        public IActionResult Index()
        {
            // Trả về View Index.cshtml sử dụng mock data JavaScript
            return View();
        }

        // 2. Trang ghi nhận khách đến mới (http://localhost:xxxx/Visitor/Create)
        [HttpGet]
        [Route("Create")] // Đường dẫn sẽ là /Visitor/Create
        public IActionResult Create()
        {
            return View();
        }

        // 3. Trang chỉnh sửa thông tin khách (http://localhost:xxxx/Visitor/Edit/{id})
        [HttpGet]
        [Route("Edit/{id}")] // Cấu hình nhận tham số id trực tiếp trên URL
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Truyền ID sang View để hiển thị hoặc xử lý tiếp
            ViewData["VisitorId"] = id;
            
            return View();
        }
    }
}