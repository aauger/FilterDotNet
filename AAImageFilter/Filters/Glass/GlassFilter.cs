using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;

namespace AAImageFilter.Filters
{
    public class GlassFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;

        /* Internals */
        private int _glassDistance;
        private bool _ready = false;
        
        /* Properties */
        public string Name => "Glass";

        public GlassFilter(IPluginConfigurator<int> pluginConfigurator, Func<int,int,IImage> imageCreator)
        { 
            this._pluginConfigurator = pluginConfigurator;
            this._imageCreator = imageCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            Random rnd = new Random();
            IImage output = this._imageCreator(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    int x2 = MathUtils.Clamp(x + rnd.Next(-this._glassDistance, this._glassDistance), 0, input.Width - 1);
                    int y2 = MathUtils.Clamp(y + rnd.Next(-this._glassDistance, this._glassDistance), 0, input.Height - 1);
                    IColor here = input.GetPixel(x, y);
                    IColor there = input.GetPixel(x2, y2);

                    output.SetPixel(x, y, there);
                    output.SetPixel(x2, y2, here);
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._glassDistance = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
