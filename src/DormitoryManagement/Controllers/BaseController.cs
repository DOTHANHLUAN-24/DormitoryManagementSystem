using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BaseController : Controller
    {
        public static int DefaultPageSize { get; set; } = 5;

        protected int PageSize
        {
            get
            {
                if (HttpContext?.Request != null)
                {
                    if (Request.Query.TryGetValue("pageSize", out var qsPageSize) && int.TryParse(qsPageSize, out int ps) && ps > 0)
                    {
                        return ps;
                    }
                    if (Request.Query.TryGetValue("PageSize", out var qsPageSize2) && int.TryParse(qsPageSize2, out int ps2) && ps2 > 0)
                    {
                        return ps2;
                    }
                }
                return DefaultPageSize;
            }
        }

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