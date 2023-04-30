using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests
{
    public class HslTests
    {
        public static IEnumerable<object[]> ConversionTestData => new List<object[]>
        {
            new object[] { new RgbaValues(255, 0, 0, 1), new HslColor(0, 100, 50, 1) },
            new object[] { new RgbaValues(17, 95, 37, 1), new HslColor(135, 69, 22, 1) },
            new object[] { new RgbaValues(255, 0, 0, 0.5f), new HslColor(0, 100, 50, 0.5f) }
        };

        [Theory]
        [MemberData(nameof(ConversionTestData))]
        public void Conversion(RgbaValues values, HslColor color)
        {
            var rgbColor = new RgbColor(values);

            var hslColor = rgbColor.To<HslColor>();

            Assert.Equal(color.Hue, hslColor.Hue, 1f);
            Assert.Equal(color.Saturation, hslColor.Saturation, 1f);
            Assert.Equal(color.Lightness, hslColor.Lightness, 1f);
            Assert.Equal(color.Alpha, hslColor.Alpha);
        }

        [Theory]
        [MemberData(nameof(ConversionTestData))]
        public void ReverseConversion(RgbaValues values, HslColor color)
        {
            var rgbColor = color.To<RgbColor>();

            Assert.Equal(values.Red, rgbColor.Red);
            Assert.Equal(values.Green, rgbColor.Green);
            Assert.Equal(values.Blue, rgbColor.Blue);
            Assert.Equal(values.Alpha, rgbColor.Alpha);
        }
    }
}
