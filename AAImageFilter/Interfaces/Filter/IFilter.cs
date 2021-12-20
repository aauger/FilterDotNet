using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Interfaces
{
    public interface IFilter
    {
        string Name { get; }
        IImage Apply(IImage input);
    }
}
