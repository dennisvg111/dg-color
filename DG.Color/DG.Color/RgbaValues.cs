using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                int multiplyPrime = 486187739;
                // Suitable nullity checks etc, of course :)
                hash = hash * multiplyPrime + _red.GetHashCode();
                hash = hash * multiplyPrime + _green.GetHashCode();
                hash = hash * multiplyPrime + _blue.GetHashCode();
                hash = hash * multiplyPrime + _alpha.GetHashCode();
                return hash;
            }
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
