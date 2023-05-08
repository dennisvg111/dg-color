namespace DG.Color.Colorblindness.Vienot1999
{
    /// <summary>
    /// Provides functionality for converting colors to the LMS (Long, Medium, Short) wavelength color space.
    /// </summary>
    public static class LmsConversion
    {
        private static readonly TransformationMatrix _linearRgbToLmsMatrix = new TransformationMatrix(
            new Vector3(0.31399022f, 0.63951294f, 0.04649755f),
            new Vector3(0.15537241f, 0.75789446f, 0.08670142f),
            new Vector3(0.01775239f, 0.10944209f, 0.87256922f));

        private static readonly TransformationMatrix _lmsToLinearRgbMatrix = new TransformationMatrix(
            new Vector3(5.47221206f, -4.6419601f, 0.16963708f),
            new Vector3(-1.1252419f, 2.29317094f, -0.1678952f),
            new Vector3(0.02980165f, -0.19318073f, 1.16364789f));

        /// <summary>
        /// Converts the given sRGB values to LMS.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Vector3 ConvertRgbToLms(RgbValues values)
        {
            var linearRgb = ConvertRgbToLinearRgb(values);
            return ConvertLinearRgbToLms(linearRgb);
        }

        /// <summary>
        /// Converts the given sRGB values to linear RGB.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Vector3 ConvertRgbToLinearRgb(RgbValues values)
        {
            float linearRed = GammaCorrection.RemoveFromByte(values.Red);
            float linearGreen = GammaCorrection.RemoveFromByte(values.Green);
            float linearBlue = GammaCorrection.RemoveFromByte(values.Blue);

            return new Vector3(linearRed, linearGreen, linearBlue);
        }

        /// <summary>
        /// Converts the given linear RGB values to LMS.
        /// </summary>
        /// <param name="linearRgb"></param>
        /// <returns></returns>
        public static Vector3 ConvertLinearRgbToLms(Vector3 linearRgb)
        {
            return _linearRgbToLmsMatrix.Transform(linearRgb);
        }

        /// <summary>
        /// Converts the given LMS vector3 to sRGB.
        /// </summary>
        /// <param name="lms"></param>
        /// <returns></returns>
        public static RgbValues ConvertLmsToRgb(Vector3 lms)
        {
            var linearRgb = ConvertLmsToLinearRgb(lms);
            return ConvertLinearRgbToRgb(linearRgb);
        }

        /// <summary>
        /// Converts the given LMS vector to linear RGB.
        /// </summary>
        /// <param name="lms"></param>
        /// <returns></returns>
        public static Vector3 ConvertLmsToLinearRgb(Vector3 lms)
        {
            return _lmsToLinearRgbMatrix.Transform(lms);
        }

        /// <summary>
        /// Converts the given linear RGB values to sRGB.
        /// </summary>
        /// <param name="linearRgb"></param>
        /// <returns></returns>
        public static RgbValues ConvertLinearRgbToRgb(Vector3 linearRgb)
        {
            byte red = GammaCorrection.ApplyForIntensity(linearRgb.V1);
            byte green = GammaCorrection.ApplyForIntensity(linearRgb.V2);
            byte blue = GammaCorrection.ApplyForIntensity(linearRgb.V3);

            return new RgbValues(red, green, blue);
        }
    }
}
