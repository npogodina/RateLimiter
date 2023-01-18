using Microsoft.AspNetCore.Mvc;

namespace RateLimiter.Controllers
{
    [ApiController]
    [Route("/")]
    public class RateLimiterController : ControllerBase
    {
        private readonly ILogger<RateLimiterController> _logger;

        public RateLimiterController(ILogger<RateLimiterController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRateLimiter")]
        public RedirectResult Get()
        {
            return Redirect("https://nataliyap.com/index.html");
        }
    }
}