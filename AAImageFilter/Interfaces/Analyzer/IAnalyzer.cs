using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Interfaces
{
    public interface IAnalyzer
    {
        string Name { get; }
        string Analyze(IImage input);
    }
}
