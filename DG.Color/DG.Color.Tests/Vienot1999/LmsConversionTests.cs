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
            new object[] { new RgbValues(0, 130, 123), new Vector3(0, 0.2232f, 0.1981f), new Vector3(0.1063f, 0.0690f, 0.0034f) },
            //new object[] { new RgbValues(255, 255, 255), new Vector3(1, 1, 1), new Vector3(0.9414f, 1.0404f, 1.0895f) },
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void ConvertRgbToLms_Works(RgbValues sRgb, Vector3 lRgb, Vector3 expected)
        {
            var actual = LmsConversion.ConvertRgbToLms(sRgb);

            Assert.Equal(expected.V1, actual.V1, 0.05);
            Assert.Equal(expected.V2, actual.V2, 0.05);
            Assert.Equal(expected.V3, actual.V3, 0.05);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ConvertRgbToLinearRgb_Works(RgbValues sRgb, Vector3 expected, Vector3 lms)
        {
            var actualLRgb = LmsConversion.ConvertRgbToLinearRgb(sRgb);

            Assert.Equal(expected.V1, actualLRgb.V1, 0.00005);
            Assert.Equal(expected.V2, actualLRgb.V2, 0.00005);
            Assert.Equal(expected.V3, actualLRgb.V3, 0.00005);

            var xyz = new RgbColor(sRgb, 1).To<XyzColor>();

            var xyz_lms = new Matrix3(
                0.4002f, 0.7076f, -0.0808f,
                -0.2263f, 1.1653f, 0.0457f,
                0, 0, 0.982f);
            var actualLms = xyz_lms * new Vector3((float)xyz.X, (float)xyz.Y, (float)xyz.Z);
            var wiki = WikiMapping(xyz);
        }

        private static Vector3 WikiMapping(XyzColor xyz)
        {
            var l = 0.7328 * xyz.X + 0.4296 * xyz.Y - 0.1624 * xyz.Z;
            var m = -0.7036 * xyz.X + 1.6975 * xyz.Y + 0.0061 * xyz.Z;
            var s = 0.0030 * xyz.X + 0.0136 * xyz.Y + 0.9834 * xyz.Z;
            return new Vector3((float)l, (float)m, (float)s);
        }
    }
}
