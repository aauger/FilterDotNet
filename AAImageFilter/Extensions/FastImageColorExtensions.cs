using AAImageFilter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Extensions
{
    public static class FastImageColorExtensions
    {
        public static FastImageColor Inverse(this FastImageColor fic)
        {
            return new FastImageColor(255 - fic.GetR(), 255 - fic.GetG(), 255 - fic.GetB());
        }
    }
}
