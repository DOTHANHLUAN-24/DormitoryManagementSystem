using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Route("Room")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: Room
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetActiveRoomsAsync();
            return View(rooms);
        }

        // GET: Room/Details/5
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var room = await _roomService.GetRoomDetailAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            // Tường minh chỉ định trả về file "Details.cshtml" ở bên trong thư mục Views/Room
            return View(room);
        }

        // GET: Room/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _roomService.CreateNewRoomAsync(room);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Tạo mới phòng bị lỗi vui lòng kiểm tra lại", ex.Message);
                }
            }
            return View(room);
        }

        // GET: Room/Edit/5
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await _roomService.GetRoomDetailAsync(id);
            // if (room == null)
            // {
            //     return NotFound();
            // }
            return View(room);
        }

        // POST: Room/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, Room room)
        {
            if (id != room.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    await _roomService.UpdateRoomInfoAsync(room);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _roomService.RemoveRoomAsync(id);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    }
}
