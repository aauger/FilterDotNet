using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class FloydSteinbergDitherFilter : IFilter, IConfigurableFilter
    {
        public string Name => "Floyd-Steinberg Dither";

        public IImage Apply(IImage input)
        {
            throw new NotImplementedException();
        }

        public IFilter Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
