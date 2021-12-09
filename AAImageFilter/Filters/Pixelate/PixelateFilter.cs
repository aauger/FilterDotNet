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
        private readonly IPluginConfigurator<(int, int)> _pluginConfigurator;
        private int _width = 0;
        private int _height = 0;
        private bool _ready = false;

        public PixelateFilter(IPluginConfigurator<(int, int)> pluginConfigurator)
        { 
            this._pluginConfigurator = pluginConfigurator;
        }

        public Image Apply(Image input)
        {
            if (!_ready)
                throw new NotReadyException();

            Bitmap bmp = (Bitmap)input;

            for (int x = 0; x < bmp.Width; x += _width)
            {
                for (int y = 0; y < bmp.Height; y += _height)
                {
                    Color r = bmp.GetPixel(x, y);

                    for (int xi = x; xi < (x + _width) && xi < bmp.Width; xi++)
                    {
                        for (int yi = y; yi < (y + _height) && yi < bmp.Height; yi++)
                        {
                            bmp.SetPixel(xi, yi, r);
                        }
                    }
                }
            }

            return bmp;
        }

        public string GetFilterName()
        {
            return "Pixelate";
        }

        public IFilter Initialize()
        {
            (_width, _height) = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
