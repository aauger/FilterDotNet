using AAImageFilter.Interfaces;
using NET6ImageFilter.ImageProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter
{
    public class Injectables
    {
        /* fi creators */
        public static IImage FiImageCreator(int x, int y) => new FIDrawingImage(x, y);
        public static IColor FiColorCreator(int r, int g, int b, int a) => new FIDrawingColor(r, g, b, a);
    }
}
