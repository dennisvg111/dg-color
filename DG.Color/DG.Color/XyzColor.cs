using System;

namespace DG.Color
{
    public class XyzColor : ConvertibleColor<XyzColor>
    {
        private readonly double _x;
        private readonly double _y;
        private readonly double _z;
        private readonly float _alpha;

        public XyzColor(double x, double y, double z, float alpha)
        {
            _x = x;
            _y = y;
            _z = z;
            _alpha = alpha;
        }

        protected override RgbaValues ConvertToRgba()
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

            return RgbaValues.Round(r * 255, g * 255, b * 255, _alpha);
        }

        protected override XyzColor CreateFrom(RgbaValues values)
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
