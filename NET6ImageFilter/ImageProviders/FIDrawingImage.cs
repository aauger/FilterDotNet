using AAImageFilter.Common;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter.ImageProviders
{
    public class FIDrawingImage : IImage
    {
        private readonly FastImage _fastImage;

        public int Width => _fastImage.Width;

        public int Height => _fastImage.Height;

        public FIDrawingImage(int width, int height)
        { 
            _fastImage = new FastImage(width, height);
        }

        public FIDrawingImage(FastImage fastImage)
        {
            _fastImage = fastImage;
        }

        public IColor GetPixel(int x, int y)
        {
            FastImageColor c = _fastImage.GetPixel(x, y);
            FIDrawingColor fidc = new(c);
            return fidc;
        }

        public void SetPixel(int x, int y, IColor color)
        {
            _fastImage.SetPixel(x, y,
                new FastImageColor(color.R, color.G, color.B,
                MathUtils.Map(color.A, 0, 255, 0, 1)));
        }

        public FastImage UnwrapFastImage()
        {
            return this._fastImage;
        }
    }
}
