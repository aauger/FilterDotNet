using FilterDotNet.Interfaces;
using FilterDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Extensions
{
    public static class IEngineExtensions
    {
        public static double ScaleValueToFractional(this IEngine engine, int value)
        {
            return MathUtils.Map(value, engine.MinValue, engine.MaxValue, 0, 1);
        }

        public static int ScaleValueFromFractional(this IEngine engine, double value)
        {
            return (int)MathUtils.Map(value, 0, 1, engine.MinValue, engine.MaxValue);
        }

        public static int ClampedScaledFromFractional(this IEngine engine, double value)
        {
            return engine.Clamp(engine.ScaleValueFromFractional(value));
        }
    }
}
