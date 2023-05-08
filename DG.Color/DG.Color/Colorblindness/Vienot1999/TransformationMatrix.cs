namespace DG.Color.Colorblindness.Vienot1999
{
    public readonly struct TransformationMatrix
    {
        private readonly Vector3 _row1;
        private readonly Vector3 _row2;
        private readonly Vector3 _row3;

        public TransformationMatrix(Vector3 row1, Vector3 row2, Vector3 row3)
        {
            _row1 = row1;
            _row2 = row2;
            _row3 = row3;
        }

        public TransformationMatrix(float v1, float v2, float v3, float v4, float v5, float v6, float v7, float v8, float v9)
        {
            _row1 = new Vector3(v1, v2, v3);
            _row2 = new Vector3(v4, v5, v6);
            _row3 = new Vector3(v7, v8, v9);
        }

        /// <summary>
        /// Transforms the given <see cref="Vector3"/> using the current matrix.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Vector3 Transform(Vector3 input)
        {
            var v1 = _row1.V1 * input.V1 + _row1.V2 * input.V2 + _row1.V3 * input.V3;
            var v2 = _row2.V1 * input.V1 + _row2.V2 * input.V2 + _row2.V3 * input.V3;
            var v3 = _row3.V1 * input.V1 + _row3.V2 * input.V2 + _row3.V3 * input.V3;

            return new Vector3(v1, v2, v3);
        }

        /// <summary>
        /// Performs right multiplication.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator *(TransformationMatrix left, Vector3 right)
        {
            var v1 = left._row1.V1 * right.V1 + left._row1.V2 * right.V2 + left._row1.V3 * right.V3;
            var v2 = left._row2.V1 * right.V1 + left._row2.V2 * right.V2 + left._row2.V3 * right.V3;
            var v3 = left._row3.V1 * right.V1 + left._row3.V2 * right.V2 + left._row3.V3 * right.V3;

            return new Vector3(v1, v2, v3);
        }

        /// <summary>
        /// Returns a string representation of this <see cref="TransformationMatrix"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{_row1}; {_row2}; {_row3}";
        }
    }
}
