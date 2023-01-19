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
        public ActionResult Get()
        {
            var rl = RateLimiterSingleton.Instance;
            if (rl.ShouldForwardRequest() == true)
            {
                return Redirect("https://nataliyap.com/index.html");
            }

            //var retryIn = (((DateTimeOffset)rl.LastRefilled).ToUnixTimeMilliseconds() + Config.RefillRateInSeconds - DateTimeOffset.Now.ToUnixTimeMilliseconds())/1000;

            var retryIn = CalculateRetryIn(rl);
            var message = $"Too many requests within the last {Config.RefillRateInSeconds} seconds. Retry in {retryIn} seconds.";

            return new ObjectResult(message) { StatusCode = 429 };
        }

        #region Helpers 

        private int CalculateRetryIn(RateLimiter rl)
        {
            var lastRefilledUnix = ((DateTimeOffset)rl.LastRefilled).ToUnixTimeSeconds();
            var nextRefillUnix = lastRefilledUnix + Config.RefillRateInSeconds;
            var nowUnix = DateTimeOffset.Now.ToUnixTimeSeconds();

            return Convert.ToInt32(nextRefillUnix - nowUnix);
        }

        #endregion
    }
}