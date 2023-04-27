using DG.Common;
using System;

namespace DG.Color
{
    /// <summary>
    /// Represents the Red, Green, Blue, and Alpha values of a color in sRGB color space.
    /// </summary>
    public readonly struct RgbaValues : IEquatable<RgbaValues>
    {
        private const double _redOpticalWeight = 0.2126;
        private const double _greenOpticalWeight = 0.7152;
        private const double _blueOpticalWeight = 0.0722;

        private readonly byte _red;
        private readonly byte _green;
        private readonly byte _blue;
        private readonly float _alpha;

        /// <summary>
        /// Creates a new instance of <see cref="RgbaValues"/>.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        public RgbaValues(byte red, byte green, byte blue, float alpha = 1)
        {
            _red = red;
            _green = green;
            _blue = blue;
            _alpha = alpha;
        }

        /// <summary>
        /// The red byte of this color.
        /// </summary>
        public byte Red => _red;

        /// <summary>
        /// The green byte of this color.
        /// </summary>
        public byte Green => _green;

        /// <summary>
        /// The blue byte of this color.
        /// </summary>
        public byte Blue => _blue;

        /// <summary>
        /// The alpha value, on a scale from 0 (transparent) to 1 (opaque).
        /// </summary>
        public float Alpha => _alpha;

        internal byte AlphaByte
        {
            get
            {
                if (Alpha >= 1.0)
                {
                    return 255;
                }
                if (Alpha <= 0.0)
                {
                    return 0;
                }
                return (byte)Math.Round(Alpha * 255.0);
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is RgbaValues values && Equals(values);
        }

        /// <inheritdoc/>
        public bool Equals(RgbaValues other)
        {
            return _red == other._red && _green == other._green && _blue == other._blue && _alpha == other._alpha;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode
                .Of(_red)
                .And(_green)
                .And(_blue)
                .And(_alpha);
        }

        /// <summary>
        /// Returns a value indicating if the two colors are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(RgbaValues left, RgbaValues right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value indicating if the two colors are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(RgbaValues left, RgbaValues right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="RgbaValues"/> structure.
        /// </summary>
        /// <returns>The 32-bit ARGB value of this <see cref="RgbaValues"/></returns>
        public int ToArgb()
        {
            return (AlphaByte << 24) | (_red << 16) | (_green << 8) | _blue;
        }

        /// <summary>
        /// Creates a new <see cref="RgbaValues"/> instance from the given 32-bit ARGB value.
        /// </summary>
        /// <param name="argb"></param>
        /// <returns></returns>
        public static RgbaValues FromArgb(int argb)
        {
            byte[] bytes = new byte[4];
            unchecked
            {
                bytes[0] = (byte)(argb >> 24);
                bytes[1] = (byte)(argb >> 16);
                bytes[2] = (byte)(argb >> 8);
                bytes[3] = (byte)(argb);
            }
            return new RgbaValues(bytes[1], bytes[2], bytes[3], bytes[0] / 255.0f);
        }

        /// <summary>
        /// Gets the perceived brightness of a color. This can be used to convert color images to grayscale.
        /// </summary>
        /// <returns></returns>
        public byte GetLuminance()
        {
            double luminance = Math.Round(_redOpticalWeight * _red + _greenOpticalWeight * _green + _blueOpticalWeight * _blue);
            if (luminance < 0)
            {
                return 0;
            }
            if (luminance > 255)
            {
                return 255;
            }
            return (byte)luminance;
        }

        /// <summary>
        /// Returns a new instance of <see cref="RgbaValues"/> with the Red, Green, and Blue values inverted.
        /// </summary>
        /// <returns></returns>
        public RgbaValues Invert()
        {
            return new RgbaValues((byte)(byte.MaxValue - Red), (byte)(byte.MaxValue - Green), (byte)(byte.MaxValue - Blue), Alpha);
        }

        /// <summary>
        /// Renders the color in hexadecimal format.
        /// </summary>
        /// <returns></returns>
        public string ToHexString()
        {

            string r = BitConverter.ToString(new byte[] { _red });
            string g = BitConverter.ToString(new byte[] { _green });
            string b = BitConverter.ToString(new byte[] { _blue });
            if (_alpha >= 0.999f)
            {
                return $"#{r}{g}{b}";
            }
            string a = BitConverter.ToString(new byte[] { AlphaByte });
            return $"#{r}{g}{b}{a}";
        }

        /// <summary>
        /// Renders the color in rgba format.
        /// </summary>
        /// <returns></returns>
        public string ToRgbaString()
        {
            return $"rgba(" + _red + ", " + _green + ", " + _blue + ", " + _alpha.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ")";
        }

        /// <inheritdoc cref="ToHexString"/>
        public override string ToString()
        {
            return ToHexString();
        }
    }
}
