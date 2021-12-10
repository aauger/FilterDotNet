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
        private readonly Func<int, int, int, int, IColor> _colorCreator;
        private int _solarizeThreshold;
        private bool _ready = false;

        public SolarizeFilter(IPluginConfigurator<int> pluginConfigurator, Func<int, int, int, int, IColor> colorCreator)
        {
            this._pluginConfigurator = pluginConfigurator;
            _colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    IColor here = input.GetPixel(x, y);
                    int avg = (here.R + here.G + here.B) / 3;
                    IColor nColor = avg > _solarizeThreshold ? here.Inverse(_colorCreator) : here;

                    input.SetPixel(x, y, nColor);
                }
            }

            return input;
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
