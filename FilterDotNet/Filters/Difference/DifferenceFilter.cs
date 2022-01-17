using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using FilterDotNet.Utils;

namespace FilterDotNet.Filters
{
    public class DifferenceFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private IPluginConfigurator<(IImage, double)>? _pluginConfigurator;
        private IEngine _engine;

        /* Internals */
        private IImage? _other;
        private double _multiplier = 1.0;
        private bool _ready = false;
        
        /* Properties */
        public string Name => "Difference";

        public DifferenceFilter(IPluginConfigurator<(IImage, double)> pluginConfigurator, IEngine engine)
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
                    IColor here = input.GetPixel(x, y);
                    IColor there = this._other!.GetPixel(x, y);

                    int rDiff = Math.Abs(here.R - there.R);
                    int gDiff = Math.Abs(here.G - there.G);
                    int bDiff = Math.Abs(here.B - there.B);

                    int r = MathUtils.Clamp(this._engine.MinValue,
                        this._engine.MaxValue,
                        (int)(rDiff * this._multiplier));
                    int g = MathUtils.Clamp(this._engine.MinValue,
                        this._engine.MaxValue,
                        (int)(gDiff * this._multiplier));
                    var b = MathUtils.Clamp(this._engine.MinValue,
                        this._engine.MaxValue,
                        (int)(bDiff * this._multiplier));

                    output.SetPixel(x, y, this._engine.CreateColor(r, g, b, 255));
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            (this._other, this._multiplier) = this._pluginConfigurator!.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
