using DG.Color.Tests.Base;
using Xunit;

namespace DG.Color.Tests
{
    public class HslColorTests : ColorConversionTests<HslColor, HslColorTests>, IConversionTestDataProvider<HslColor>
    {

        protected override void AssertColorEqual(HslColor excpected, HslColor actual)
        {
            Assert.Equal(excpected.Hue, actual.Hue, 0.005f);
            Assert.Equal(excpected.Saturation, actual.Saturation, 0.005f);
            Assert.Equal(excpected.Lightness, actual.Lightness, 0.005f);
        }

        public ColorConversionTestData<HslColor>[] GetTestData()
        {
            return new ColorConversionTestData<HslColor>[]
            {
                new ColorConversionTestData<HslColor>(new RgbColor(255, 0, 0, 1), new HslColor(0, 100, 50, 1)),
                new ColorConversionTestData<HslColor>(new RgbColor(17, 95, 37, 1), new HslColor(135.38f, 69.64f, 21.96f, 1)),
                new ColorConversionTestData<HslColor>(new RgbColor(111, 89, 212, 1), new HslColor(251f, 59f, 59f, 1)),
                new ColorConversionTestData<HslColor>(new RgbColor(255, 0, 0, 0.5f), new HslColor(0, 100, 50, 0.5f))
            };
        }
    }
}
