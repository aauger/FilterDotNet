using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter.ImageProviders
{
    public class GDIDrawingImage : IImage
    {
        private readonly Bitmap _bitmap;

        public int Width => _bitmap.Width;

        public int Height => _bitmap.Height;

        public GDIDrawingImage(int x, int y)
        {
            _bitmap = new Bitmap(x, y);
        }

        private GDIDrawingImage(Bitmap bitmap)
        { 
            _bitmap = bitmap;
        }

        public static IImage Create(int x, int y)
        {
            return new GDIDrawingImage(x, y);
        }

        public IImage From(IImage image)
        {
            IImage ret = new GDIDrawingImage(image.Width, image.Height);
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
            GDIDrawingColor dc = new GDIDrawingColor(c.R, c.G, c.B, c.A);
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

        public static GDIDrawingImage WrapBitmap(Bitmap bitmap)
        {
            return new GDIDrawingImage(bitmap);
        }
    }
}
