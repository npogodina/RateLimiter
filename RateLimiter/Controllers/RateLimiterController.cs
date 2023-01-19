using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public ActionResult Get()
        {
            var rl = RateLimiterSingleton.Instance;
            if (rl.ShouldForwardRequest() == true)
            {
                return Redirect("https://nataliyap.com/index.html");
            }

            var message = $"Too many requests within the last {Config.RefillRateInMilliseconds} milliseconds.";

            return new ObjectResult(message) { StatusCode = 429 };
        }
    }
}