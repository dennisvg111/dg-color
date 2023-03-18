using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.Color
{
    public readonly struct RgbaValues : IEquatable<RgbaValues>
    {
        private readonly float _alpha;
        private readonly byte _red;
        private readonly byte _green;
        private readonly byte _blue;

        public RgbaValues(float alpha, byte red, byte green, byte blue)
        {
            _alpha = alpha;
            _red = red;
            _green = green;
            _blue = blue;
        }

        public float Alpha => _alpha;

        public byte Red => _red;
        public byte Green => _green;
        public byte Blue => _blue;

        public override bool Equals(object obj)
        {
            return obj is RgbaValues values && Equals(values);
        }

        public bool Equals(RgbaValues other)
        {
            return _alpha == other._alpha &&
                   _red == other._red &&
                   _green == other._green &&
                   _blue == other._blue;
        }

        public override int GetHashCode()
        {
            int hashCode = 1838352370;
            hashCode = hashCode * -1521134295 + _alpha.GetHashCode();
            hashCode = hashCode * -1521134295 + _red.GetHashCode();
            hashCode = hashCode * -1521134295 + _green.GetHashCode();
            hashCode = hashCode * -1521134295 + _blue.GetHashCode();
            return hashCode;
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
