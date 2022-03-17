using FilterDotNet.Interfaces;
using FilterDotNet.Exceptions;

namespace FilterDotNet.Filters
{
    public class ColorMatrixFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<float[,]> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private float[,]? _matrix;

        /* Properties */
        public string Name => "Color Matrix";

        public ColorMatrixFilter(IPluginConfigurator<float[,]> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            float[,] m = this._matrix!;

            IImage output = this._engine!.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    IColor color = input.GetPixel(x, y);

                    int adjustedR = this._engine.Clamp((int)(color.R * m[0, 0] + color.G * m[0, 1] + color.B * m[0, 2]));
                    int adjustedG = this._engine.Clamp((int)(color.R * m[1, 0] + color.G * m[1, 1] + color.B * m[1, 2]));
                    int adjustedB = this._engine.Clamp((int)(color.R * m[2, 0] + color.G * m[2, 1] + color.B * m[2, 2]));
                    IColor adjustedColor = this._engine.CreateColor(adjustedR, adjustedG, adjustedB, color.A);
                    output.SetPixel(x, y, adjustedColor);
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._matrix = _pluginConfigurator!.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
