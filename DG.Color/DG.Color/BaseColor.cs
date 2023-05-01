using DG.Common.Exceptions;
using DG.Common.InstanceManagement;
using System;

namespace DG.Color
{
    /// <summary>
    /// Represents a color in the sRGB color space. This color can be converted to any class that implements <see cref="ConvertibleColor{TColor}"/>.
    /// </summary>
    public abstract class BaseColor
    {
        private Lazy<RgbValues> _values;
        internal RgbValues Values
        {
            get
            {
                return _values.Value;
            }
        }

        private readonly float _alpha;

        /// <summary>
        /// The alpha value, on a scale from 0 (transparent) to 1 (opaque).
        /// </summary>
        public float Alpha => _alpha;

        /// <summary>
        /// Initializes a new instance of <see cref="BaseColor"/>, with the given <paramref name="alpha"/> value.
        /// </summary>
        /// <param name="alpha"><inheritdoc cref="Alpha" path="/summary"/></param>
        protected BaseColor(float alpha)
        {
            ThrowIf.Number(alpha, nameof(alpha)).IsNotBetweenInclusive(0, 1);
            _values = new Lazy<RgbValues>(() => ConvertToRgba());
            _alpha = alpha;
        }

        /// <summary>
        /// Convert this color to the matching <see cref="RgbValues"/> values.
        /// </summary>
        /// <returns></returns>
        protected abstract RgbValues ConvertToRgba();

        /// <summary>
        /// Converts this color to any other color type that inherits from <see cref="ConvertibleColor{TColor}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T To<T>() where T : ConvertibleColor<T>
        {
            var color = UnsafeInstanceOf<T>.Shared;
            return color.NewInstanceFrom(Values, _alpha);
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="BaseColor"/> structure.
        /// </summary>
        /// <returns>The 32-bit ARGB value of this <see cref="BaseColor"/></returns>
        public int ToArgb()
        {
            return Values.ToArgb(_alpha);
        }

        /// <inheritdoc cref="RgbValues.ToHexString()"/>
        public string ToHexString()
        {
            return Values.ToHexString();
        }

        /// <inheritdoc cref="RgbValues.ToRgbaString()"/>
        public string ToRgbaString()
        {
            return Values.ToRgbaString();
        }
    }
}
