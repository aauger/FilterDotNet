using AAImageFilter.Interfaces;

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

        public static int Difference(this IColor c, IColor other)
        { 
            return Math.Abs((c.R - other.R))
                + Math.Abs((c.G - other.G))
                + Math.Abs((c.B - other.B));
        }
    }
}
