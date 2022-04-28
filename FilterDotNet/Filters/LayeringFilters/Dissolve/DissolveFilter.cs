using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Filters
{
    public class DissolveFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(IImage, int)> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private IImage? _other;
        private int? _factor;
        
        /* Properties */
        public string Name => "Dissolve";

        public DissolveFilter(IPluginConfigurator<(IImage, int)> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            if (this._other is null 
                || this._factor is null 
                || !(input.Width == this._other.Width && input.Height == this._other.Height))
                throw new BadConfigurationException("input and configured image have different dimensions");

            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) => 
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    IColor color = (DateTime.Now.Ticks % 101 < this._factor) switch
                    {
                        (true) => this._other.GetPixel(x, y),
                        (_) => input.GetPixel(x, y)
                    };
                    output.SetPixel(x, y, color);
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            (this._other, this._factor) = _pluginConfigurator.GetPluginConfiguration();                
            this._ready = true;
            return this;
        }
    }
}
