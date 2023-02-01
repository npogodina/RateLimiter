namespace RateLimiter
{
    public class BucketRefiller : BackgroundService
    {
        private readonly IBucket _bucket;

        private readonly ILogger<BucketRefiller> _logger;

        public BucketRefiller(ILogger<BucketRefiller> logger)
        {
            _bucket = RateLimiterSingleton.Instance;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var i = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{DateTime.Now}: Checking Tokens in the bucket - loop {i}");

                Refill();

                i++;
                await Task.Delay(Config.RefillRateInSeconds * 1000); // milliseconds
            }
        }

        private void Refill()
        {
            _logger.LogInformation($"{DateTime.Now}: {_bucket.Tokens}/{Config.BucketSize} available.");

            var refilled = _bucket.Refill();
            var message = refilled ? "Bucket refilled" : "Refill not needed";
            _logger.LogInformation($"{DateTime.Now}: {message}");

            _logger.LogInformation($"{DateTime.Now}: {nameof(_bucket.LastRefilled)} updated to {_bucket.LastRefilled}");
        }
    }
}
