using System;

namespace DG.Color.Utilities
{
    public readonly struct TransformationMatrix : ITransformationMatrixWithOneRow, ITransformationMatrixWithTwoRows
    {
        private readonly MatrixRow _row1;
        private readonly MatrixRow _row2;
        private readonly MatrixRow _row3;

        /// <summary>
        /// Returns an instance of a 3x3 identity transformation matrix.
        /// </summary>
        public static TransformationMatrix Identity => _identity.Value;
        private static readonly Lazy<TransformationMatrix> _identity = new Lazy<TransformationMatrix>(() => TransformationMatrix.WithRow(1, 0, 0).WithRow(0, 1, 0).WithRow(0, 0, 1));

        private TransformationMatrix(MatrixRow row1, MatrixRow row2, MatrixRow row3)
        {
            _row1 = row1;
            _row2 = row2;
            _row3 = row3;
        }

        /// <summary>
        /// Transforms the given <see cref="ColorVector"/> using the current matrix.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ColorVector Transform(ColorVector input)
        {
            var v1 = _row1.Transform(input);
            var v2 = _row2.Transform(input);
            var v3 = _row3.Transform(input);

            return new ColorVector(v1, v2, v3);
        }

        /// <summary>
        /// Returns a string representation of this <see cref="TransformationMatrix"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{_row1}; {_row2}; {_row3}";
        }

        public static ITransformationMatrixWithOneRow WithRow(float x, float y, float z)
        {
            return new TransformationMatrix(new MatrixRow(x, y, z), default(MatrixRow), default(MatrixRow));
        }

        ITransformationMatrixWithTwoRows ITransformationMatrixWithOneRow.WithRow(float x, float y, float z)
        {
            return new TransformationMatrix(_row1, new MatrixRow(x, y, z), default(MatrixRow));
        }

        TransformationMatrix ITransformationMatrixWithTwoRows.WithRow(float x, float y, float z)
        {
            return new TransformationMatrix(_row1, _row2, new MatrixRow(x, y, z));
        }
    }

    public interface ITransformationMatrixWithOneRow
    {
        ITransformationMatrixWithTwoRows WithRow(float x, float y, float z);
    }

    public interface ITransformationMatrixWithTwoRows
    {
        TransformationMatrix WithRow(float x, float y, float z);
    }
}
