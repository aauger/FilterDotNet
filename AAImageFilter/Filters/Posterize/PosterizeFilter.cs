using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Interfaces;
using AAImageFilter.Exceptions;
using System.Drawing;

namespace AAImageFilter.Filters
{
    public class PosterizeFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private int _levels;
        private bool _ready = false;

        /* Properties */
        public string Name => "Posterize";

        public PosterizeFilter(IPluginConfigurator<int> pluginConfigurator, Func<int, int, IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        {
            _pluginConfigurator = pluginConfigurator;
            _imageCreator = imageCreator;
            _colorCreator = colorCreator;
        }


        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            IImage ret = _imageCreator(input.Width, input.Height);
            int bs = 255 / _levels;

            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    int nr, nb, ng;
                    IColor c = input.GetPixel(x, y);
                    // ((i / n) * n) will get the nearest n to i
                    nr = (int)(Math.Round(c.R / (float)bs) * bs);
                    ng = (int)(Math.Round(c.G / (float)bs) * bs);
                    nb = (int)(Math.Round(c.B / (float)bs) * bs);
                    ret.SetPixel(x, y, _colorCreator(nr, ng, nb, 255));
                }
            }

            return ret;
        }

        public IFilter Initialize()
        {
            this._levels = _pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
