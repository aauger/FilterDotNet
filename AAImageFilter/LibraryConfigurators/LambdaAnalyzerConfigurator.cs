using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.LibraryConfigurators
{
    public class LambdaAnalyzerConfigurator<T> : IAnalyzerConfigurator<T>
    {
        private readonly Func<T> _lambda;

        public LambdaAnalyzerConfigurator(Func<T> lambda)
        { 
            this._lambda = lambda;
        }

        public T GetAnalyzerConfiguration()
        {
            return _lambda();
        }
    }
}
