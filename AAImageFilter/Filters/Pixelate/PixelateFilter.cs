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
    public class PixelateFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(int, int)> _pluginConfigurator;

        /* Internals */
        private int _width = 0;
        private int _height = 0;
        private bool _ready = false;

        /* Properties */
        public string Name => "Pixelate";

        public PixelateFilter(IPluginConfigurator<(int, int)> pluginConfigurator)
        { 
            this._pluginConfigurator = pluginConfigurator;
        }

        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            for (int x = 0; x < input.Width; x += _width)
            {
                for (int y = 0; y < input.Height; y += _height)
                {
                    IColor r = input.GetPixel(x, y);

                    for (int xi = x; xi < (x + _width) && xi < input.Width; xi++)
                    {
                        for (int yi = y; yi < (y + _height) && yi < input.Height; yi++)
                        {
                            input.SetPixel(xi, yi, r);
                        }
                    }
                }
            }

            return input;
        }

        public IFilter Initialize()
        {
            (_width, _height) = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
