using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class ColorOverlayFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<IImage> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private IImage? _mask;
        private bool _ready = false;

        /* Properties */
        public string Name => "Color Overlay";

        public ColorOverlayFilter(IPluginConfigurator<IImage> pluginConfigurator, IEngine engine)
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
                    IColor m = this._mask!.GetPixel(x, y);
                    bool isWhite =
                        m.R == 255 && m.G == 255 && m.B == 255;
                    if (!isWhite)
                        output.SetPixel(x, y, input.GetPixel(x, y));
                    else
                        output.SetPixel(x, y, this._engine.CreateColor(0, 0, 0, 255));
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._mask = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
