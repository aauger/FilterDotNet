using AAImageFilter.Common;
using AAImageFilter.Interfaces;
using NET6ImageFilter.ImageProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter
{
    public class Injectables
    {
        /* gdi creators */
        public static IImage gdiImageCreator(int x, int y) => new GDIDrawingImage(x, y);
        public static IColor gdiColorCreator(int r, int g, int b, int a) => new GDIDrawingColor(r, g, b, a);

        /* fast image input & output adaptors */
        public static FastImage fastImageAdaptor(IImage a)
        {
            if (a is FIDrawingImage fid)
            {
                return fid.UnwrapFastImage();
            }

            FIDrawingImage fidn = new FIDrawingImage(a.Width, a.Height);
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    fidn.SetPixel(x, y, a.GetPixel(x, y));
                }
            }
            return fidn.UnwrapFastImage();
        }
        public static IImage fastImageOutdaptor(FastImage fastImage) => new FIDrawingImage(fastImage);
    }
}
