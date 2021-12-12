using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class InvertFilter : IFilter
    {
        /* DI */
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Properties */
        public string Name => throw new NotImplementedException();

        public InvertFilter(Func<int, int, int, int, IColor> colorCreator)
        {
            _colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    IColor here = input.GetPixel(x, y);

                    int r = 255 - here.R;
                    int g = 255 - here.G;
                    int b = 255 - here.B;

                    IColor nColor = _colorCreator(r, g, b, 255);

                    input.SetPixel(x, y, nColor);
                }
            }

            return input;
        }
    }
}
