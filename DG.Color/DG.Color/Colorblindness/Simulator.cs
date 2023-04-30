namespace DG.Color.Colorblindness
{
    public static class Simulator
    {
        public static BaseColor Simulate(BaseColor color, ColorVisionDeficiency deficiency)
        {
            if (deficiency == ColorVisionDeficiency.Achromatomaly || deficiency == ColorVisionDeficiency.Achromatopsia)
            {
                return SimulateMonochromacy(color, deficiency.IsWeak());
            }

            var shift = SimulationShift.For(deficiency);
            return SimulateShift(color, shift);
        }

        public static BaseColor SimulateShift(BaseColor color, SimulationShift shift)
        {
            var rgb = color.Values;
        }

        public static BaseColor SimulateMonochromacy(BaseColor color, bool isWeak)
        {
            var rgb = color.Values;

            // D65 in sRGB
            var z1 = rgb.Red * 0.212656 + rgb.Green * 0.715158 + rgb.Blue * 0.072186;
            var zAnachromistic = new RgbColor(RgbaValues.Round(z1, z1, z1, rgb.Alpha));
            if (!isWeak)
            {
                return zAnachromistic;
            }
            var v = 1.75;
            var n = v + 1;
            var weakValues = RgbaValues.Round(
                (v * zAnachromistic.Red + rgb.Red) / n,
                (v * zAnachromistic.Green + rgb.Green) / n,
                (v * zAnachromistic.Blue + rgb.Blue) / n,
                zAnachromistic.Alpha
            );
            return new RgbColor(weakValues);
        }
    }
}
