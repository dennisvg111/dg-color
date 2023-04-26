namespace DG.Color
{
    public class RgbColor : ConvertibleColor<RgbColor>
    {
        private readonly RgbaValues _values;

        public byte Red => _values.Red;
        public byte Green => _values.Green;
        public byte Blue => _values.Blue;
        public float Alpha => _values.Alpha;

        public RgbColor(RgbaValues values)
        {
            _values = values;
        }

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

        protected override RgbaValues ConvertToRgba()
        {
            return _values;
        }

        protected override RgbColor CreateFrom(RgbaValues values)
        {
            return new RgbColor(values);
        }
    }
}
