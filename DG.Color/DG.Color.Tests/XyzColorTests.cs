using DG.Color.Tests.Base;
using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests
{
    public class XyzColorTests : ColorConversionTests<XyzColor>
    {
        public static IEnumerable<object[]> GetColorData => new List<object[]>
        {
            new object[] { new RgbColor(0, 0, 0), new XyzColor(0, 0, 0, 1) },
            new object[] { new RgbColor(17, 95, 37), new XyzColor(0.0466, 0.0844, 0.0313, 1) },
            new object[] { new RgbColor(255, 0, 0, 0.5f), new XyzColor(0.4124, 0.2126, 0.0193, 0.5f) }
        };

        protected override void AssertColorEqual(XyzColor excpected, XyzColor actual)
        {
            Assert.Equal(excpected.X, actual.X, 0.00005f);
            Assert.Equal(excpected.Y, actual.Y, 0.00005f);
            Assert.Equal(excpected.Z, actual.Z, 0.00005f);
        }
    }
}
