using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Route("Service")]
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }
    }
}
