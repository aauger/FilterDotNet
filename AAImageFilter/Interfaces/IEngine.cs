using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Interfaces
{
    public interface IEngine
    {
        Func<int,int,IImage> CreateImage { get; }
        Func<int, int, int, int, IColor> CreateColor { get; }
    }
}
