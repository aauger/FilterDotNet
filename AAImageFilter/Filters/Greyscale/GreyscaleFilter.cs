using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class GreyscaleFilter: IFilter
    {
        /* DI */
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Properties */
        public string Name => "Greyscale";

        public GreyscaleFilter(Func<int, int, int, int, IColor> colorCreator)
        {
            this._colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            Parallel.For(0, input.Width, (int x) => {
                Parallel.For(0, input.Height, (int y) => {
                    IColor here = input.GetPixel(x, y);
                    int avg = (here.R + here.G + here.B) / 3;

                    IColor nColor = this._colorCreator(avg, avg, avg, 255);

                    input.SetPixel(x, y, nColor);
                });
            });
            return input;
        }
    }
}
