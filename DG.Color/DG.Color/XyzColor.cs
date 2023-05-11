using DG.Color.Colorblindness.Vienot1999;
using DG.Color.Utilities;

namespace DG.Color
{
    /// <summary>
    /// A color in the CIE XYZ color space.
    /// </summary>
    public class XyzColor : ConvertibleColor<XyzColor>, IConvertibleTo<XyyColor>
    {
        public static readonly TransformationMatrix RgbToXyzMatrix = TransformationMatrix
                .WithRow(0.4124f, 0.3576f, 0.1805f)
                .WithRow(0.2126f, 0.7152f, 0.0722f)
                .WithRow(0.0193f, 0.1192f, 0.9505f);

        public static readonly TransformationMatrix XyzToRgbMatrix = RgbToXyzMatrix.Inverse();

        private readonly double _x;
        private readonly double _y;
        private readonly double _z;

        /// <summary>
        /// The X value of this color (a mix of response curves chosen to be non-negative). This can be a value from 0 through 1.
        /// </summary>
        public double X => _x;

        /// <summary>
        /// The Y value of this color (luminance). This can be a value from 0 through 1.
        /// </summary>
        public double Y => _y;

        /// <summary>
        /// The Z value of this color (the S cone response). This can be a value from 0 through 1.
        /// </summary>
        public double Z => _z;

        /// <summary>
        /// Initializes a new instance of <see cref="XyzColor"/> with the given values.
        /// </summary>
        /// <param name="x"><inheritdoc cref="X" path="/summary"/></param>
        /// <param name="y"><inheritdoc cref="Y" path="/summary"/></param>
        /// <param name="z"><inheritdoc cref="Z" path="/summary"/></param>
        /// <param name="alpha"><inheritdoc cref="BaseColor.Alpha" path="/summary"/></param>
        public XyzColor(double x, double y, double z, float alpha) : base(alpha)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <inheritdoc/>
        protected override RgbValues GetRgbValues()
        {
            var xyz = new ColorVector((float)_x, (float)_y, (float)_z);
            var rgb = XyzToRgbMatrix.Transform(xyz);
            rgb = GammaCorrection.Apply(rgb);

            return RgbValues.Round(rgb);
        }

        /// <inheritdoc/>
        protected override XyzColor CreateFrom(RgbValues values, float alpha)
        {
            var rgb = new ColorVector(values.Red / 255f, values.Green / 255f, values.Blue / 255f);
            rgb = GammaCorrection.Remove(rgb);
            var xyz = RgbToXyzMatrix.Transform(rgb);

            return new XyzColor(xyz.X, xyz.Y, xyz.Z, alpha);
        }

        /// <summary>
        /// Renders the color in CIE XYZ format.
        /// </summary>
        /// <returns></returns>
        public string ToXyzString()
        {
            return $"CIE XYZ({_x}, {_y}, {_z})";
        }

        /// <inheritdoc/>
        public void Convert(out XyyColor color)
        {
            var n = _x + _y + _z;

            if (n == 0)
            {
                color = new XyyColor(0, 0, _y, Alpha);
                return;
            }
            double x = _x / n;
            double smallY = _y / n;
            double largeY = _y;
            color = new XyyColor(x, smallY, largeY, Alpha);
        }
    }
}
