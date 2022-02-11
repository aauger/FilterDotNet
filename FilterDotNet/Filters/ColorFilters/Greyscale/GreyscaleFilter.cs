using FilterDotNet.Extensions;
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
                    IColor originalColor = input.GetPixel(x, y);
                    int average = originalColor.Average();

                    IColor greyColor = this._engine.CreateColor(average, average, average, this._engine.MaxValue);

                    output.SetPixel(x, y, greyColor);
                });
            });
            return output;
        }
    }
}
