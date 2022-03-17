using FilterDotNet.Exceptions;
using FilterDotNet.Extensions;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class FixChromaticAberrationFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private int _offset = default;

        /* Properties */
        public string Name => "Fix Chromatic Aberration";

        public FixChromaticAberrationFilter(IPluginConfigurator<int> pluginConfigurator, IEngine engine)
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
                Parallel.For(0, input.Height, (int y) => 
                {
                    IColor here = input.GetPixel(x, y);
                    int nr = here.R, ng = here.G, nb = here.B;
                    if (!input.OutOfBounds(x - this._offset, y - this._offset))
                        ng = input.GetPixel(x - this._offset, y - this._offset).G;
                    if (!input.OutOfBounds(x + this._offset, y + this._offset))
                        nr = input.GetPixel(x + this._offset, y + this._offset).R;
                    output.SetPixel(x, y, this._engine.CreateColor(nr, ng, nb, here.A));
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._offset = _pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
