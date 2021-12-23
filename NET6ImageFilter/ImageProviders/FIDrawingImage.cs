using AAImageFilter.Interfaces;
using AAImageFilter.Utils;
using FastImageLibrary;
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

        public FIDrawingImage(Image a)
        {
            Bitmap bmp = (Bitmap)a;
            _fastImage = new FastImage(bmp.Width, bmp.Height);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                { 
                    _fastImage.SetPixel(x,y, bmp.GetPixel(x,y));
                }
            }
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
