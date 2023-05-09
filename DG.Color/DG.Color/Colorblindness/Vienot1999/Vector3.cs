namespace DG.Color.Colorblindness.Vienot1999
{
    public readonly struct Vector3
    {
        private readonly float _v1;
        private readonly float _v2;
        private readonly float _v3;

        /// <summary>
        /// The first componenent of this vector.
        /// </summary>
        public float V1 => _v1;

        /// <summary>
        /// The second component of this vector.
        /// </summary>
        public float V2 => _v2;

        /// <summary>
        /// The third component of this vector.
        /// </summary>
        public float V3 => _v3;

        public Vector3(float v1, float v2, float v3)
        {
            _v1 = v1;
            _v2 = v2;
            _v3 = v3;
        }

        public Vector3(Vector3 v)
        {
            _v1 = v._v1;
            _v2 = v._v2;
            _v3 = v._v3;
        }

        /// <summary>
        /// Adds the values of the right vector to the values of the left vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left._v1 + right._v1, left._v2 + right._v2, left._v3 + right._v3);
        }

        /// <summary>
        /// Subtracts the values of the right vector from the values of the left vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left._v1 - right._v1, left._v2 - right._v2, left._v3 - right._v3);
        }

        /// <summary>
        /// Returns a string representation of this <see cref="Vector3"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{V1} {V2} {V3}";
        }
    }
}
