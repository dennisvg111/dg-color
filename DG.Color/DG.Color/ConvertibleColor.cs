namespace DG.Color
{
    /// <summary>
    ///  Note that the generic parameter <typeparamref name="TColor"/> should be the color class itself.
    /// </summary>
    /// <typeparam name="TColor"></typeparam>
    public abstract class ConvertibleColor<TColor> : BaseColor where TColor : ConvertibleColor<TColor>
    {
        /// <summary>
        /// Note that this method will always be called on an instance created using a parameterless constructor.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected abstract TColor CreateFrom(RgbaValues values);

        internal TColor NewInstanceFrom(RgbaValues values)
        {
            return CreateFrom(values);
        }
    }
}