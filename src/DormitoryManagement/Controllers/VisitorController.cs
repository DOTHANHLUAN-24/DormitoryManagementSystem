using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class VisitorController : BaseController
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("Edit/{id}")]
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