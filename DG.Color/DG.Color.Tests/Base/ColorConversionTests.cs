using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests.Base
{
    public abstract class ColorConversionTests<T> where T : ConvertibleColor<T>
    {
        protected abstract void AssertColorEqual(T excpected, T actual);

        [Theory]
#pragma warning disable xUnit1015 // MemberData must reference an existing member
        [MemberData("GetColorData")]
#pragma warning restore xUnit1015 // MemberData must reference an existing member
        public void Conversion(RgbColor rgbColor, T expected)
        {
            var actual = rgbColor.To<T>();

            AssertColorEqual(expected, actual);
            Assert.Equal(expected.Alpha, actual.Alpha);
        }

        [Theory]
#pragma warning disable xUnit1015 // MemberData must reference an existing member
        [MemberData("GetColorData")]
#pragma warning restore xUnit1015 // MemberData must reference an existing member
        public void ReverseConversion(RgbColor expected, T color)
        {
            var actual = color.To<RgbColor>();

            Assert.Equal(expected.Red, actual.Red);
            Assert.Equal(expected.Green, actual.Green);
            Assert.Equal(expected.Blue, actual.Blue);
            Assert.Equal(expected.Alpha, actual.Alpha);
        }

        public static IEnumerable<object[]> Unused()
        {
            return new List<object[]>();
        }
    }

    public class ColorConversionTestData<T> : IEnumerable<object> where T : ConvertibleColor<T>
    {
        public RgbColor RgbColor { get; set; }
        public T CorrespondingColor { get; set; }

        public ColorConversionTestData(RgbColor rgbColor, T correspondingColor)
        {
            RgbColor = rgbColor;
            CorrespondingColor = correspondingColor;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return new List<object> { RgbColor, CorrespondingColor }.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
