using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BaseController : Controller
    {
        public static int PageSize { get; set; } = 5;

        protected string? CurrentUserName =>
            User.Identity?.Name;

        protected Guid? CurrentUserId
        {
            get
            {
                var userId = User.FindFirst("UserId")?.Value;

                return Guid.TryParse(userId, out var id)
                    ? id
                    : null;
            }
        }
    }
}