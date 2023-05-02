using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DG.Color.Tests.Base
{
    public abstract class ColorConversionTests<TColor, TProvider> where TColor : ConvertibleColor<TColor> where TProvider : IConversionTestDataProvider<TColor>, new()
    {
        protected abstract void AssertColorEqual(TColor expected, TColor actual);

        public static IEnumerable<object[]> GetConversionData()
        {
            TProvider provider = new TProvider();
            var colors = provider.GetTestData();
            return colors.Select(c => c.ToObjectArray());
        }

        [Theory]
        [MemberData(nameof(GetConversionData))]
        public void ConvertFromRgb(RgbColor rgbColor, TColor expected)
        {
            var actual = rgbColor.To<TColor>();

            AssertColorEqual(expected, actual);
            Assert.Equal(expected.Alpha, actual.Alpha);
        }

        [Theory]
        [MemberData(nameof(GetConversionData))]
        public void ConvertToRgb(RgbColor expected, TColor color)
        {
            var actual = color.To<RgbColor>();

            Assert.Equal(expected.Red, actual.Red);
            Assert.Equal(expected.Green, actual.Green);
            Assert.Equal(expected.Blue, actual.Blue);
            Assert.Equal(expected.Alpha, actual.Alpha);
        }
    }

    public interface IConversionTestDataProvider<T> where T : ConvertibleColor<T>
    {
        ColorConversionTestData<T>[] GetTestData();
    }

    public class ColorConversionTestData<T>
    {
        public RgbColor RgbColor { get; set; }
        public T CorrespondingColor { get; set; }

        public ColorConversionTestData(RgbColor rgbColor, T correspondingColor)
        {
            RgbColor = rgbColor;
            CorrespondingColor = correspondingColor;
        }

        public object[] ToObjectArray()
        {
            return new object[] { RgbColor, CorrespondingColor };
        }
    }
}
