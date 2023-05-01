namespace DG.Color
{
    public class XyyColor : ConvertibleColor<XyyColor>
    {
        private readonly double _x;
        private readonly double _smallY;
        private readonly double _largeY;
        private readonly float _alpha;

        public XyyColor(double x, double smallY, double largeY, float alpha)
        {
            _x = x;
            _smallY = smallY;
            _largeY = largeY;
            _alpha = alpha;
        }

        public double X => _x;

        public double SmallY => _smallY;

        public double LargeY => _largeY;

        public float Alpha => _alpha;

        /// <inheritdoc/>
        protected override RgbaValues ConvertToRgba()
        {
            double newX = (_x * _largeY) / _smallY;
            double newY = _largeY;
            double newZ = ((1 - _x - _smallY) * _largeY) / _smallY;

            var temp = new XyzColor(newX, newY, newZ, _alpha);
            return temp.Values;
        }

        /// <inheritdoc/>
        protected override XyyColor CreateFrom(RgbaValues values)
        {
            var o = new RgbColor(values).To<XyzColor>();
            var n = o.X + o.Y + o.Z;

            if (n == 0)
            {
                return new XyyColor(0, 0, o.Y, values.Alpha);
            }
            double x = o.X / n;
            double smallY = o.Y / n;
            double largeY = o.Y;
            return new XyyColor(x, smallY, largeY, values.Alpha);
        }
    }
}
