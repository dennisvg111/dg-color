namespace DG.Color
{
    public interface IConvertibleTo<TColor> where TColor : BaseColor
    {
        void Convert(out TColor color);
    }
}
