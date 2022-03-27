using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class RotateChannelsFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Rotate Channels";

        public RotateChannelsFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) => 
                {
                    IColor original = input.GetPixel(x, y);
                    IColor transformed = this._engine.CreateColor(original.G, original.B, original.R, original.A);
                    output.SetPixel(x, y, transformed);
                });
            });

            return output;
        }
    }
}
