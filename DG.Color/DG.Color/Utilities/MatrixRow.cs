namespace DG.Color.Utilities
{
    internal readonly struct MatrixRow
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;

        public MatrixRow(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        internal float Transform(ColorVector input)
        {
            return _x * input.X + _y * input.Y + _z * input.Z;
        }

        /// <summary>
        /// Returns a string representation of this <see cref="MatrixRow"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{_x} {_y} {_z}";
        }
    }
}
