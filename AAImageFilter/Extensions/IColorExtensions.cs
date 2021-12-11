using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Extensions
{
    public static class IColorExtensions
    {
        public static IColor Inverse(this IColor c, Func<int, int, int, int, IColor> _colorCreator)
        {
            return _colorCreator(
                    255 - c.R,
                    255 - c.G,
                    255 - c.B,
                    255 - c.A
                );
        }
    }
}
