using FilterDotNet.Interfaces;
using NET6ImageFilter.ImageProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter
{
    public class FastImageEngine : IEngine
    {
        public Func<int, int, IImage> CreateImage => (int x, int y) => new FIDrawingImage(x,y);
        public Func<int, int, int, int, IColor> CreateColor => (int r, int g, int b, int a) => new FIDrawingColor(r,g,b,a);
    }

    public static class Injectables
    {
        public static FastImageEngine FiEngine => new FastImageEngine();
        /* fi creators */
        public static IImage FiImageCreator(int x, int y) => new FIDrawingImage(x, y);
        public static IColor FiColorCreator(int r, int g, int b, int a) => new FIDrawingColor(r, g, b, a);
    }


}
