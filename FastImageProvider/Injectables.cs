using FastImageProvider;
using FilterDotNet.Interfaces;
using FilterDotNet.Utils;

namespace FastImageProvider
{
    public class FastImageEngine : IEngine
    {
        public Func<int, int, IImage> CreateImage => (int x, int y) => new FIDrawingImage(x,y);
        public Func<int, int, int, int, IColor> CreateColor => (int r, int g, int b, int a) => new FIDrawingColor(r,g,b,a);
        public Func<int, int> Clamp => (int i) => MathUtils.Clamp(i, MinValue, MaxValue);
        public int MaxValue => 255;
        public int MinValue => 0;
    }

    public static class Injectables
    {
        public static FastImageEngine FiEngine => new FastImageEngine();
    }
}
