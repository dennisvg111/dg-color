using DG.Common.InstanceManagement;
using System;

namespace DG.Color
{
    /// <summary>
    /// Represents a color in the sRGB color space. This color can be converted to any class that implements <see cref="ConvertibleColor{TColor}"/>.
    /// </summary>
    public abstract class BaseColor
    {
        private RgbaValues _values;
        private RgbaValues Values
        {
            get
            {
                if (_values == null)
                {
                    _values = ConvertToRgba();
                }
                return _values;
            }
        }

        protected abstract RgbaValues ConvertToRgba();

        /// <summary>
        /// Converts this color to any other color type that inherits from <see cref="ConvertibleColor{TColor}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T To<T>() where T : ConvertibleColor<T>
        {
            var color = Uninitialized<T>.Instance;
            return color.NewInstanceFrom(Values);
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="BaseColor{TColor}"/> structure.
        /// </summary>
        /// <returns>The 32-bit ARGB value of this <see cref="BaseColor{TColor}"/></returns>
        public int ToArgb()
        {
            return Values.ToArgb();
        }

        /// <summary>
        /// Renders the color in hexadecimal format.
        /// </summary>
        /// <returns></returns>
        public string ToHexString()
        {
            string r = BitConverter.ToString(new byte[] { Values.Red });
            string g = BitConverter.ToString(new byte[] { Values.Green });
            string b = BitConverter.ToString(new byte[] { Values.Blue });
            if (Values.Alpha >= 0.99f)
            {
                return $"#{r}{g}{b}";
            }
            string a = BitConverter.ToString(new byte[] { Values.AlphaByte });
            return $"#{r}{g}{b}{a}";
        }
    }
}
