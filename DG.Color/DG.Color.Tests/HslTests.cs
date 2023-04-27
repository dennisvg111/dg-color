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

            Assert.InRange(hslColor.Hue, color.Hue - 1, color.Hue + 1);
            Assert.InRange(hslColor.Saturation, color.Saturation - 1, color.Saturation + 1);
            Assert.InRange(hslColor.Lightness, color.Lightness - 1, color.Lightness + 1);
            Assert.InRange(hslColor.Alpha, color.Alpha - 0.001, color.Alpha + 0.001);
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
