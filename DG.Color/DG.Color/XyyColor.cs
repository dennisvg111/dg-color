namespace DG.Color
{
    public class XyyColor : ConvertibleColor<XyyColor>
    {
        private readonly double _x;
        private readonly double _smallY;
        private readonly double _largeY;

        public XyyColor(double x, double smallY, double largeY, float alpha) : base(alpha)
        {
            _x = x;
            _smallY = smallY;
            _largeY = largeY;
        }

        public double X => _x;

        public double SmallY => _smallY;

        public double LargeY => _largeY;

        /// <inheritdoc/>
        protected override RgbValues ConvertToRgba()
        {
            double newX = (_x * _largeY) / _smallY;
            double newY = _largeY;
            double newZ = ((1 - _x - _smallY) * _largeY) / _smallY;

            var temp = new XyzColor(newX, newY, newZ, Alpha);
            return temp.Values;
        }

        /// <inheritdoc/>
        protected override XyyColor CreateFrom(RgbValues values, float alpha)
        {
            var o = new RgbColor(values, alpha).To<XyzColor>();
            var n = o.X + o.Y + o.Z;

            if (n == 0)
            {
                return new XyyColor(0, 0, o.Y, alpha);
            }
            double x = o.X / n;
            double smallY = o.Y / n;
            double largeY = o.Y;
            return new XyyColor(x, smallY, largeY, alpha);
        }
    }
}
