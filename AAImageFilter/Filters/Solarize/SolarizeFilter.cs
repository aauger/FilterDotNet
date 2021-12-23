using AAImageFilter.Interfaces;
using AAImageFilter.Exceptions;
using AAImageFilter.Extensions;

namespace AAImageFilter.Filters
{
    public class SolarizeFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private int _solarizeThreshold;
        private bool _ready = false;

        /* Properties */
        public string Name => "Solarize";

        public SolarizeFilter(IPluginConfigurator<int> pluginConfigurator, Func<int, int, int, int, IColor> colorCreator)
        {
            this._pluginConfigurator = pluginConfigurator;
            _colorCreator = colorCreator;
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
                    IColor nColor = avg > _solarizeThreshold ? here.Inverse(_colorCreator) : here;

                    input.SetPixel(x, y, nColor);
                });
            });

            return input;
        }

        public IFilter Initialize()
        {
            _solarizeThreshold = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
