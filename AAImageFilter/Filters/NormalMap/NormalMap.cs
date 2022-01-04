using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;

namespace AAImageFilter.Filters
{
    public class NormalMap : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(IImage, double)> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private IImage? _map;
        private double _multiplier = 1.0;
        private bool _ready = false;

        /* Properties */
        public string Name => "Normal Map";

        public NormalMap(IPluginConfigurator<(IImage, double)> pluginConfigurator, Func<int, int, IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
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

            Parallel.For(0, input.Width, (int x) => {
                Parallel.For(0, input.Height, (int y) => 
                {
                    IColor mapColor = this._map!.GetPixel(x, y);
                    double xm = MathUtils.Map(mapColor.R, 0, 255, -1, 1);
                    double ym = MathUtils.Map(mapColor.G, 0, 255, -1, -1);
                    double zm = MathUtils.Map(mapColor.B, 128, 255, 0, -1);
                    double lat = Math.Acos(zm);
                    double lon = Math.Atan2(ym, xm);

                    int xCalc = (int)Math.Round(x + ((xm) * this._multiplier));
                    int xCalcWrapF = xCalc > input.Width - 1 ? xCalc - input.Width - 1 : xCalc;
                    int xCalcWrapS = xCalcWrapF < 0 ? -xCalcWrapF : xCalcWrapF;

                    int yCalc = (int)Math.Round(y + ((ym) * this._multiplier));
                    int yCalcWrapF = yCalc > input.Height - 1 ? yCalc - input.Height - 1 : yCalc;
                    int yCalcWrapS = yCalcWrapF < 0 ? -yCalcWrapF : yCalcWrapF;

                    IColor transposedLocation = input.GetPixel(xCalcWrapS, yCalcWrapS);
                    output.SetPixel(x, y, transposedLocation);
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            (this._map, this._multiplier) = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
