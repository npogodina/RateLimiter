using System.Net.Sockets;

namespace RateLimiter
{
    public interface IBucket
    {
        int Tokens { get; }

        void Refill();
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

        public void Refill()
        {
            Console.WriteLine($"{DateTime.Now}: {Tokens}/{Config.BucketSize} available.");

            if (Tokens < Config.BucketSize)
            {
                Tokens = Config.BucketSize;
                Console.WriteLine($"{DateTime.Now}: Bucket refilled");
            }

            LastRefilled = DateTime.Now;
            Console.WriteLine($"{DateTime.Now}: {nameof(LastRefilled)} updated to {LastRefilled}");
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


