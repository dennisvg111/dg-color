using DG.Color.Colorblindness.Vienot1999;
using DG.Color.Utilities;
using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests.Vienot1999
{
    public class LmsConversionTests
    {
        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            // sRGB, linear RGB, LMS
            new object[] { new RgbValues(0, 130, 123), new ColorVector(0, 0.2232f, 0.1981f), new ColorVector(0.1519818f, 0.1863722f, 0.197297f) },
            new object[] { new RgbValues(255, 255, 255), new ColorVector(1, 1, 1), new ColorVector(1f, 1f, 1f) },
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void ConvertRgbToLms_Works(RgbValues sRgb, ColorVector lRgb, ColorVector expected)
        {
            var actual = LmsConversion.ConvertRgbToLms(sRgb);

            Assert.Equal(expected.X, actual.X, 0.0005);
            Assert.Equal(expected.Y, actual.Y, 0.0005);
            Assert.Equal(expected.Z, actual.Z, 0.0005);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ConvertRgbToLinearRgb_Works(RgbValues sRgb, ColorVector expected, ColorVector lms)
        {
            var actualLRgb = LmsConversion.ConvertRgbToLinearRgb(sRgb);

            Assert.Equal(expected.X, actualLRgb.X, 0.00005);
            Assert.Equal(expected.Y, actualLRgb.Y, 0.00005);
            Assert.Equal(expected.Z, actualLRgb.Z, 0.00005);
        }

        [Theory]
        [InlineData(0, 130, 123)]
        [InlineData(0, 0, 0)]
        [InlineData(255, 255, 255)]
        [InlineData(255, 236, 0)]
        public void ConvertRgbToLms_SameAsXyz(byte red, byte green, byte blue)
        {
            var sRgb = new RgbValues(red, green, blue);
            var expected = GetExpectedLmsUsingVonKries(sRgb);

            var actual = LmsConversion.ConvertRgbToLms(sRgb);

            Assert.Equal(expected.X, actual.X, 0.0005);
            Assert.Equal(expected.Y, actual.Y, 0.0005);
            Assert.Equal(expected.Z, actual.Z, 0.0005);
        }

        private ColorVector GetExpectedLmsUsingVonKries(RgbValues values)
        {
            var xyz = new RgbColor(values, 1).To<XyzColor>();

            //Von Kries transformations.
            //D65 normalised
            var xyz_lms = TransformationMatrix
                .WithRow(0.4002f, 0.7076f, -0.0808f)
                .WithRow(-0.2263f, 1.1653f, 0.0457f)
                .WithRow(0.0000f, 0.0000f, 0.9182f);

            return xyz_lms.Transform(new ColorVector((float)xyz.X, (float)xyz.Y, (float)xyz.Z));
        }
    }
}
