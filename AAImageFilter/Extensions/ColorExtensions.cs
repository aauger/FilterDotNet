using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Extensions
{
    public static class ColorExtensions
    {
        public static Color Inverse(this Color c)
        { 
            return Color.FromArgb(255-c.R,255-c.G,255-c.B);
        }
    }
}
