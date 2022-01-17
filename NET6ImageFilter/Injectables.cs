using FilterDotNet.Interfaces;
using NET6ImageFilter.ImageProviders;

namespace NET6ImageFilter
{
    public class FastImageEngine : IEngine
    {
        public Func<int, int, IImage> CreateImage => (int x, int y) => new FIDrawingImage(x,y);
        public Func<int, int, int, int, IColor> CreateColor => (int r, int g, int b, int a) => new FIDrawingColor(r,g,b,a);
        public int MaxValue => 255;
        public int MinValue => 0;
    }

    public static class Injectables
    {
        public static FastImageEngine FiEngine => new FastImageEngine();
    }
}
