namespace DG.Color.Colorblindness
{
    /// <summary>
    /// Represents a type of colorblindness, or color vison deficiency.
    /// </summary>
    public enum ColorVisionDeficiency
    {
        /// <summary>
        /// Red-weak.
        /// </summary>
        Protanomaly = 1,

        /// <summary>
        /// Red-blind.
        /// </summary>
        Protanopia = 2,

        /// <summary>
        /// Green-weak.
        /// </summary>
        Deuteranomaly = 3,

        /// <summary>
        /// Green-blind.
        /// </summary>
        Deuteranopia = 4,

        /// <summary>
        /// Blue-weak.
        /// </summary>
        Tritanomaly = 5,

        /// <summary>
        /// Blue-blind.
        /// </summary>
        Tritanopia = 6,

        /// <summary>
        /// Partial monochromacy.
        /// </summary>
        Achromatomaly = 7,

        /// <summary>
        /// Complete monochromacy.
        /// </summary>
        Achromatopsia = 8
    }

    /// <summary>
    /// This class provides extension methods for the <see cref="ColorVisionDeficiency"/> enum.
    /// </summary>
    public static class ColorVisionDeficiencyExtensions
    {
        /// <summary>
        /// Returns a value indicating if this type of <see cref="ColorVisionDeficiency"/> is weak (for example green-weak) or not (for example green-blind).
        /// </summary>
        /// <param name="deficiency"></param>
        /// <returns></returns>
        public static bool IsWeak(this ColorVisionDeficiency deficiency)
        {
            return (int)deficiency % 2 == 1;
        }
    }
}
