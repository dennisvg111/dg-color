namespace DG.Color
{
    /// <summary>
    /// Represents a color in the sRGB color space.
    /// </summary>
    public class RgbColor : ConvertibleColor<RgbColor>
    {
        private readonly RgbaValues _values;


        /// <summary>
        /// The red byte of this color.
        /// </summary>
        public byte Red => _values.Red;

        /// <summary>
        /// The green byte of this color.
        /// </summary>
        public byte Green => _values.Green;

        /// <summary>
        /// The blue byte of this color.
        /// </summary>
        public byte Blue => _values.Blue;

        /// <summary>
        /// The alpha value, on a scale from 0 (transparent) to 1 (opaque).
        /// </summary>
        public float Alpha => _values.Alpha;

        /// <summary>
        /// Creates a new instance of <see cref="RgbColor"/> with the given values.
        /// </summary>
        /// <param name="values"></param>
        public RgbColor(RgbaValues values)
        {
            _values = values;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RgbColor"/> with the given values.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        public RgbColor(byte red, byte green, byte blue, float alpha = 1) : this(new RgbaValues(red, green, blue, alpha))
        {

        }

        /// <summary>
        /// Returns a new <see cref="RgbColor"/> with the R, G, and B values inverted.
        /// </summary>
        /// <returns></returns>
        public RgbColor Invert()
        {
            return new RgbColor(_values.Invert());
        }

        /// <inheritdoc/>
        protected override RgbaValues ConvertToRgba()
        {
            return _values;
        }

        /// <inheritdoc/>
        protected override RgbColor CreateFrom(RgbaValues values)
        {
            return new RgbColor(values);
        }
    }
}
