namespace DG.Color
{
    /// <summary>
    /// Represents a color in the sRGB color space.
    /// </summary>
    public class RgbColor : ConvertibleColor<RgbColor>
    {
        private readonly RgbValues _values;

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
        /// Creates a new instance of <see cref="RgbColor"/> with the given values.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="alpha"></param>
        public RgbColor(RgbValues values, float alpha) : base(alpha)
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
        public RgbColor(byte red, byte green, byte blue, float alpha = 1) : this(new RgbValues(red, green, blue), alpha)
        {

        }

        /// <summary>
        /// Returns a new <see cref="RgbColor"/> with the R, G, and B values inverted.
        /// </summary>
        /// <returns></returns>
        public RgbColor Invert()
        {
            return new RgbColor(_values.Invert(), Alpha);
        }

        /// <inheritdoc/>
        protected override RgbValues ConvertToRgba()
        {
            return _values;
        }

        /// <inheritdoc/>
        protected override RgbColor CreateFrom(RgbValues values, float alpha)
        {
            return new RgbColor(values, alpha);
        }
    }
}
