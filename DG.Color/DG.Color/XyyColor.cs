namespace DG.Color
{
    /// <summary>
    /// A color in the CIE XyY color space.
    /// </summary>
    public class XyyColor : ConvertibleColor<XyyColor>, IConvertibleTo<XyzColor>
    {
        private readonly double _x;
        private readonly double _smallY;
        private readonly double _largeY;

        /// <summary>
        /// Initializes a new instance of <see cref="XyyColor"/> with the given values.
        /// </summary>
        /// <param name="x"><inheritdoc cref="X" path="/summary"/></param>
        /// <param name="smallY"><inheritdoc cref="SmallY" path="/summary"/></param>
        /// <param name="largeY"><inheritdoc cref="LargeY" path="/summary"/></param>
        /// <param name="alpha"><inheritdoc cref="BaseColor.Alpha" path="/summary"/></param>
        public XyyColor(double x, double smallY, double largeY, float alpha) : base(alpha)
        {
            _x = x;
            _smallY = smallY;
            _largeY = largeY;
        }

        /// <summary>
        /// The X value of this color (chrominance values derived from the tristimulus values X, Y and Z in the CIE XYZ color space). This can be a value from 0 through 1.
        /// </summary>
        public double X => _x;

        /// <summary>
        /// The y value of this color (chrominance values derived from the tristimulus values X, Y and Z in the CIE XYZ color space). This can be a value from 0 through 1.
        /// </summary>
        public double SmallY => _smallY;

        /// <summary>
        /// The Y value of this color (luminance). This can be a value from 0 through 1.
        /// </summary>
        public double LargeY => _largeY;

        /// <inheritdoc/>
        protected override RgbValues GetRgbValues()
        {
            Convert(out XyzColor xyz);
            return xyz.Values;
        }

        /// <inheritdoc/>
        protected override XyyColor CreateFrom(RgbValues values, float alpha)
        {
            var xyz = new RgbColor(values, alpha).To<XyzColor>();
            xyz.Convert(out XyyColor color);
            return color;
        }

        /// <inheritdoc/>
        public void Convert(out XyzColor color)
        {
            double newX = (_x * _largeY) / _smallY;
            double newY = _largeY;
            double newZ = ((1 - _x - _smallY) * _largeY) / _smallY;

            color = new XyzColor(newX, newY, newZ, Alpha);
        }
    }
}
