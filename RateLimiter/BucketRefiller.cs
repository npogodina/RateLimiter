namespace RateLimiter
{
    public class BucketRefiller
    {
        public void Refill(IBucket bucket)
        {
            while (true)
            {
                if (bucket.Tokens < Config.BucketSize)
                {
                    bucket.Refill();
                    Console.WriteLine("Bucket refilled");
                    Thread.Sleep(Config.RefillRateInMilliseconds);
                }
            }
        }
    }
}
