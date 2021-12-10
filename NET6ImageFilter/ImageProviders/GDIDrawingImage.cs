using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter.ImageProviders
{
    public class DrawingImage : IImage
    {
        private readonly Bitmap _bitmap;

        public int Width => _bitmap.Width;

        public int Height => _bitmap.Height;

        public DrawingImage(int x, int y)
        {
            _bitmap = new Bitmap(x, y);
        }

        private DrawingImage(Bitmap bitmap)
        { 
            _bitmap = bitmap;
        }

        public static IImage Create(int x, int y)
        {
            return new DrawingImage(x, y);
        }

        public IImage From(IImage image)
        {
            IImage ret = new DrawingImage(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                { 
                    ret.SetPixel(x,y, image.GetPixel(x,y));
                }
            }
            return ret;
        }

        public IColor GetPixel(int x, int y)
        {
            Color c = _bitmap.GetPixel(x,y);
            DrawingColor dc = new DrawingColor(c.R, c.G, c.B, c.A);
            return dc;
        }

        public void SetPixel(int x, int y, IColor color)
        {
            _bitmap.SetPixel(x, y, Color.FromArgb(color.A, color.R, color.G, color.B));
        }

        public Bitmap UnwrapBitmap()
        {
            return _bitmap;
        }

        public static DrawingImage WrapBitmap(Bitmap bitmap)
        {
            return new DrawingImage(bitmap);
        }
    }
}
