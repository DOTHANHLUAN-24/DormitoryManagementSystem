using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormitoryManagement.Infrastructure.Data;
using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DormitoryManagement.Controllers
{

    [Route("Asset")]
    public class AssetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách vật tư
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var assets = await _context.Assets
                .Include(a => a.Room)
                .ToListAsync();
            return View(assets);
        }

        // Giao diện thêm vật tư
        [HttpGet("Create")]
        public IActionResult Create()
        {
            // Lấy danh sách phòng để chọn khi thêm vật tư
            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "RoomNumber");
            return View(new Asset());
        }

        // Xử lý logic thêm vật tư
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Assets.Add(asset);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Thêm vật tư thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Không thể lưu dữ liệu. Vui lòng thử lại.");
                }
            }

            // Load lại danh sách phòng nếu có lỗi
            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "RoomNumber", asset.RoomId);
            return View(asset);
        }
    }
}