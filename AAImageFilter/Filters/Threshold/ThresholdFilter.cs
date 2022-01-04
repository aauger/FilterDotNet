using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class ThresholdFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private int _threshold;
        private bool _ready = false;

        /* Properties */
        public string Name => "Threshold";

        public ThresholdFilter(IPluginConfigurator<int> pluginConfigurator, Func<int,int,IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._imageCreator = imageCreator;
            this._colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._imageCreator(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) => {
                    IColor here = input.GetPixel(x, y);
                    int avg = (here.R + here.G + here.B) / 3;
                    IColor nColor = avg > this._threshold ? this._colorCreator(255, 255, 255, 255) : this._colorCreator(0, 0, 0, 255);

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
