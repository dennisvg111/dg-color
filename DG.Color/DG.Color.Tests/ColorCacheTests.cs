using DG.Color.Utilities;
using Xunit;

namespace DG.Color.Tests
{
    public class ColorCacheTests
    {
        [Fact]
        public void CacheTest()
        {
            ColorTransformationWithCounter counter = new ColorTransformationWithCounter();
            ColorCache cache = new ColorCache((c) => counter.Transform(c));
            RgbValues r1 = new RgbValues(128, 0, 0);
            RgbValues r2 = new RgbValues(255, 255, 255);

            var transformed = cache.GetResultOrCreate(r1);
            transformed = cache.GetResultOrCreate(r1);

            Assert.Equal(1, counter.Calls);

            transformed = cache.GetResultOrCreate(r2);
            transformed = cache.GetResultOrCreate(r1);

            Assert.Equal(2, counter.Calls);
        }

        private class ColorTransformationWithCounter
        {
            private int _calls;
            public int Calls => _calls;

            public RgbValues Transform(RgbValues values)
            {
                _calls++;
                return new RgbValues(values.Red, values.Red, values.Red);
            }
        }
    }
}
