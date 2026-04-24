using DormitoryManagement.Data.Dao;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            var roomDao = new RoomDao();
            var listRooms = roomDao.ListAllRoom();
            var listRoomTypes = roomDao.ListAllRoomType();

            return View();
        }
    }
}
