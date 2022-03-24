using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class RotateNinetyCounterClockwiseFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Rotate 90° Counter-Clockwise";

        public RotateNinetyCounterClockwiseFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Height, input.Width);

            Parallel.For(0, output.Width, (int x) =>
            {
                Parallel.For(0, output.Height, (int y) =>
                {
                    output.SetPixel(x, y, input.GetPixel(input.Width-y-1, x));
                });
            });

            return output;
        }
    }
}
