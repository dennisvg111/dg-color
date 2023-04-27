using DG.Common.InstanceManagement;
using System;

namespace DG.Color
{
    /// <summary>
    /// Represents a color in the sRGB color space. This color can be converted to any class that implements <see cref="ConvertibleColor{TColor}"/>.
    /// </summary>
    public abstract class BaseColor
    {
        private Lazy<RgbaValues> _values;
        private RgbaValues Values
        {
            get
            {
                return _values.Value;
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BaseColor"/>.
        /// </summary>
        protected BaseColor()
        {
            _values = new Lazy<RgbaValues>(() => ConvertToRgba());
        }

        /// <summary>
        /// Convert this color to the matching <see cref="RgbaValues"/> values.
        /// </summary>
        /// <returns></returns>
        protected abstract RgbaValues ConvertToRgba();

        /// <summary>
        /// Converts this color to any other color type that inherits from <see cref="ConvertibleColor{TColor}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T To<T>() where T : ConvertibleColor<T>
        {
            var color = UnsafeInstanceOf<T>.Shared;
            return color.NewInstanceFrom(Values);
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="BaseColor"/> structure.
        /// </summary>
        /// <returns>The 32-bit ARGB value of this <see cref="BaseColor"/></returns>
        public int ToArgb()
        {
            return Values.ToArgb();
        }

        /// <inheritdoc cref="RgbaValues.ToHexString()"/>
        public string ToHexString()
        {
            return Values.ToHexString();
        }

        /// <inheritdoc cref="RgbaValues.ToRgbaString()"/>
        public string ToRgbaString()
        {
            return Values.ToRgbaString();
        }
    }
}
