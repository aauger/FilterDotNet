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
        
        /* Creation */
        IImage Create(int x, int y);
        IImage From(IImage image);

        /* Mutation */
        void SetPixel(int x, int y, IColor color);
        IColor GetPixel(int x, int y);
    }
}
