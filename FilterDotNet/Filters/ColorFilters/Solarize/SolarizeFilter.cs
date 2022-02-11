using FilterDotNet.Interfaces;
using FilterDotNet.Exceptions;
using FilterDotNet.Extensions;

namespace FilterDotNet.Filters
{
    public class SolarizeFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private int _solarizeThreshold;
        private bool _ready = false;

        /* Properties */
        public string Name => "Solarize";

        public SolarizeFilter(IPluginConfigurator<int> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) => {
                    IColor here = input.GetPixel(x, y);
                    int avg = (here.R + here.G + here.B) / 3;
                    IColor nColor = avg > this._solarizeThreshold ? here.Inverse(this._engine) : here;

                    output.SetPixel(x, y, nColor);
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._solarizeThreshold = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
