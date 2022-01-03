using AAImageFilter.Interfaces;
using AAImageFilter.Exceptions;

namespace AAImageFilter.Filters
{
    public class PosterizeFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private int _levels;
        private bool _ready = false;

        /* Properties */
        public string Name => "Posterize";

        public PosterizeFilter(IPluginConfigurator<int> pluginConfigurator, Func<int, int, IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._imageCreator = imageCreator;
            this._colorCreator = colorCreator;
        }


        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage ret = this._imageCreator(input.Width, input.Height);
            int bs = 255 / this._levels;

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
                    ret.SetPixel(x, y, this._colorCreator(nr, ng, nb, 255));
                });
            });

            return ret;
        }

        public IFilter Initialize()
        {
            this._levels = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
