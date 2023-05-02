namespace DG.Color
{
    /// <summary>
    /// <inheritdoc cref="BaseColor"/>
    /// <para></para>
    /// Note that the generic parameter <typeparamref name="TColor"/> should be the color class itself.
    /// </summary>
    /// <typeparam name="TColor"></typeparam>
    public abstract class ConvertibleColor<TColor> : BaseColor where TColor : ConvertibleColor<TColor>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConvertibleColor{TColor}"/>, with the given <paramref name="alpha"/> value.
        /// </summary>
        /// <param name="alpha"></param>
        protected ConvertibleColor(float alpha) : base(alpha)
        {
        }

        /// <summary>
        /// Returns a new instance of <typeparamref name="TColor"/>, converted from the given <see cref="RgbValues"/> values.
        /// <para></para>
        /// Note that this method will always be called on an instance created using a parameterless constructor.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        protected abstract TColor CreateFrom(RgbValues values, float alpha);

        internal TColor NewInstanceFrom(RgbValues values, float alpha)
        {
            return CreateFrom(values, alpha);
        }
    }
}