using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Filters.Statisical
{
    public enum StatisticalFilterMode { 
        MIN,
        MAX,
        MEDIAN,
        MODE,
        AVERAGE
    }
 
    public class StatisticalFilterConfiguration {
        public StatisticalFilterMode Mode { get; set; } = StatisticalFilterMode.AVERAGE;
        public bool Thresholding { get; set; } = false;
        public int Threshold { get; set; } = 0;
        public int BlockSize { get; set; } = 0;
    }

    public class StatisticalFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<StatisticalFilterConfiguration> _pluginConfigurator;


        /* Properties */
        public string Name => "Statistical (Min, Max, Median, Mode)";

        public StatisticalFilter() { 
            
        }

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
