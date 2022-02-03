using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class ReversibleSplittingFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;
        
        /* Properties */
        public string Name => "Reversible Split";

        public ReversibleSplittingFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);
            int x = 0;
            for (int i = 0; i < input.Width; i += 2)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    output.SetPixel(x, y, input.GetPixel(i, y));
                }
                x++;
            }
            for (int i = 1; i < input.Width; i += 2)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    output.SetPixel(x, y, input.GetPixel(i, y));
                }
                x++;
            }
            return output;
        }
    }
}
