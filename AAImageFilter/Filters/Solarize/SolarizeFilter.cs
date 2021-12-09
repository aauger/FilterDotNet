using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Interfaces;
using AAImageFilter.Exceptions;
using AAImageFilter.Extensions;
using System.Drawing;

namespace AAImageFilter.Filters
{
    public class SolarizeFilter : IFilter, IConfigurableFilter
    {
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private int _solarizeThreshold;
        private bool _ready = false;

        public SolarizeFilter(IPluginConfigurator<int> pluginConfigurator)
        { 
            this._pluginConfigurator = pluginConfigurator;
        }

        public Image Apply(Image input)
        {
            if (!_ready)
                throw new NotReadyException();

            Bitmap b = (Bitmap)input;

            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    Color here = b.GetPixel(x, y);
                    int avg = (here.R + here.G + here.B) / 3;
                    Color nColor = avg > _solarizeThreshold ? here.Inverse() : here;

                    b.SetPixel(x, y, nColor);
                }
            }

            return b;
        }

        public string GetFilterName()
        {
            return "Solarize";
        }

        public IFilter Initialize()
        {
            _solarizeThreshold = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
