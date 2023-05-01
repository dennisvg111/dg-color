using DG.Color.Tests.Base;
using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests
{
    public class HslTests : ColorConversionTests<HslColor>
    {
        public static IEnumerable<object[]> GetColorData => new List<object[]>
        {
            new object[] { new RgbColor(255, 0, 0, 1), new HslColor(0, 100, 50, 1) },
            new object[] { new RgbColor(17, 95, 37, 1), new HslColor(135, 69, 22, 1) },
            new object[] { new RgbColor(255, 0, 0, 0.5f), new HslColor(0, 100, 50, 0.5f) }
        };

        protected override void AssertColorEqual(HslColor excpected, HslColor actual)
        {
            Assert.Equal(excpected.Hue, actual.Hue, 1f);
            Assert.Equal(excpected.Saturation, actual.Saturation, 1f);
            Assert.Equal(excpected.Lightness, actual.Lightness, 1f);
            Assert.Equal(excpected.Alpha, actual.Alpha);
        }
    }
}
