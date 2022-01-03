using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class ColorOverlayFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<IImage> _pluginConfigurator;
        private Func<int, int, IImage> _imageCreator;
        private Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private IImage? _mask;
        private bool _ready = false;

        /* Properties */
        public string Name => "Color Overlay";

        public ColorOverlayFilter(IPluginConfigurator<IImage> pluginConfigurator, Func<int, int, IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        {
            _pluginConfigurator = pluginConfigurator;
            _imageCreator = imageCreator;
            _colorCreator = colorCreator;
        }


        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._imageCreator(input.Width, input.Height);

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
                        output.SetPixel(x, y, this._colorCreator(0, 0, 0, 255));
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
