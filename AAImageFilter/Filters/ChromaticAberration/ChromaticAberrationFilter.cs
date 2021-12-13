using AAImageFilter.Interfaces;
using AAImageFilter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _imageCreator = imageCreator;
            _colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            IImage output = _imageCreator(input.Width, input.Height);
            for (int x = 2; x < input.Width - 2; x++)
            {
                for (int y = 2; y < input.Height - 2; y++)
                {
                    IColor here = input.GetPixel(x, y);
                    IColor upLeft = input.GetPixel(x - 2, y - 2);
                    IColor boRight = input.GetPixel(x + 2, y + 2);

                    int R = MathUtils.RGBClamp((int)(here.R * .25 + upLeft.R * .75));
                    int B = MathUtils.RGBClamp((int)(here.B * .25 + boRight.B * .75));

                    output.SetPixel(x, y, _colorCreator(R, here.G, B, 255));
                }
            }
            return output;
        }
    }
}
