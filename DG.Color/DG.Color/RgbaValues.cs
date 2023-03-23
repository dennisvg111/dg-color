using DG.Common;
using System;

namespace DG.Color
{
    public readonly struct RgbaValues : IEquatable<RgbaValues>
    {
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
    }
}
