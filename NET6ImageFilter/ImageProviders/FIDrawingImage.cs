using FilterDotNet.Interfaces;
using FilterDotNet.Utils;
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

        public FIDrawingImage(FastImage fastImage, bool clone = false)
        {
            if (!clone)
            { 
                _fastImage = fastImage;
                return;
            }

            _fastImage = new FastImage(fastImage.Width, fastImage.Height);
            Parallel.For(0, fastImage.Width, (int x) => 
            {
                Parallel.For(0, fastImage.Height, (int y) => 
                {
                    _fastImage.SetPixel(x, y, fastImage.GetPixel(x, y));
                });
            });
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
                new FastImageColor(color.R, color.G, color.B, color.A));
        }

        public FastImage UnwrapFastImage()
        {
            return this._fastImage;
        }
    }
}
