namespace RateLimiter
{
    public interface IBucket
    {
        int Tokens { get; }

        void Refill();
    }

    public class RateLimiter : IBucket
    {
        private readonly int _bucketSize;

        public int Tokens { get; private set; }

        public DateTime LastRefilled { get; private set; }

        public RateLimiter()
        {
            _bucketSize = Config.BucketSize;
            Tokens = _bucketSize;
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

        public void Refill()
        {
            Tokens = Config.BucketSize;
            LastRefilled = DateTime.Now;
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


