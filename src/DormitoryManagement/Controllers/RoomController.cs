using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace DormitoryManagement.Controllers
{
    [Route("Room")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;// Khai báo thêm các service cần thiết để lấy dữ liệu cho dropdown
        private readonly IBlockService _blockService;// Dùng để lấy danh sách tòa nhà
        private readonly IRoomTypeService _roomTypeService;// Dùng để lấy danh sách loại phòng

        public RoomController(
            IRoomService roomService,
            IBlockService blockService,
            IRoomTypeService roomTypeService)
        {
            _roomService = roomService;
            _blockService = blockService;
            _roomTypeService = roomTypeService;
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
            // if (room == null)
            // {
            //     return NotFound();
            // }

            // Tường minh chỉ định trả về file "Details.cshtml" ở bên trong thư mục Views/Room
            return View(room);
        }

        // GET: Room/Create
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
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
            await PopulateDropdownsAsync();
            return View(room);
        }

        // GET: Room/Edit/5
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await _roomService.GetRoomDetailAsync(id);

            await PopulateDropdownsAsync();
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
            await PopulateDropdownsAsync();
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

        /// <summary>
        /// Hàm hỗ trợ đổ dữ liệu vào ViewBag cho các Dropdown trong View
        /// </summary>
        private async Task PopulateDropdownsAsync()
        {
            // Lấy danh sách Tòa nhà từ database
            var blocks = await _blockService.GetAllBlocksAsync();
            ViewBag.Blocks = blocks.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.BlockName
            }).ToList();

            // Lấy danh sách Loại phòng từ database
            var roomTypes = await _roomTypeService.GetAllRoomTypesAsync();
            ViewBag.RoomTypes = roomTypes.Select(rt => new SelectListItem
            {
                Value = rt.Id.ToString(),
                Text = rt.TypeName // Thay đổi thành Name hoặc TypeName tùy theo Class RoomType của bạn
            }).ToList();

            // Lấy danh sách trạng thái từ Enum
            ViewBag.RoomStatuses = Enum.GetValues(typeof(RoomStatus))
                .Cast<RoomStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();
        }
    }
}
