using DG.Color.Colorblindness.Vienot1999;
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
                new Vector3(4, 7.3f, 7),
                new TransformationMatrix(
                    new Vector3(1, 0, 0),
                    new Vector3(0, 1, 0),
                    new Vector3(0, 0, 1)),
                new Vector3(4, 7.3f, 7),
            },
            new object[]
            {
                new Vector3(1, 1, 1),
                new TransformationMatrix(
                    new Vector3(0, 0, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(0, 0, 0)),
                new Vector3(0, 0, 0),
            },
            new object[]
            {
                new Vector3(0, 8, 2),
                new TransformationMatrix(
                    new Vector3(0, 0, 1),
                    new Vector3(0, 1, 0),
                    new Vector3(1, 0, 0)),
                new Vector3(2, 8, 0),
            },
            new object[]
            {
                new Vector3(0, 1, 2),
                new TransformationMatrix(
                    new Vector3(2, 0, 0),
                    new Vector3(0, 2, 0),
                    new Vector3(0, 0, 2)),
                new Vector3(0, 2, 4),
            },
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Transform_Works(Vector3 input, TransformationMatrix matrix, Vector3 expected)
        {
            var actual = matrix.Transform(input);

            Assert.Equal(expected.V1, actual.V1);
            Assert.Equal(expected.V2, actual.V2);
            Assert.Equal(expected.V3, actual.V3);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Multiplication_Works(Vector3 input, TransformationMatrix matrix, Vector3 expected)
        {
            var actual = matrix * input;

            Assert.Equal(expected.V1, actual.V1);
            Assert.Equal(expected.V2, actual.V2);
            Assert.Equal(expected.V3, actual.V3);
        }
    }
}
