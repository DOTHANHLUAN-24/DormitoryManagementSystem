using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class BlockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
