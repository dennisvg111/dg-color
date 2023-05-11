using System;
using System.Linq;

namespace DG.Color.Utilities
{
    internal class ColorCacher
    {
        private Lazy<RgbValues>[] _colors = new Lazy<RgbValues>[255 * 255 * 255];

        public ColorCacher(Func<RgbValues, RgbValues> colorFunction)
        {
            _colors = Enumerable.Range(0, 255 * 255 * 255).Select(i => new Lazy<RgbValues>(() => GetForColor(i, colorFunction))).ToArray();
        }

        private RgbValues GetForColor(int argb, Func<RgbValues, RgbValues> colorFunction)
        {
            RgbValues original = RgbValues.FromArgb(argb, out _);

            return colorFunction(original);
        }
    }
}
