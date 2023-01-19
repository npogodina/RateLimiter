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
                    Console.WriteLine($"{DateTime.Now}: {bucket.Tokens}/{Config.BucketSize} available.");
                    
                    bucket.Refill();
                    Console.WriteLine($"{DateTime.Now}: Bucket refilled");

                    Thread.Sleep(Config.RefillRateInSeconds * 1000);
                }
            }
        }
    }
}
