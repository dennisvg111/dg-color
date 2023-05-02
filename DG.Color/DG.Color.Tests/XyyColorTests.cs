using DG.Color.Tests.Base;
using Xunit;

namespace DG.Color.Tests
{
    public class XyyColorTests : ColorConversionTests<XyyColor, XyyColorTests>, IConversionTestDataProvider<XyyColor>
    {
        public ColorConversionTestData<XyyColor>[] GetTestData()
        {
            return new ColorConversionTestData<XyyColor>[]
            {
                new ColorConversionTestData<XyyColor>(new RgbColor(0, 0, 0, 1), new XyyColor(0, 0, 0, 1)),
                new ColorConversionTestData<XyyColor>(new RgbColor(255, 255, 255, 1), new XyyColor(0.3127, 0.3290, 1, 1)),
                new ColorConversionTestData<XyyColor>(new RgbColor(255, 255, 0, 1), new XyyColor(0.4193, 0.5053, 0.9278, 1)),
                new ColorConversionTestData<XyyColor>(new RgbColor(17, 95, 37, 1), new XyyColor(0.2870, 0.5199, 0.0844, 1)),
                new ColorConversionTestData<XyyColor>(new RgbColor(254,54,11, 1), new XyyColor(0.6154, 0.3457, 0.2373, 1)),
                new ColorConversionTestData<XyyColor>(new RgbColor(255, 0, 0, 0.5f), new XyyColor(0.6401, 0.3300, 0.2126, 0.5f))
            };
        }

        protected override void AssertColorEqual(XyyColor expected, XyyColor actual)
        {
            Assert.Equal(expected.X, actual.X, 0.00005f);
            Assert.Equal(expected.SmallY, actual.SmallY, 0.00005f);
            Assert.Equal(expected.LargeY, actual.LargeY, 0.00005f);
        }
    }
}
