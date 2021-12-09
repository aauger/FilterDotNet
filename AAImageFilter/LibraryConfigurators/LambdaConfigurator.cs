using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Interfaces;

namespace AAImageFilter.LibraryConfigurators
{
    public class LambdaConfigurator<T> : IPluginConfigurator<T>
    {
        private readonly Func<T> _lambda;

        public LambdaConfigurator(Func<T> lambda)
        {
            _lambda = lambda;
        }

        public T GetPluginConfiguration()
        {
            return _lambda();
        }
    }
}
