using DG.Common;
using System;

namespace DG.Color
{
    public readonly struct RgbaValues : IEquatable<RgbaValues>
    {
        private const double _redOpticalWeight = 0.2126;
        private const double _greenOpticalWeight = 0.7152;
        private const double _blueOpticalWeight = 0.0722;

        private readonly byte _red;
        private readonly byte _green;
        private readonly byte _blue;
        private readonly float _alpha;

        public RgbaValues(byte red, byte green, byte blue, float alpha = 1)
        {
            _red = red;
            _green = green;
            _blue = blue;
            _alpha = alpha;
        }

        public byte Red => _red;
        public byte Green => _green;
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

        public override bool Equals(object obj)
        {
            return obj is RgbaValues values && Equals(values);
        }

        public bool Equals(RgbaValues other)
        {
            return _red == other._red && _green == other._green && _blue == other._blue && _alpha == other._alpha;
        }

        public override int GetHashCode()
        {
            return HashCode
                .Of(_red)
                .And(_green)
                .And(_blue)
                .And(_alpha);
        }

        public static bool operator ==(RgbaValues left, RgbaValues right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RgbaValues left, RgbaValues right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="BaseColor{TColor}"/> structure.
        /// </summary>
        /// <returns>The 32-bit ARGB value of this <see cref="BaseColor{TColor}"/></returns>
        public int ToArgb()
        {
            return (AlphaByte << 24) | (_red << 16) | (_green << 8) | _blue;
        }

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
        /// Returns a new instance of <see cref="RgbaValues"/> with the R, G, and B values inverted.
        /// </summary>
        /// <returns></returns>
        public RgbaValues Invert()
        {
            return new RgbaValues((byte)(byte.MaxValue - Red), (byte)(byte.MaxValue - Green), (byte)(byte.MaxValue - Blue), Alpha);
        }
    }
}
