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
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private int _threshold;
        private bool _ready = false;

        /* Properties */
        public string Name => "Threshold";

        public ThresholdFilter(IPluginConfigurator<int> pluginConfigurator, Func<int, int, int, int, IColor> colorCreator)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) => {
                    IColor here = input.GetPixel(x, y);
                    int avg = (here.R + here.G + here.B) / 3;
                    IColor nColor = avg > _threshold ? _colorCreator(255, 255, 255, 255) : _colorCreator(0, 0, 0, 255);

                    input.SetPixel(x, y, nColor);
                });
            });

            return input;
        }

        public IFilter Initialize()
        {
            _threshold = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
