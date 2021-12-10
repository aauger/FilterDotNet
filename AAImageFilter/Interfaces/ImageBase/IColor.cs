using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Interfaces
{
    public interface IColor
    {
        int R { get; set; }
        int G { get; set; }
        int B { get; set; }
        int A { get; set; }
    }
}
