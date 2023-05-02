using System;
using System.Collections.Generic;

namespace DG.Color
{
    /// <summary>
    /// A color in the CIE XYZ color space.
    /// </summary>
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

        /// <summary>
        /// The X value of this color (a mix of response curves chosen to be non-negative). This can be a value from 0 through 1.
        /// </summary>
        public double X => _x;

        /// <summary>
        /// The Y value of this color (luminance). This can be a value from 0 through 1.
        /// </summary>
        public double Y => _y;

        /// <summary>
        /// The Z value of this color (the S cone response). This can be a value from 0 through 1.
        /// </summary>
        public double Z => _z;

        /// <summary>
        /// Initializes a new instance of <see cref="XyzColor"/> with the given values.
        /// </summary>
        /// <param name="x"><inheritdoc cref="X" path="/summary"/></param>
        /// <param name="y"><inheritdoc cref="Y" path="/summary"/></param>
        /// <param name="z"><inheritdoc cref="Z" path="/summary"/></param>
        /// <param name="alpha"><inheritdoc cref="BaseColor.Alpha" path="/summary"/></param>
        public XyzColor(double x, double y, double z, float alpha) : base(alpha)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <inheritdoc/>
        protected override RgbValues GetRgbValues()
        {
            double r, g, b;
            r = _x * 3.2406f + _y * -1.5372f + _z * -0.4986f;
            g = _x * -0.9689f + _y * 1.8758f + _z * 0.0415f;
            b = _x * 0.0557f + _y * -0.2040f + _z * 1.0570f;

            if (r > 0.0031308f) r = 1.055f * (float)Math.Pow(r, (1 / 2.4f)) - 0.055f;
            else r = 12.92f * r;
            if (g > 0.0031308f) g = 1.055f * (float)Math.Pow(g, (1 / 2.4f)) - 0.055f;
            else g = 12.92f * g;
            if (b > 0.0031308f) b = 1.055f * (float)Math.Pow(b, (1 / 2.4f)) - 0.055f;
            else b = 12.92f * b;

            return RgbValues.Round(r * 255, g * 255, b * 255);
        }

        /// <inheritdoc/>
        protected override XyzColor CreateFrom(RgbValues values, float alpha)
        {

            double r, g, b;
            r = values.Red / 255d;
            g = values.Green / 255d;
            b = values.Blue / 255d;


            if (r > 0.04045f) r = (float)Math.Pow((r + 0.055) / 1.055, 2.4);
            else r = r / 12.92f;
            if (g > 0.04045) g = (float)Math.Pow((g + 0.055f) / 1.055f, 2.4f);
            else g = g / 12.92f;
            if (b > 0.04045f) b = (float)Math.Pow((b + 0.055f) / 1.055f, 2.4f);
            else b = b / 12.92f;

            double x = (r * 0.4124f + g * 0.3576f + b * 0.1805f);
            double y = (r * 0.2126f + g * 0.7152f + b * 0.0722f);
            double z = (r * 0.0193f + g * 0.1192f + b * 0.9505f);

            return new XyzColor(x, y, z, alpha);
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
