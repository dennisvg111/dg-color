using System;
using System.Collections.Generic;

namespace DG.Color
{
    public class XyzColor : ConvertibleColor<XyzColor>
    {
        /// <summary>
        /// A matrix used for converting RGB colors to XYZ.
        /// </summary>
        public static IReadOnlyList<double> RGB_XYZ_MATRIX { get { return new double[] { 0.41242371206635076, 0.21265606784927693, 0.019331987577444885, 0.3575793401363035, 0.715157818248362, 0.11919267420354762, 0.1804662232369621, 0.0721864539171564, 0.9504491124870351 }; } }

        /// <summary>
        /// A matrix used for converting XYZ colors to RGB
        /// </summary>
        public static IReadOnlyList<double> XYZ_RGB_MATRIX { get { return inverse(RGB_XYZ_MATRIX); } }

        private static double determinant(IReadOnlyList<double> m)
        {
            return m[0] * (m[4] * m[8] - m[5] * m[7]) -
                   m[1] * (m[3] * m[8] - m[5] * m[6]) +
                   m[2] * (m[3] * m[7] - m[4] * m[6]);
        }

        private static double[] inverse(IReadOnlyList<double> m)
        {
            var d = 1.0 / determinant(m);
            return new double[] {
                d * (m[4] * m[8] - m[5] * m[7]), // 1-3
                d * (-1 * (m[1] * m[8] - m[2] * m[7])),
                d * (m[1] * m[5] - m[2] * m[4]),
                d * (-1 * (m[3] * m[8] - m[5] * m[6])), // 4-6
                d * (m[0] * m[8] - m[2] * m[6]),
                d * (-1 * (m[0] * m[5] - m[2] * m[3])),
                d * (m[3] * m[7] - m[4] * m[6]), // 7-9
                d * (-1 * (m[0] * m[7] - m[1] * m[6])),
                d * (m[0] * m[4] - m[1] * m[3])
            };
        }

        private readonly double _x;
        private readonly double _y;
        private readonly double _z;
        private readonly float _alpha;

        public double X => _x;

        public double Y => _y;

        public double Z => _z;

        public XyzColor(double x, double y, double z, float alpha)
        {
            _x = x;
            _y = y;
            _z = z;
            _alpha = alpha;
        }

        /// <inheritdoc/>
        protected override RgbaValues ConvertToRgba()
        {
            double r, g, b;
            r = _x * 3.2406f + _y * -1.5372f + _z * -0.4986f;
            g = _x * -0.9689f + _y * 1.8758f + _z * 0.0415f;
            b = _x * 0.0557f + _y * -0.2040f + _z * 1.0570f;

            r = r > 0.0031308f ? 1.055f * Math.Pow(r, (1 / 2.4f)) - 0.055f : 12.92f * r;
            g = g > 0.0031308f ? 1.055f * Math.Pow(g, (1 / 2.4f)) - 0.055f : 12.92f * g;
            b = b > 0.0031308f ? 1.055f * Math.Pow(b, (1 / 2.4f)) - 0.055f : 12.92f * b;

            return RgbaValues.Round(r * 255, g * 255, b * 255, _alpha);
        }

        /// <inheritdoc/>
        protected override XyzColor CreateFrom(RgbaValues values)
        {

            double r, g, b;
            r = values.Red / 255d;
            g = values.Green / 255d;
            b = values.Blue / 255d;

            r = r > 0.04045f ? Math.Pow((r + 0.055) / 1.055, 2.4) : r / 12.92f;
            g = g > 0.04045 ? Math.Pow((g + 0.055f) / 1.055f, 2.4f) : g / 12.92f;
            b = b > 0.04045f ? Math.Pow((b + 0.055f) / 1.055f, 2.4f) : b / 12.92f;

            double x = (r * 0.4124f + g * 0.3576f + b * 0.1805f);
            double y = (r * 0.2126f + g * 0.7152f + b * 0.0722f);
            double z = (r * 0.0193f + g * 0.1192f + b * 0.9505f);

            return new XyzColor(x, y, z, values.Alpha);
        }

        /// <summary>
        /// Renders the color in CIE XYZ format.
        /// </summary>
        /// <returns></returns>
        public string ToXyzString()
        {
            return $"CIE XYZ({_x}, {_y}, {_z})";
        }
    }
}
