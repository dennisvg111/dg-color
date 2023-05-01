using System;
using System.Collections.Generic;

namespace DG.Color
{
    /// <summary>
    /// Represents a color in the sRGB color space.
    /// </summary>
    public class HslColor : ConvertibleColor<HslColor>
    {
        private const float _goldenRatio = 0.618033988749895f;

        private readonly float _hue;
        private readonly float _saturation;
        private readonly float _lightness;

        /// <summary>
        /// The hue of this color, in degrees. This can be a value from 0 through 360.
        /// </summary>
        public float Hue => _hue;

        /// <summary>
        /// The saturation of this color. This can be a value from 0 through 100.
        /// </summary>
        public float Saturation => _saturation;

        /// <summary>
        /// The lightness of this color. This can be a value from 0 through 100.
        /// </summary>
        public float Lightness => _lightness;

        /// <summary>
        /// Creates a new instance of <see cref="HslColor"/> with the given values.
        /// </summary>
        /// <param name="hue"><inheritdoc cref="Hue" path="/summary"/></param>
        /// <param name="saturation"><inheritdoc cref="Saturation" path="/summary"/></param>
        /// <param name="lightness"><inheritdoc cref="Lightness" path="/summary"/></param>
        /// <param name="alpha"><inheritdoc cref="BaseColor.Alpha" path="/summary"/></param>
        public HslColor(float hue, float saturation, float lightness, float alpha) : base(alpha)
        {
            while (hue < 0)
            {
                hue = (360 + hue) % 360;
            }
            if (hue >= 360)
            {
                hue %= 360;
            }
            _hue = hue;
            _saturation = saturation;
            _lightness = lightness;
        }

        /// <summary>
        /// Lerps/Interpolates a color between this color and the given <paramref name="endColor"/>. The value <paramref name="t"/> is the interpolation value, and should be between 0 and 1.
        /// </summary>
        /// <param name="endColor"></param>
        /// <param name="t">The interpolation value. This should be between 0 (the current color) and 1 (the end color)</param>
        /// <returns></returns>
        public HslColor Lerp(HslColor endColor, float t)
        {
            if (t > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(t), "Interpolation value cannot be more than 1.");
            }
            if (t < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(t), "Interpolation value cannot be less than 0.");
            }
            ////based on https://www.alanzucconi.com/2016/01/06/colour-interpolation/ and https://stackoverflow.com/a/13489050/5022761
            float h1 = _hue;
            float h2 = endColor._hue;
            var deltaH = h2 - h1;

            float h, s, l, a;
            h = Interpolate(deltaH > 180 ? (h1 + 360) : h1, deltaH < -180 ? (h2 + 360) : h2, t, 1);
            s = Interpolate(_saturation, endColor._saturation, t, 1);
            l = Interpolate(_lightness, endColor._lightness, t, 1);
            a = Interpolate(Alpha, endColor.Alpha, t, 1);
            while (h < 0)
            {
                h = (360 + h) % 360;
            }
            h = h % 360;
            return new HslColor(h, s, l, a);
        }

        private static float Interpolate(float startValue, float endValue, float stepNumber, float lastStepNumber)
        {
            return (endValue - startValue) * stepNumber / lastStepNumber + startValue;
        }

        /// <summary>
        /// Returns a psuedo-random range of distinct colors.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="startingColor"></param>
        /// <returns></returns>
        public static IEnumerable<HslColor> Range(HslColor startingColor, int count)
        {
            var hue = startingColor._hue;
            for (int i = 0; i < count; i++)
            {
                yield return new HslColor(hue, startingColor._saturation, startingColor._lightness, startingColor.Alpha);
                hue += (360 * _goldenRatio);
                hue %= 360;
            }
        }

        /// <inheritdoc/>
        protected override RgbValues ConvertToRgba()
        {
            byte r, g, b;
            float h, s, l;
            h = _hue / 60f;
            s = _saturation / 100f;
            l = _lightness / 100f;
            if (s < 0.00001f)
            {
                r = (byte)Math.Round(l * 255d);
                g = (byte)Math.Round(l * 255d);
                b = (byte)Math.Round(l * 255d);
            }
            else
            {
                double t1, t2;
                double th = h / 6.0d;

                if (l < 0.5d)
                {
                    t2 = l * (1d + s);
                }
                else
                {
                    t2 = (l + s) - (l * s);
                }
                t1 = 2d * l - t2;

                double tr, tg, tb;
                tr = th + (1.0d / 3.0d);
                tg = th;
                tb = th - (1.0d / 3.0d);

                tr = ColorCalc(tr, t1, t2);
                tg = ColorCalc(tg, t1, t2);
                tb = ColorCalc(tb, t1, t2);
                r = (byte)Math.Round(tr * 255d);
                g = (byte)Math.Round(tg * 255d);
                b = (byte)Math.Round(tb * 255d);
            }
            return new RgbValues(r, g, b);
        }

        private static double ColorCalc(double c, double t1, double t2)
        {

            if (c < 0) c += 1d;
            if (c > 1) c -= 1d;
            if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
            if (2.0d * c < 1.0d) return t2;
            if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
            return t1;
        }

        /// <inheritdoc/>
        protected override HslColor CreateFrom(RgbValues values, float alpha)
        {
            float r = (values.Red / 255f);
            float g = (values.Green / 255f);
            float b = (values.Blue / 255f);

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);
            float d = max - min;

            float h = 0;
            float s = 0;
            float l = ((max + min) / 2.0f);

            if (d < -0.00001f || d > 0.00001f)
            {
                if (l < 0.5f)
                {
                    s = (d / (max + min));
                }
                else
                {
                    s = (d / (2.0f - max - min));
                }


                if (r == max)
                {
                    h = (g - b) / d;
                }
                else if (g == max)
                {
                    h = 2f + (b - r) / d;
                }
                else if (b == max)
                {
                    h = 4f + (r - g) / d;
                }
            }
            return new HslColor((h * 60) % 360, (s * 100), (l * 100), alpha);
        }
    }
}
