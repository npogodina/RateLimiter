namespace RateLimiter
{
    public class BucketRefiller : BackgroundService
    {
        private readonly ILogger<BucketRefiller> _logger;

        public BucketRefiller(ILogger<BucketRefiller> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bucket = RateLimiterSingleton.Instance;

            var i = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{DateTime.Now}: Checking Tokens in the bucket - loop {i}");

                bucket.Refill();

                i++;
                await Task.Delay(Config.RefillRateInSeconds * 1000); // milliseconds
            }
        }
    }
}
