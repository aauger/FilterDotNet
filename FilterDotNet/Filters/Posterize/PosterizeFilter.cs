using FilterDotNet.Interfaces;
using FilterDotNet.Exceptions;

namespace FilterDotNet.Filters
{
    public class PosterizeFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private int _levels;
        private bool _ready = false;

        /* Properties */
        public string Name => "Posterize";

        public PosterizeFilter(IPluginConfigurator<int> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }


        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._engine.CreateImage(input.Width, input.Height);
            int bs = this._engine.MaxValue / this._levels;

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    int nr, nb, ng;
                    IColor c = input.GetPixel(x, y);
                    // ((i / n) * n) will get the nearest n to i
                    nr = (int)(Math.Round(c.R / (float)bs) * bs);
                    ng = (int)(Math.Round(c.G / (float)bs) * bs);
                    nb = (int)(Math.Round(c.B / (float)bs) * bs);
                    output.SetPixel(x, y, this._engine.CreateColor(nr, ng, nb, this._engine.MaxValue));
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._levels = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
