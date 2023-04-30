namespace DG.Color
{
    public class XyyColor : ConvertibleColor<XyyColor>
    {
        private readonly double x, smallY, largeY;
        private readonly float _alpha;


        protected override RgbaValues ConvertToRgba()
        {
            double newX = (x * largeY) / smallY;
            double newY = largeY;
            double newZ = ((1 - x - smallY) * largeY) / smallY;

            var temp = new XyzColor(newX, newY, newZ, _alpha);
            return temp.Values;
        }

        protected override XyyColor CreateFrom(RgbaValues values)
        {
            throw new System.NotImplementedException();
        }
    }
}
