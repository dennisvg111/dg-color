using DG.Color.Utilities;

namespace DG.Color.Colorblindness.Vienot1999
{
    /// <summary>
    /// Provides functionality for converting colors to the LMS (Long, Medium, Short) wavelength color space.
    /// </summary>
    public static class LmsConversion
    {
        private static readonly TransformationMatrix _linearRgbToLmsMatrix = TransformationMatrix
            .WithRow(0.31399022f, 0.63951294f, 0.04649755f)
            .WithRow(0.15537241f, 0.75789446f, 0.08670142f)
            .WithRow(0.01775239f, 0.10944209f, 0.87256922f);

        private static readonly TransformationMatrix _lmsToLinearRgbMatrix = TransformationMatrix
            .WithRow(5.47221206f, -4.6419601f, 0.16963708f)
            .WithRow(-1.1252419f, 2.29317094f, -0.1678952f)
            .WithRow(0.02980165f, -0.19318073f, 1.16364789f);

        /// <summary>
        /// Converts the given sRGB values to LMS.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static ColorVector ConvertRgbToLms(RgbValues values)
        {
            var linearRgb = ConvertRgbToLinearRgb(values);
            return ConvertLinearRgbToLms(linearRgb);
        }

        /// <summary>
        /// Converts the given sRGB values to linear RGB.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static ColorVector ConvertRgbToLinearRgb(RgbValues values)
        {
            float linearRed = GammaCorrection.RemoveFromByte(values.Red);
            float linearGreen = GammaCorrection.RemoveFromByte(values.Green);
            float linearBlue = GammaCorrection.RemoveFromByte(values.Blue);

            return new ColorVector(linearRed, linearGreen, linearBlue);
        }

        /// <summary>
        /// Converts the given linear RGB values to LMS.
        /// </summary>
        /// <param name="linearRgb"></param>
        /// <returns></returns>
        public static ColorVector ConvertLinearRgbToLms(ColorVector linearRgb)
        {
            return _linearRgbToLmsMatrix.Transform(linearRgb);
        }

        /// <summary>
        /// Converts the given LMS vector3 to sRGB.
        /// </summary>
        /// <param name="lms"></param>
        /// <returns></returns>
        public static RgbValues ConvertLmsToRgb(ColorVector lms)
        {
            var linearRgb = ConvertLmsToLinearRgb(lms);
            return ConvertLinearRgbToRgb(linearRgb);
        }

        /// <summary>
        /// Converts the given LMS vector to linear RGB.
        /// </summary>
        /// <param name="lms"></param>
        /// <returns></returns>
        public static ColorVector ConvertLmsToLinearRgb(ColorVector lms)
        {
            return _lmsToLinearRgbMatrix.Transform(lms);
        }

        /// <summary>
        /// Converts the given linear RGB values to sRGB.
        /// </summary>
        /// <param name="linearRgb"></param>
        /// <returns></returns>
        public static RgbValues ConvertLinearRgbToRgb(ColorVector linearRgb)
        {
            byte red = GammaCorrection.ApplyForIntensity(linearRgb.X);
            byte green = GammaCorrection.ApplyForIntensity(linearRgb.Y);
            byte blue = GammaCorrection.ApplyForIntensity(linearRgb.Z);

            return new RgbValues(red, green, blue);
        }
    }
}
