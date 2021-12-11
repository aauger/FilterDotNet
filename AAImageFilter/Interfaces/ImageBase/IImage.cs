using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Interfaces
{
    public interface IImage
    {
        /* Properties */
        int Width { get; }
        int Height { get; }

        /* Mutation */
        void SetPixel(int x, int y, IColor color);
        IColor GetPixel(int x, int y);
    }
}
