using FilterDotNet.Interfaces;

namespace FilterDotNet.Extensions
{
    public static class IColorExtensions
    {
        public static bool Equivalent(this IColor c, IColor other)
        {
            return (c.R == other.R && c.G == other.G && c.B == other.B);
        }

        public static IColor Inverse(this IColor c, IEngine engine)
        {
            return engine.CreateColor(
                    engine.MaxValue - c.R,
                    engine.MaxValue - c.G,
                    engine.MaxValue - c.B,
                    engine.MaxValue
                );
        }

        public static int Difference(this IColor c, IColor other)
        { 
            return Math.Abs((c.R - other.R))
                + Math.Abs((c.G - other.G))
                + Math.Abs((c.B - other.B));
        }

        public static int Average(this IColor c)
        {
            return (c.R + c.G + c.B) / 3;
        }
    }
}
