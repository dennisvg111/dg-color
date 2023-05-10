using DG.Color.Colorblindness.Vienot1999;
using DG.Color.Utilities;

namespace DG.Color.Colorblindness
{
    public class Vienot1999Simulator
    {
        private static readonly TransformationMatrix _daltonMatrix = TransformationMatrix
            .WithRow(0.0f, 0.0f, 0.0f)
            .WithRow(0.7f, 1.0f, 0.0f)
            .WithRow(0.7f, 0.0f, 1.0f);

        private readonly int[] _colorMap = new int[256 * 256 * 256];

        private void CalculateColorMap(TransformationMatrix sim, float amount)
        {
            for (int i = 0; i < _colorMap.Length; i++)
            {
                var startValues = RgbValues.FromArgb(i, out _);
                var simulated = Simulate(startValues, sim, amount);
                _colorMap[i] = simulated.ToArgb(1);
            }
        }

        public RgbValues Simulate(RgbValues values, TransformationMatrix sim, float amount)
        {
            var lms = LmsConversion.ConvertRgbToLms(values);
            var transformed = sim.Transform(lms);
            var simulatedValues = LmsConversion.ConvertLmsToRgb(transformed);

            byte finalRed = (byte)(values.Red * (1.0 - amount) + simulatedValues.Red * amount);
            byte finalGreen = (byte)(values.Green * (1.0 - amount) + simulatedValues.Green * amount);
            byte finalBlue = (byte)(values.Blue * (1.0 - amount) + simulatedValues.Blue * amount);

            return new RgbValues(finalRed, finalGreen, finalBlue);
        }

        public RgbValues Daltonize(RgbValues values, TransformationMatrix sim, float amount)
        {
            var linRGB = LmsConversion.ConvertRgbToLinearRgb(values);

            var lms = LmsConversion.ConvertLinearRgbToLms(linRGB);
            var transformed = sim.Transform(lms);
            var simRGB = LmsConversion.ConvertLmsToLinearRgb(transformed);

            ColorVector error = linRGB - simRGB;
            ColorVector correction = _daltonMatrix.Transform(error);
            ColorVector daltonizedLinearRgb = correction + linRGB;

            var daltonizedValues = LmsConversion.ConvertLinearRgbToRgb(daltonizedLinearRgb);

            byte finalRed = (byte)(values.Red * (1.0 - amount) + daltonizedValues.Red * amount);
            byte finalGreen = (byte)(values.Green * (1.0 - amount) + daltonizedValues.Green * amount);
            byte finalBlue = (byte)(values.Blue * (1.0 - amount) + daltonizedValues.Blue * amount);

            return new RgbValues(finalRed, finalGreen, finalBlue);
        }
    }
}
