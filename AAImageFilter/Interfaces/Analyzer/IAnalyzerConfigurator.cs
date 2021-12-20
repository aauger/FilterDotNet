using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Interfaces
{
    public interface IAnalyzerConfigurator<T>
    {
        T GetAnalyzerConfiguration();
    }
}
