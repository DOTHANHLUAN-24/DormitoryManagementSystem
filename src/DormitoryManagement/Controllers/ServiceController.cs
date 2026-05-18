using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormitoryManagement.Infrastructure.Data;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Controllers
{
    [Route("Service")]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách dịch vụ đang hoạt động
        public async Task<IActionResult> Index()
        {
            var services = await _context.Utilities
                .Where(u => u.IsActive)
                .ToListAsync();
            return View(services);
        }

        // Hiển thị danh sách dịch vụ trong Thùng rác
        [HttpGet("Trash")]
        public async Task<IActionResult> Trash()
        {
            var deletedServices = await _context.Utilities
                .Where(u => !u.IsActive)
                .ToListAsync();
            return View(deletedServices);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View(new Utility());
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var service = await _context.Utilities.FindAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        // Xóa mềm: Chuyển trạng thái IsActive thành false
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var service = await _context.Utilities.FindAsync(id);
            if (service != null)
            {
                service.IsActive = false;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã đưa dịch vụ vào thùng rác.";
            }
            return RedirectToAction(nameof(Index));
        }

        // Khôi phục: Chuyển trạng thái IsActive thành true
        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            var service = await _context.Utilities.FindAsync(id);
            if (service != null)
            {
                service.IsActive = true;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Khôi phục dịch vụ thành công.";
            }
            return RedirectToAction(nameof(Trash));
        }

        // Xóa vĩnh viễn: Gỡ bỏ hoàn toàn khỏi Database
        [HttpPost("HardDelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            var service = await _context.Utilities.FindAsync(id);
            if (service != null)
            {
                _context.Utilities.Remove(service);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã xóa vĩnh viễn dịch vụ.";
            }
            return RedirectToAction(nameof(Trash));
        }
    }
}
