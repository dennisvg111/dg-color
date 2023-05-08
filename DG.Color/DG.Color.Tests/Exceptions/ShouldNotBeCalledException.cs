using System;

namespace DG.Color.Tests.Exceptions
{
    public class ShouldNotBeCalledException : Exception
    {
        public ShouldNotBeCalledException() : base("This method should not be called during tests.")
        {
        }
    }
}
