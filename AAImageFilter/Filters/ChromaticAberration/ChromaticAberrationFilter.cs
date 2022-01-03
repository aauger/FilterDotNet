using AAImageFilter.Interfaces;
using AAImageFilter.Utils;

namespace AAImageFilter.Filters
{
    public class ChromaticAberrationFilter : IFilter
    {
        /* DI */
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Properties */
        public string Name => "Chromatic Abberation";

        public ChromaticAberrationFilter(Func<int, int, IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        {
            this._imageCreator = imageCreator;
            this._colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._imageCreator(input.Width, input.Height);
            Parallel.For(2, input.Width - 2, x => {
                Parallel.For(2, input.Height - 2, y => {
                    IColor here = input.GetPixel(x, y);
                    IColor upLeft = input.GetPixel(x - 2, y - 2);
                    IColor boRight = input.GetPixel(x + 2, y + 2);

                    int R = MathUtils.RGBClamp((int)(here.R * .25 + upLeft.R * .75));
                    int B = MathUtils.RGBClamp((int)(here.B * .25 + boRight.B * .75));

                    output.SetPixel(x, y, this._colorCreator(R, here.G, B, 255));
                });
            });
            return output;
        }
    }
}
