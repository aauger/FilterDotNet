using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class ThresholdFilter : IFilter, IConfigurableFilter
    {
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;
        private int _threshold;

        private bool _ready = false;

        public ThresholdFilter(IPluginConfigurator<int> pluginConfigurator, Func<int, int, int, int, IColor> colorCreator)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._colorCreator = colorCreator;
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
                    IColor nColor = avg > _threshold ? _colorCreator(255,255,255,255) : _colorCreator(0,0,0,255);

                    input.SetPixel(x, y, nColor);
                }
            }

            return input;
        }

        public string GetFilterName()
        {
            return "Threshold";
        }

        public IFilter Initialize()
        {
            _threshold = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
