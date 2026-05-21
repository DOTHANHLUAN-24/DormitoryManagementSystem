using System.Diagnostics;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DormitoryManagement.Controllers
{
    [Route("/")]
    [AllowAnonymous]
    public class HomeController(
        IRoomService roomService,
        IUtilityService utilityService,
        IBlockService blockService,
        IRoomTypeService roomTypeService
    ) : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Rooms")]
        public async Task<IActionResult> Rooms(RoomFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = 12; // 3 columns * 4 rows

            var pagedRooms = await roomService.GetPagedRoomsAsync(filter);

            var blocks = await blockService.GetAllBlocksAsync();
            var roomTypes = await roomTypeService.GetAllRoomTypesAsync();

            ViewBag.Blocks = new SelectList(blocks, "Id", "BlockName", filter.BlockId);
            ViewBag.RoomTypes = new SelectList(roomTypes, "Id", "TypeName", filter.RoomTypeId);
            ViewBag.Filter = filter;

            return View(pagedRooms);
        }

        [HttpGet("Rooms/Details/{id}")]
        public async Task<IActionResult> RoomDetails(Guid id)
        {
            var room = await roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpGet("Services")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Services()
        {
            var utilities = await utilityService.GetAllActiveUtilitiesAsync();
            return View(utilities);
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet("Guide")]
        public IActionResult Guide()
        {
            return View();
        }
    }
}
