using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class ThresholdFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private int _threshold;
        private bool _ready = false;

        /* Properties */
        public string Name => "Threshold";

        public ThresholdFilter(IPluginConfigurator<int> pluginConfigurator, IEngine engine)
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
                    IColor nColor = avg > this._threshold ? 
                        this._engine.CreateColor(this._engine.MaxValue, 
                            this._engine.MaxValue, 
                            this._engine.MaxValue, 
                            this._engine.MaxValue) 
                        : this._engine.CreateColor(this._engine.MinValue, 
                            this._engine.MinValue, 
                            this._engine.MinValue, 
                            this._engine.MaxValue);

                    output.SetPixel(x, y, nColor);
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._threshold = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
