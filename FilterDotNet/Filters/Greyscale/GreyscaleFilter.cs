using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class GreyscaleFilter: IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Greyscale";

        public GreyscaleFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) => {
                Parallel.For(0, input.Height, (int y) => {
                    IColor here = input.GetPixel(x, y);
                    int avg = (here.R + here.G + here.B) / 3;

                    IColor nColor = this._engine.CreateColor(avg, avg, avg, 255);

                    output.SetPixel(x, y, nColor);
                });
            });
            return output;
        }
    }
}
