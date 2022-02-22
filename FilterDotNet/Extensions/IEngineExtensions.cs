using FilterDotNet.Interfaces;
using FilterDotNet.Utils;

namespace FilterDotNet.Extensions
{
    public static class IEngineExtensions
    {
        public static double ScaleValueToFractional(this IEngine engine, int value) => 
            Utilities.Map(value, engine.MinValue, engine.MaxValue, 0, 1);

        public static int ScaleValueFromFractional(this IEngine engine, double value) => 
            (int)Utilities.Map(value, 0, 1, engine.MinValue, engine.MaxValue);

        public static int ClampedScaledFromFractional(this IEngine engine, double value) => 
            engine.Clamp(engine.ScaleValueFromFractional(value));
    }
}
