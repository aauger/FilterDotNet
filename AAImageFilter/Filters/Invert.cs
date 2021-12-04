using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class Invert : IFilter
    {
        public Image Apply(Image input)
        {
            Bitmap i = (Bitmap)input;

            for (int x = 0; x < i.Width; x++)
            {
                for (int y = 0; y < i.Height; y++)
                {
                    Color here = i.GetPixel(x, y);

                    int r = 255 - here.R;
                    int g = 255 - here.G;
                    int b = 255 - here.B;

                    i.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return i;
        }

        public string GetFilterName()
        {
            return "Invert";
        }
    }
}
