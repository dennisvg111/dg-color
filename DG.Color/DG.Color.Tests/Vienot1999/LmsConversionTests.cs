using DG.Color.Colorblindness.Vienot1999;
using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests.Vienot1999
{
    public class LmsConversionTests
    {
        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            // sRGB, linear RGB, LMS
            new object[] { new RgbValues(0, 130, 123), new Vector3(0, 0.2232f, 0.1981f), new Vector3(0.1519818f, 0.1863722f, 0.197297f) },
            new object[] { new RgbValues(255, 255, 255), new Vector3(1, 1, 1), new Vector3(1f, 1f, 1f) },
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void ConvertRgbToLms_Works(RgbValues sRgb, Vector3 lRgb, Vector3 expected)
        {
            var actual = LmsConversion.ConvertRgbToLms(sRgb);

            Assert.Equal(expected.V1, actual.V1, 0.0005);
            Assert.Equal(expected.V2, actual.V2, 0.0005);
            Assert.Equal(expected.V3, actual.V3, 0.0005);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ConvertRgbToLinearRgb_Works(RgbValues sRgb, Vector3 expected, Vector3 lms)
        {
            var actualLRgb = LmsConversion.ConvertRgbToLinearRgb(sRgb);

            Assert.Equal(expected.V1, actualLRgb.V1, 0.00005);
            Assert.Equal(expected.V2, actualLRgb.V2, 0.00005);
            Assert.Equal(expected.V3, actualLRgb.V3, 0.00005);
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

            Assert.Equal(expected.V1, actual.V1, 0.0005);
            Assert.Equal(expected.V2, actual.V2, 0.0005);
            Assert.Equal(expected.V3, actual.V3, 0.0005);
        }

        private Vector3 GetExpectedLmsUsingVonKries(RgbValues values)
        {
            var xyz = new RgbColor(values, 1).To<XyzColor>();

            //Von Kries transformations.
            //D65 normalised
            var xyz_lms = new TransformationMatrix(
                new Vector3(0.4002f, 0.7076f, -0.0808f),
                new Vector3(-0.2263f, 1.1653f, 0.0457f),
                new Vector3(0.0000f, 0.0000f, 0.9182f)
            );

            return xyz_lms * new Vector3((float)xyz.X, (float)xyz.Y, (float)xyz.Z);
        }
    }
}
