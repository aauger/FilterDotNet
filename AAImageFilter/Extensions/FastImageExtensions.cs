using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Common;

namespace AAImageFilter.Extensions
{
    public static class FastImageExtensions
    {
        public static bool OutOfBounds(this FastImage f, int x, int y)
        { 
            return x < 0 || x >= f.Width || y < 0 || y >= f.Height;
        }
    }
}
