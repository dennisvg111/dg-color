﻿namespace DG.Color.Utilities
{
    public readonly struct ColorVector
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;

        /// <summary>
        /// The first componenent of this vector.
        /// </summary>
        public float X => _x;

        /// <summary>
        /// The second component of this vector.
        /// </summary>
        public float Y => _y;

        /// <summary>
        /// The third component of this vector.
        /// </summary>
        public float Z => _z;

        public ColorVector(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        /// Adds the values of the right vector to the values of the left vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ColorVector operator +(ColorVector left, ColorVector right)
        {
            return new ColorVector(left._x + right._x, left._y + right._y, left._z + right._z);
        }

        /// <summary>
        /// Subtracts the values of the right vector from the values of the left vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ColorVector operator -(ColorVector left, ColorVector right)
        {
            return new ColorVector(left._x - right._x, left._y - right._y, left._z - right._z);
        }

        /// <summary>
        /// Returns a string representation of this <see cref="ColorVector"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }
    }
}
