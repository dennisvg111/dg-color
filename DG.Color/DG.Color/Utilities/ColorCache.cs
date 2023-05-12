using System;
using System.Collections.Generic;

namespace DG.Color.Utilities
{
    /// <summary>
    /// Provides a way to cache the results of a color transformation.
    /// </summary>
    public class ColorCache
    {
        private readonly Func<RgbValues, RgbValues> _colorFunction;
        private readonly Dictionary<RgbValues, Lazy<RgbValues>> _transformedColors;

        /// <summary>
        /// Initializes a new instance of <see cref="ColorCache"/> with the given <paramref name="colorFunction"/>.
        /// </summary>
        /// <param name="colorFunction"></param>
        public ColorCache(Func<RgbValues, RgbValues> colorFunction)
        {
            _colorFunction = colorFunction;
            _transformedColors = new Dictionary<RgbValues, Lazy<RgbValues>>(256 * 256 * 256);
        }

        /// <summary>
        /// Applies the color transformation to the given color, or returns a cached result if it exists.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public RgbValues GetResultOrCreate(RgbValues color)
        {
            if (!_transformedColors.ContainsKey(color))
            {
                _transformedColors[color] = new Lazy<RgbValues>(() => _colorFunction(color));
            }
            return _transformedColors[color].Value;
        }
    }
}
