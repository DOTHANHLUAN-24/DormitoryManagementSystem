using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data; // Để dùng SaveChangesAsync nếu chưa có UnitOfWork
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("RoomType")]
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly ApplicationDbContext _context;

        public RoomTypeController(IRoomTypeRepository roomTypeRepository, ApplicationDbContext context)
        {
            _roomTypeRepository = roomTypeRepository;
            _context = context; // Inject Context để thực hiện SaveChanges sau khi gọi Repo
        }

        // GET: RoomType
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            int pageSize = 5; // Bạn có thể tăng lên 10 tùy ý

            // Sử dụng GetPagedAsync từ BaseRepository
            var result = await _roomTypeRepository.GetPagedAsync(
                pageIndex: page,
                pageSize: pageSize,
                predicate: x => (string.IsNullOrEmpty(search) || x.TypeName.Contains(search)) && !x.IsDeleted,
                orderBy: x => x.OrderBy(rt => rt.TypeName)
            );

            ViewBag.Search = search;
            return View(result);
        }

        // GET: RoomType/Details/5
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var roomType = await _roomTypeRepository.GetRoomTypeWithRoomsAsync(id);
            if (roomType == null) return NotFound();

            return View(roomType);
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
        [Route("Create")]
        public async Task<IActionResult> Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng tên loại phòng
                if (await _roomTypeRepository.IsTypeNameDuplicateAsync(roomType.TypeName))
                {
                    ModelState.AddModelError("TypeName", "Tên loại phòng này đã tồn tại trong hệ thống.");
                    return View(roomType);
                }

                await _roomTypeRepository.AddAsync(roomType);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thêm loại phòng mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        [Route("Edit/{id}")]
        // GET: RoomType/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null) return NotFound();

            return View(roomType);
        }

        // POST: RoomType/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, RoomType roomType)
        {
            if (id != roomType.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                // Kiểm tra tên mới có bị trùng với loại phòng khác không
                if (await _roomTypeRepository.IsTypeNameDuplicateAsync(roomType.TypeName, id))
                {
                    ModelState.AddModelError("TypeName", "Tên loại phòng này đã được sử dụng.");
                    return View(roomType);
                }

                try
                {
                    // Lấy bản ghi gốc từ DB để tránh mất dữ liệu Audit (CreatedDate)
                    var existing = await _roomTypeRepository.GetByIdAsync(id);
                    if (existing == null) return NotFound();

                    // Cập nhật các trường thông tin
                    existing.TypeName = roomType.TypeName;
                    existing.BasePrice = roomType.BasePrice;
                    existing.MaxOccupants = roomType.MaxOccupants;
                    existing.Description = roomType.Description;
                    existing.IsActive = roomType.IsActive;

                    await _roomTypeRepository.UpdateAsync(existing);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Cập nhật loại phòng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình cập nhật.");
                }
            }
            return View(roomType);
        }

        // POST: RoomType/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null) return NotFound();

            // Quy trình nghiệp vụ: Kiểm tra xem loại phòng này có đang chứa phòng nào không
            var withRooms = await _roomTypeRepository.GetRoomTypeWithRoomsAsync(id);
            if (withRooms != null && withRooms.Rooms.Any(r => !r.IsDeleted))
            {
                TempData["Error"] = "Không thể xóa loại phòng này vì đang có phòng thuộc danh mục này.";
                return RedirectToAction(nameof(Index));
            }

            await _roomTypeRepository.DeleteAsync(roomType, isSoftDelete: true);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Xóa loại phòng thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}