using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class DifferenceFilter : IFilter, IConfigurableFilter
    {
        /* DI */

        /* Internals */
        private IImage _other = ;
        private double _multiplier = 1.0;
        
        /* Properties */
        public string Name => "Difference";

        public IImage Apply(IImage input)
        {
            throw new NotImplementedException();
        }

        public IFilter Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
