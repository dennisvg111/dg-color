using DG.Color.Utilities;
using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests.Vienot1999
{
    public class TransformationMatrixTests
    {
        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            // vector3 input, matrix, vector3 expected
            new object[]
            {
                new ColorVector(4, 7.3f, 7),
                TransformationMatrix.Identity,
                new ColorVector(4, 7.3f, 7),
            },
            new object[]
            {
                new ColorVector(1, 1, 1),
                TransformationMatrix
                    .WithRow(0, 0, 0)
                    .WithRow(0, 0, 0)
                    .WithRow(0, 0, 0),
                new ColorVector(0, 0, 0),
            },
            new object[]
            {
                new ColorVector(0, 8, 2),
                TransformationMatrix
                    .WithRow(0, 0, 1)
                    .WithRow(0, 1, 0)
                    .WithRow(1, 0, 0),
                new ColorVector(2, 8, 0),
            },
            new object[]
            {
                new ColorVector(0, 1, 2),
                TransformationMatrix
                    .WithRow(2, 0, 0)
                    .WithRow(0, 2, 0)
                    .WithRow(0, 0, 2),
                new ColorVector(0, 2, 4),
            },
            new object[]
            {
                new ColorVector(5, 1, 2),
                TransformationMatrix
                    .WithRow(0.5f, 0, 0)
                    .WithRow(0, 2, 0)
                    .WithRow(0, 0, 0.1f),
                new ColorVector(2.5f, 2, 0.2f),
            },
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Transform_Works(ColorVector input, TransformationMatrix matrix, ColorVector expected)
        {
            var actual = matrix.Transform(input);

            Assert.Equal(expected.X, actual.X);
            Assert.Equal(expected.Y, actual.Y);
            Assert.Equal(expected.Z, actual.Z);
        }

        [Fact]
        public void Inverse_Works()
        {
            var matrix = TransformationMatrix
                .WithRow(1, 2, 3)
                .WithRow(0, 1, 4)
                .WithRow(5, 6, 0);
            var expectedMatrix = TransformationMatrix
                .WithRow(-24, 18, 5)
                .WithRow(20, -15, -4)
                .WithRow(-5, 4, 1);
            var invertedMatrix = matrix.Inverse();

            var expected = new ColorVector(12, 40, 5);

            var transformed = matrix.Transform(expected);
            var actual = invertedMatrix.Transform(transformed);

            Assert.Equal(expectedMatrix.ToString(), invertedMatrix.ToString());
            Assert.Equal(expected.X, actual.X);
            Assert.Equal(expected.Y, actual.Y);
            Assert.Equal(expected.Z, actual.Z);
        }
    }
}
