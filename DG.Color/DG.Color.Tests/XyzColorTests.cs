using DG.Color.Tests.Base;
using Xunit;

namespace DG.Color.Tests
{
    public class XyzColorTests : ColorConversionTests<XyzColor, XyzColorTests>, IConversionTestDataProvider<XyzColor>
    {
        public ColorConversionTestData<XyzColor>[] GetTestData()
        {
            return new ColorConversionTestData<XyzColor>[]
            {
                new ColorConversionTestData<XyzColor>(new RgbColor(0, 0, 0), new XyzColor(0, 0, 0, 1)),
                new ColorConversionTestData<XyzColor>(new RgbColor(17, 95, 37), new XyzColor(0.0466, 0.0844, 0.0313, 1)),
                new ColorConversionTestData<XyzColor>(new RgbColor(255, 0, 0, 0.5f), new XyzColor(0.4124, 0.2126, 0.0193, 0.5f))
            };
        }

        protected override void AssertColorEqual(XyzColor excpected, XyzColor actual)
        {
            Assert.Equal(excpected.X, actual.X, 0.00005f);
            Assert.Equal(excpected.Y, actual.Y, 0.00005f);
            Assert.Equal(excpected.Z, actual.Z, 0.00005f);
        }
    }
}
