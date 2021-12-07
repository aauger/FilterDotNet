using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Extensions
{
    public static class ImageExtensions
    {
        public static bool OutOfBounds(this Image i, int x, int y)
        { 
            return x < 0 || x >= i.Width || y < 0 || y >= i.Height;
        }
    }
}
