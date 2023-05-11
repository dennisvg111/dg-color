namespace DG.Color
{
    /// <summary>
    /// This interface specifies a way to directly convert from the current type to <typeparamref name="TColor"/>.
    /// </summary>
    /// <typeparam name="TColor"></typeparam>
    public interface IConvertibleTo<TColor> where TColor : BaseColor
    {
        // We use an out parameter so the generic return type can be given when calling this function.

        /// <summary>
        /// Converts the current color directly to <typeparamref name="TColor"/>.
        /// </summary>
        /// <param name="color"></param>
        void Convert(out TColor color);
    }
}
