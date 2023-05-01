using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests
{
    public class XyzColorTests
    {
        public static IEnumerable<object[]> ConversionTestData => new List<object[]>
        {
            new object[] { new RgbaValues(0, 0, 0, 1), new XyzColor(0, 0, 0, 1) },
            new object[] { new RgbaValues(17, 95, 37, 1), new XyzColor(0.0466, 0.0844, 0.0313, 1) },
            new object[] { new RgbaValues(255, 0, 0, 0.5f), new XyzColor(0.4124, 0.2126, 0.0193, 0.5f) }
        };

        [Theory]
        [MemberData(nameof(ConversionTestData))]
        public void Conversion(RgbaValues values, XyzColor color)
        {
            var rgbColor = new RgbColor(values);

            var xyzColor = rgbColor.To<XyzColor>();

            Assert.Equal(color.X, xyzColor.X, 0.00005f);
            Assert.Equal(color.Y, xyzColor.Y, 0.00005f);
            Assert.Equal(color.Z, xyzColor.Z, 0.00005f);
        }

        [Theory]
        [MemberData(nameof(ConversionTestData))]
        public void ReverseConversion(RgbaValues values, XyzColor color)
        {
            var rgbColor = color.To<RgbColor>();

            Assert.Equal(values.Red, rgbColor.Red);
            Assert.Equal(values.Green, rgbColor.Green);
            Assert.Equal(values.Blue, rgbColor.Blue);
            Assert.Equal(values.Alpha, rgbColor.Alpha);
        }
    }
}
