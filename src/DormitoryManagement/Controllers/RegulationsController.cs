using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class RegulationsController : Controller
    {
        // GET: /Regulations/Index
        public IActionResult Index()
        {
            ViewData["Title"] = "Nội quy Ký túc xá";
            return View();
        }
    }
}