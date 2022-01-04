using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class InvertFilter : IFilter
    {
        /* DI */
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Properties */
        public string Name => "Invert";

        public InvertFilter(Func<int,int,IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        {
            this._imageCreator = imageCreator;
            this._colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._imageCreator(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) => {
                    IColor here = input.GetPixel(x, y);

                    int r = 255 - here.R;
                    int g = 255 - here.G;
                    int b = 255 - here.B;

                    IColor nColor = this._colorCreator(r, g, b, 255);

                    output.SetPixel(x, y, nColor);
                });
            });

            return output;
        }
    }
}
