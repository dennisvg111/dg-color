using DG.Color.Colorblindness;
using System.Collections.Generic;
using Xunit;

namespace DG.Color.Tests
{
    public class SimulatorTests
    {
        public static IEnumerable<object[]> TestData => new List<object[]>()
        {
            new object[] { new RgbValues(0, 0, 0), new RgbValues(0, 0, 0), ColorVisionDeficiency.Tritanomaly },
            new object[] { new RgbValues(200, 100, 50), new RgbValues(160, 121, 47), ColorVisionDeficiency.Deuteranopia },
            new object[] { new RgbValues(200, 100, 50), new RgbValues(175, 113, 48), ColorVisionDeficiency.Deuteranomaly },

            new object[] { new RgbValues(66, 222, 173), new RgbValues(157, 206, 165), ColorVisionDeficiency.Protanomaly },
            new object[] { new RgbValues(66, 222, 173), new RgbValues(209, 196, 160), ColorVisionDeficiency.Protanopia },
            new object[] { new RgbValues(66, 222, 173), new RgbValues(165, 201, 179), ColorVisionDeficiency.Deuteranomaly },
            new object[] { new RgbValues(66, 222, 173), new RgbValues(222, 190, 182), ColorVisionDeficiency.Deuteranopia },
            new object[] { new RgbValues(66, 222, 173), new RgbValues(86, 216, 209), ColorVisionDeficiency.Tritanomaly },
            new object[] { new RgbValues(66, 222, 173), new RgbValues(98, 213, 230), ColorVisionDeficiency.Tritanopia },
            new object[] { new RgbValues(66, 222, 173), new RgbValues(142, 198, 181), ColorVisionDeficiency.Achromatomaly },
            new object[] { new RgbValues(66, 222, 173), new RgbValues(185, 185, 185), ColorVisionDeficiency.Achromatopsia },
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestSimulation(RgbValues original, RgbValues expected, ColorVisionDeficiency type)
        {
            var originalColor = new RgbColor(original, 1);

            var simulated = Simulator.Simulate(originalColor, type);
            var actual = simulated.To<RgbColor>();

            Assert.Equal(expected.Red, actual.Red);
            Assert.Equal(expected.Green, actual.Green);
            Assert.Equal(expected.Blue, actual.Blue);
            Assert.Equal(originalColor.Alpha, actual.Alpha);
        }
    }
}
