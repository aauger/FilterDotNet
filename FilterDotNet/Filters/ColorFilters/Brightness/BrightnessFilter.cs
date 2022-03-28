using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class BrightnessFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private IPluginConfigurator<int> _pluginConfigurator;
        private IEngine _engine;

        /* Internals */
        private float _multiplier = 1.0f;

        public BrightnessFilter(IPluginConfigurator<int> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public string Name => "Adjust Brightness";

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) => 
            {
                Parallel.For(0, input.Height, (int y) => 
                {
                    IColor original = input.GetPixel(x, y);
                    int nr = this._engine.Clamp((int)(original.R * this._multiplier));
                    int ng = this._engine.Clamp((int)(original.G * this._multiplier));
                    int nb = this._engine.Clamp((int)(original.B * this._multiplier));

                    output.SetPixel(x, y, this._engine.CreateColor(nr, ng, nb, original.A));
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._multiplier = _pluginConfigurator.GetPluginConfiguration() / 100.0f;
            return this;
        }
    }
}
