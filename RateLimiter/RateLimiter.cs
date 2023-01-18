namespace RateLimiter
{
    public class RateLimiter
    {
        private readonly int _bucketSize;

        public static int AvailableTokens { get; private set; }

        public RateLimiter(int bucketSize)
        {
            _bucketSize = bucketSize;
            AvailableTokens = _bucketSize;
        }

        public bool ShouldForwardRequest()
        {
            if (AvailableTokens > 0)
            {
                AvailableTokens--;
                return true;
            }

            return false;
        }
    }

    public class RateLimiterSingleton
    {
        private static RateLimiter _rateLimiter = new RateLimiter(3);

        private RateLimiterSingleton()
        {

        }

        public static RateLimiter Instance
        {
            get { return _rateLimiter; }
        }
    }
}


