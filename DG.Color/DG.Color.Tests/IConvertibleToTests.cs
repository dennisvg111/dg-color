using DG.Color.Tests.Exceptions;
using System;
using Xunit;

namespace DG.Color.Tests
{
    public class IConvertibleToTests
    {
        [Fact]
        public void Convert_IsUsed()
        {
            var color = new ConvertibleColor(1);

            color.To<NonImplementedColor>();
        }
        [Fact]
        public void Convert_IsNotUsed()
        {
            var color = new NonImplementedColor(1);

            Action action = () => color.To<ConvertibleColor>();

            Assert.Throws<ShouldNotBeCalledException>(action);
        }

        public class ConvertibleColor : ConvertibleColor<ConvertibleColor>, IConvertibleTo<NonImplementedColor>
        {
            public ConvertibleColor(float alpha) : base(alpha)
            {
            }

            public void Convert(out NonImplementedColor color)
            {
                color = new NonImplementedColor(Alpha);
            }

            protected override ConvertibleColor CreateFrom(RgbValues values, float alpha)
            {
                throw new ShouldNotBeCalledException();
            }

            protected override RgbValues GetRgbValues()
            {
                throw new ShouldNotBeCalledException();
            }
        }

        public class NonImplementedColor : ConvertibleColor<NonImplementedColor>
        {
            public NonImplementedColor(float alpha) : base(alpha)
            {
            }

            protected override NonImplementedColor CreateFrom(RgbValues values, float alpha)
            {
                throw new ShouldNotBeCalledException();
            }

            protected override RgbValues GetRgbValues()
            {
                throw new ShouldNotBeCalledException();
            }
        }
    }
}
