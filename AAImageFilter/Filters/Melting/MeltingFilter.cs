using AAImageFilter.Extensions;
using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Filters
{
    /// <summary>
    /// This filter is inherently serial, so unfortunately no speed improvements to be gained by using Parallel.For(...)
    /// </summary>
    public class MeltingFilter : IFilter
    {
        /* DI */
        private readonly Func<int, int, IImage> _imageCreator;

        /* Properties */
        public string Name => "Melting";

        public MeltingFilter(Func<int, int, IImage> imageCreator)
        {
            this._imageCreator = imageCreator;
        }

        public IImage Apply(IImage input)
        {
            IImage output = input.Clone(this._imageCreator);
            Random rnd = new();
            for (int i = 0; i < output.Width * output.Width; i++)
            {
                int x = rnd.Next(0, output.Width);
                int y = rnd.Next(0, output.Height);
                IColor c = output.GetPixel(x, y);
                for(; y + 1 < output.Height && c.Average() > output.GetPixel(x, y + 1).Average(); y++)
                {
                    output.SetPixel(x, y, output.GetPixel(x, y + 1));
                    output.SetPixel(x, y + 1, c);
                }
            }
            return output;
        }
    }
}
