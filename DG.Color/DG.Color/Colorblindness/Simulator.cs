using System;

namespace DG.Color.Colorblindness
{
    public static class Simulator
    {
        private const double GAMMA = 2.2;

        public static BaseColor Simulate(BaseColor color, ColorVisionDeficiency deficiency)
        {
            if (deficiency == ColorVisionDeficiency.Achromatomaly || deficiency == ColorVisionDeficiency.Achromatopsia)
            {
                return SimulateMonochromacy(color, deficiency.IsWeak());
            }

            var shift = SimulationShift.For(deficiency);
            return SimulateShift(color, shift, deficiency.IsWeak());
        }

        public static BaseColor SimulateShift(BaseColor color, SimulationShift line, bool isWeak)
        {
            var c = color.To<XyyColor>();
            var slope = (c.SmallY - line.Y) / (c.X - line.X);
            var yi = c.SmallY - c.X * slope; // slope, and y-intercept (at x=0)

            // Find the change in the x and y dimensions (no Y change)
            var dx = (line.Yi - yi) / (slope - line.M);
            var dSmallY = (slope * dx) + yi;
            var dLargeY = 0;

            // Find the simulated colors XYZ coords
            double newX = dx * c.LargeY / dSmallY;
            double newY = c.LargeY;
            double newZ = (1 - (dx + dSmallY)) * c.LargeY / dSmallY;
            var xyzColor = new XyzColor(newX, newY, newZ, c.Alpha);

            // Calculate difference between sim color and neutral color
            var ngx = 0.312713 * c.LargeY / 0.329016; // find neutral grey using D65 white-point
            var ngz = 0.358271 * c.LargeY / 0.329016;
            var dX = ngx - xyzColor.X;
            var dZ = ngz - xyzColor.Z;

            // find out how much to shift sim color toward neutral to fit in RGB space
            var M = XyzColor.XYZ_RGB_MATRIX;
            var dR = dX * M[0] + dLargeY * M[3] + dZ * M[6];
            var dG = dX * M[1] + dLargeY * M[4] + dZ * M[7];
            var dB = dX * M[2] + dLargeY * M[5] + dZ * M[8];

            // convert d to linear RGB
            var tempR = xyzColor.X * M[0] + xyzColor.Y * M[3] + xyzColor.Z * M[6];
            var tempG = xyzColor.X * M[1] + xyzColor.Y * M[4] + xyzColor.Z * M[7];
            var tempB = xyzColor.X * M[2] + xyzColor.Y * M[5] + xyzColor.Z * M[8];

            var adjustR = ((tempR < 0 ? 0 : 1) - tempR) / dR;
            var adjustG = ((tempG < 0 ? 0 : 1) - tempG) / dG;
            var adjustB = ((tempB < 0 ? 0 : 1) - tempB) / dB;

            adjustR = (adjustR > 1 || adjustR < 0) ? 0 : adjustR;
            adjustG = (adjustG > 1 || adjustG < 0) ? 0 : adjustG;
            adjustB = (adjustB > 1 || adjustB < 0) ? 0 : adjustB;
            var adjust = adjustR > adjustG ? adjustR : adjustG;
            if (adjustB > adjust)
            {
                adjust = adjustB;
            }

            // shift proportionally...
            //tempR = tempR + adjust * dR;
            //tempG = tempG + adjust * dG;
            //tempB = tempB + adjust * dB;
            // apply gamma and clamp simulated color...
            tempR = 255 * (tempR <= 0 ? 0 : tempR >= 1 ? 1 : Math.Pow(tempR, 1 / GAMMA));
            tempG = 255 * (tempG <= 0 ? 0 : tempG >= 1 ? 1 : Math.Pow(tempG, 1 / GAMMA));
            tempB = 255 * (tempB <= 0 ? 0 : tempB >= 1 ? 1 : Math.Pow(tempB, 1 / GAMMA));
            //
            if (isWeak)
            {
                var v = 1.75;
                var n = v + 1;
                var rgb = color.Values;
                tempR = (v * tempR + rgb.Red) / n;
                tempG = (v * tempG + rgb.Green) / n;
                tempB = (v * tempB + rgb.Blue) / n;
            }
            return new RgbColor(RgbValues.Round(tempR, tempG, tempB), color.Alpha);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="isPartial"></param>
        /// <returns></returns>
        public static BaseColor SimulateMonochromacy(BaseColor color, bool isPartial)
        {
            var rgb = color.Values;

            // D65 in sRGB
            var z1 = rgb.Red * 0.212656 + rgb.Green * 0.715158 + rgb.Blue * 0.072186;
            var zAnachromistic = new RgbColor(RgbValues.Round(z1, z1, z1), color.Alpha);
            if (!isPartial)
            {
                return zAnachromistic;
            }
            var v = 1.75;
            var n = v + 1;
            var weakValues = RgbValues.Round(
                (v * zAnachromistic.Red + rgb.Red) / n,
                (v * zAnachromistic.Green + rgb.Green) / n,
                (v * zAnachromistic.Blue + rgb.Blue) / n
            );
            return new RgbColor(weakValues, color.Alpha);
        }
    }
}
