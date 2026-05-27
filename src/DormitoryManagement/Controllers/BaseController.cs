using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BaseController : Controller
    {
        public static int PageSize { get; set; } = 5;

        private ILogger? _logger;
        protected ILogger Logger => _logger ??= HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(GetType());

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