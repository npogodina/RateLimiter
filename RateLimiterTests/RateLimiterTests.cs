using RateLimiter;

namespace RateLimiterTests
{
    public class RateLimiterTests
    {
        private RateLimiter.RateLimiter rl = RateLimiterSingleton.Instance;

        [Test]
        public void RateLimiterForwardsRequestsWhenItHasTokens()
        {
            rl.Refill();
            Assert.That(rl.Tokens, Is.GreaterThan(0));
            Assert.That(rl.ShouldForwardRequest(), Is.True);          
        }

        [Test]
        public void RateLimiterWillNotForwardWhenOutOfTokens()
        {
            UseUpAllTokens();
            Assert.That(rl.ShouldForwardRequest(), Is.False);
        }

        [Test]
        public void RefillsBucket()
        {
            UseUpAllTokens();
            rl.Refill();
            Assert.That(rl.Tokens, Is.EqualTo(Config.BucketSize));
        }

        [Test]
        public void UpdatesLastRefilledValueWhenRefills()
        {
            var initial = rl.LastRefilled;
            rl.Refill();
            Assert.That(DateTime.Compare(initial, rl.LastRefilled), Is.LessThan(0));
        }

        #region Helpers
        private void UseUpAllTokens()
        {
            while (rl.Tokens > 0)
            {
                rl.ShouldForwardRequest();
            }
        }
        #endregion
    }
}