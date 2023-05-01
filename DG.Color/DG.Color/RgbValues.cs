using DG.Common;
using System;

namespace DG.Color
{
    /// <summary>
    /// Represents the red, green, and blue values of a color in sRGB color space.
    /// </summary>
    public readonly struct RgbValues : IEquatable<RgbValues>
    {
        private const double _redOpticalWeight = 0.2126;
        private const double _greenOpticalWeight = 0.7152;
        private const double _blueOpticalWeight = 0.0722;

        private readonly byte _red;
        private readonly byte _green;
        private readonly byte _blue;

        /// <summary>
        /// Creates a new instance of <see cref="RgbValues"/>.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public RgbValues(byte red, byte green, byte blue)
        {
            _red = red;
            _green = green;
            _blue = blue;
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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is RgbValues values && Equals(values);
        }

        /// <inheritdoc/>
        public bool Equals(RgbValues other)
        {
            return _red == other._red && _green == other._green && _blue == other._blue;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode
                .Of(_red)
                .And(_green)
                .And(_blue);
        }

        /// <summary>
        /// Returns a value indicating if the two colors are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(RgbValues left, RgbValues right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value indicating if the two colors are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(RgbValues left, RgbValues right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="RgbValues"/> structure.
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns>The 32-bit ARGB value of this <see cref="RgbValues"/></returns>
        public int ToArgb(float alpha)
        {
            var alphaByte = (byte)alpha * 255;
            return (alphaByte << 24) | (_red << 16) | (_green << 8) | _blue;
        }

        /// <summary>
        /// Creates a new <see cref="RgbValues"/> instance from the given 32-bit ARGB value.
        /// </summary>
        /// <param name="argb"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static RgbValues FromArgb(int argb, out float alpha)
        {
            byte[] bytes = new byte[4];
            unchecked
            {
                bytes[0] = (byte)(argb >> 24);
                bytes[1] = (byte)(argb >> 16);
                bytes[2] = (byte)(argb >> 8);
                bytes[3] = (byte)(argb);
            }
            alpha = bytes[0] / 255.0f;
            return new RgbValues(bytes[1], bytes[2], bytes[3]);
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
        /// Returns a new instance of <see cref="RgbValues"/> with the Red, Green, and Blue values inverted.
        /// </summary>
        /// <returns></returns>
        public RgbValues Invert()
        {
            return new RgbValues((byte)(byte.MaxValue - Red), (byte)(byte.MaxValue - Green), (byte)(byte.MaxValue - Blue));
        }

        /// <summary>
        /// Returns a new instance of <see cref="RgbValues"/> where the <paramref name="red"/>, <paramref name="green"/>, and <paramref name="blue"/> values are given as <see cref="double"/> instead of <see cref="byte"/>.
        /// <para></para>
        /// Note this function still expects the <paramref name="red"/>, <paramref name="green"/>, and <paramref name="blue"/> values to be between 0 and 255.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public static RgbValues Round(double red, double green, double blue)
        {
            return new RgbValues
            (
                (byte)Math.Round(red, MidpointRounding.AwayFromZero),
                (byte)Math.Round(green, MidpointRounding.AwayFromZero),
                (byte)Math.Round(blue, MidpointRounding.AwayFromZero)
            );
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
            return $"#{r}{g}{b}";
        }

        /// <summary>
        /// Renders the color in rgba format.
        /// </summary>
        /// <returns></returns>
        public string ToRgbaString()
        {
            return $"rgba({_red}, {_green}, {_blue})";
        }

        /// <inheritdoc cref="ToHexString"/>
        public override string ToString()
        {
            return ToHexString();
        }
    }
}
