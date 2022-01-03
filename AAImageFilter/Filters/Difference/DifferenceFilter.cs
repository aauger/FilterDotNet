using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;

namespace AAImageFilter.Filters
{
    public class DifferenceFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private IPluginConfigurator<(IImage, double)>? _pluginConfigurator;
        private Func<int, int, IImage> _imageCreator;
        private Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private IImage? _other;
        private double _multiplier = 1.0;
        private bool _ready = false;
        
        /* Properties */
        public string Name => "Difference";

        public DifferenceFilter(IPluginConfigurator<(IImage, double)> pluginConfigurator, Func<int, int, IImage> imageCreator, Func<int,int,int,int,IColor> colorCreator)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._colorCreator = colorCreator;
            this._imageCreator = imageCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage ret = this._imageCreator(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) => 
                {
                    IColor here = input.GetPixel(x, y);
                    IColor there = this._other!.GetPixel(x, y);

                    int rDiff = Math.Abs(here.R - there.R);
                    int gDiff = Math.Abs(here.G - there.G);
                    int bDiff = Math.Abs(here.B - there.B);

                    int r = MathUtils.RGBClamp((int)(rDiff * this._multiplier));
                    int g = MathUtils.RGBClamp((int)(gDiff * this._multiplier));
                    var b = MathUtils.RGBClamp((int)(bDiff * this._multiplier));

                    ret.SetPixel(x, y, this._colorCreator(r, g, b, 255));
                });
            });

            return ret;
        }

        public IFilter Initialize()
        {
            (this._other, this._multiplier) = this._pluginConfigurator!.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
