using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class InvertFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Invert";

        public InvertFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) => {
                    IColor here = input.GetPixel(x, y);

                    int r = 255 - here.R;
                    int g = 255 - here.G;
                    int b = 255 - here.B;

                    IColor nColor = this._engine.CreateColor(r, g, b, this._engine.MaxValue);

                    output.SetPixel(x, y, nColor);
                });
            });

            return output;
        }
    }
}
