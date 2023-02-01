namespace RateLimiter
{
    public interface IBucket
    {
        int Tokens { get; }

        DateTime LastRefilled { get; }

        bool Refill();
    }

    public class RateLimiter : IBucket
    {
        public int Tokens { get; private set; }

        public DateTime LastRefilled { get; private set; }

        public RateLimiter()
        {
            Tokens = Config.BucketSize;
            LastRefilled = DateTime.Now;
        }

        public bool ShouldForwardRequest()
        {
            if (Tokens > 0)
            {
                Tokens--;
                return true;
            }

            return false;
        }

        public bool Refill()
        {
            var refilled = false;

            if (Tokens < Config.BucketSize)
            {
                Tokens = Config.BucketSize;
                refilled = true;
            }

            LastRefilled = DateTime.Now;
            return refilled;
        }
    }

    public class RateLimiterSingleton
    {
        private static RateLimiter _rateLimiter = new RateLimiter();

        private RateLimiterSingleton()
        {

        }

        public static RateLimiter Instance
        {
            get { return _rateLimiter; }
        }
    }
}


