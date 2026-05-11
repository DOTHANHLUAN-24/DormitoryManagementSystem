using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("RoomType")]
    public class RoomTypeController : Controller
    {
        // Sau này bạn sẽ inject Service vào đây giống như UserController
        // private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController()
        {
        }

        // GET: RoomType
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Trả về view danh sách loại phòng
            return View();
        }
        [Route("Create")]
        // GET: RoomType/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                // Logic lưu vào database sẽ thực hiện ở đây qua Service
                TempData["Success"] = "Thêm loại phòng mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        [Route("Edit/{id}")]
        // GET: RoomType/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Logic lấy dữ liệu theo ID để sửa
            return View();
        }

        // POST: RoomType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                // Logic cập nhật database sẽ thực hiện ở đây qua Service
                TempData["Success"] = "Cập nhật loại phòng thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

    }
}
