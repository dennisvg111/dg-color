using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.Color
{
    public class RgbColor : BaseColor<RgbColor>
    {
        protected override RgbaValues ConvertToRgba()
        {
            throw new NotImplementedException();
        }

        protected override RgbColor CreateFromRgba(RgbaValues rgbaValues)
        {
            throw new NotImplementedException();
        }
    }
}
