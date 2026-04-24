using DormitoryManagement.Data.Dao;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            var userDao = new UserDao();
            var listUsers = userDao.ListAllPaging("", 1, 10);

            return View(listUsers);
        }
    }
}
