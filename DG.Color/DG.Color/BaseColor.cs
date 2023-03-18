using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DG.Color
{
    /// <summary>
    /// Represents a color in the sRGB color space. This color can be converted to any class that implements <see cref="BaseColor"/>. Note that the generic parameter <typeparamref name="TColor"/> should be the child color class itself.
    /// </summary>
    public abstract class BaseColor<TColor> where TColor : BaseColor<TColor>
    {
        private const double _redOpticalWeight = 0.2126;
        private const double _greenOpticalWeight = 0.7152;
        private const double _blueOpticalWeight = 0.0722;

        private RgbaValues _values;
        private RgbaValues Values
        {
            get
            {
                if (_values == null)
                {
                    _values = ConvertToRgba();
                }
                return _values;
            }
        }

        protected abstract RgbaValues ConvertToRgba();


        /// <summary>
        /// This method needs to be implemented to support converting colors with the <see cref="ConvertTo{T}"/> method.
        /// <para></para>
        /// Note that this method needs to return a basecolor of the correct (currently implementing) type, otherwise a <see cref="NotImplementedException"/> will be thrown when converting.
        /// <para></para>
        /// Also note that this method will always be called on an instance created using a parameterless constructor.
        /// </summary>
        /// <param name="rgbaValues"></param>
        /// <returns></returns>
        protected abstract TColor CreateFromRgba(RgbaValues rgbaValues);

        /// <summary>
        /// Converts this color to a color of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ConvertTo<T>() where T : BaseColor<T>
        {
            var color = Uninitialized<T>.Instance;
            var newColor = color.CreateFromRgba(ConvertToRgba());
            if (!(newColor is T))
            {
                throw new NotImplementedException($"Method ${nameof(CreateFromRgba)} is not correctly implemented for type {typeof(T).FullName}.");
            }
            return (T)newColor;
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="BaseColor{TColor}"/> structure.
        /// </summary>
        /// <returns>The 32-bit ARGB value of this <see cref="BaseColor{TColor}"/></returns>
        public int ToArgb()
        {
            return (Values.AlphaByte << 24) | (Values.Red << 16) | (Values.Green << 8) | Values.Blue;
        }

        /// <summary>
        /// Gets the perceived brightness of a color. This can be used to convert color images to grayscale.
        /// </summary>
        /// <returns></returns>
        public byte GetLuminance()
        {
            double luminance = Math.Round(_redOpticalWeight * Values.Red + _greenOpticalWeight * Values.Green + _blueOpticalWeight * Values.Blue);
            if (luminance < 0)
            {
                return 0;
            }
            if (luminance > 255)
            {
                return 255;
            }
            return (byte)luminance;
        }

        #region Child class initialization logic
        private static class Uninitialized<T> where T : BaseColor<T>
        {
            private static readonly T _instance;
            public static T Instance => _instance;
            static Uninitialized()
            {
                _instance = Construct();
            }

            static T Construct()
            {
                Type t = typeof(T);
                if (HasDefaultConstructor(t))
                {
                    var constructor = Expression.Lambda<Func<T>>(Expression.New(t)).Compile();
                    return constructor();
                }

                return (T)FormatterServices.GetUninitializedObject(t);
            }
        }

        private static bool HasDefaultConstructor(Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
        #endregion
    }
}
