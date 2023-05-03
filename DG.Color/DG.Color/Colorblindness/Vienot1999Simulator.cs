using DG.Color.Colorblindness.Vienot1999;

namespace DG.Color.Colorblindness
{
    public class Vienot1999Simulator
    {
        private static readonly Matrix3 _daltonMatrix = new Matrix3(
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.7f, 1.0f, 0.0f),
            new Vector3(0.7f, 0.0f, 1.0f));

        private readonly int[] _colorMap = new int[256 * 256 * 256];

        private void CalculateColorMap(Matrix3 sim, float amount)
        {
            for (int i = 0; i < _colorMap.Length; i++)
            {
                var startValues = RgbValues.FromArgb(i, out _);
                var simulated = Simulate(startValues, sim, amount);
                _colorMap[i] = simulated.ToArgb(1);
            }
        }

        public RgbValues Simulate(RgbValues values, Matrix3 sim, float amount)
        {
            var lms = LmsConversion.ConvertRgbToLms(values);
            var transformed = sim * lms;
            var simulatedValues = LmsConversion.ConvertLmsToRgb(transformed);

            byte finalRed = (byte)(values.Red * (1.0 - amount) + simulatedValues.Red * amount);
            byte finalGreen = (byte)(values.Green * (1.0 - amount) + simulatedValues.Green * amount);
            byte finalBlue = (byte)(values.Blue * (1.0 - amount) + simulatedValues.Blue * amount);

            return new RgbValues(finalRed, finalGreen, finalBlue);
        }

        public RgbValues Daltonize(RgbValues values, Matrix3 sim, float amount)
        {
            var linRGB = LmsConversion.ConvertRgbToLinearRgb(values);

            var lms = LmsConversion.ConvertLinearRgbToLms(linRGB);
            var transformed = sim * lms;
            var simRGB = LmsConversion.ConvertLmsToLinearRgb(transformed);

            Vector3 error = linRGB - simRGB;
            Vector3 correction = _daltonMatrix * error;
            Vector3 daltonizedLinearRgb = correction + linRGB;

            var daltonizedValues = LmsConversion.ConvertLinearRgbToRgb(daltonizedLinearRgb);

            byte finalRed = (byte)(values.Red * (1.0 - amount) + daltonizedValues.Red * amount);
            byte finalGreen = (byte)(values.Green * (1.0 - amount) + daltonizedValues.Green * amount);
            byte finalBlue = (byte)(values.Blue * (1.0 - amount) + daltonizedValues.Blue * amount);

            return new RgbValues(finalRed, finalGreen, finalBlue);
        }
    }
}
