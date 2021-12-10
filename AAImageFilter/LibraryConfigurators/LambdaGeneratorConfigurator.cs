using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.LibraryConfigurators
{
    public class LambdaGeneratorConfigurator<T> : IGeneratorConfigurator<T>
    {
        private readonly Func<T> _lambda;

        public LambdaGeneratorConfigurator(Func<T> lambda)
        { 
            this._lambda = lambda;
        }

        public T GetGeneratorConfiguration()
        {
            return _lambda();
        }
    }
}
