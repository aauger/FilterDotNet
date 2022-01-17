using FilterDotNet.Exceptions;
using FilterDotNet.Extensions;
using FilterDotNet.Interfaces;
using FilterDotNet.Utils;

namespace FilterDotNet.Filters
{
    public class FloydSteinbergDitherFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<List<IColor>> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private List<IColor>? _colorList;
        private bool _ready = false;

        /* Properties */
        public string Name => "Floyd-Steinberg Dither";

        public FloydSteinbergDitherFilter(IPluginConfigurator<List<IColor>> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = input.Clone(this._engine.CreateImage);
            List<IColor> lCol = this._colorList!;
            for (int y = 0; y < output.Height; y++)
            {
                for (int x = 0; x < output.Width; x++)
                {
                    IColor oldC = output.GetPixel(x, y);
                    (IColor newC, ColorError error) = FindNearest(lCol, oldC);
                    output.SetPixel(x, y, newC);
                    if (!output.OutOfBounds(x + 1, y))
                        output.SetPixel(x + 1, y, RebaseColor(output.GetPixel(x + 1, y), error, .4375));
                    if (!output.OutOfBounds(x - 1, y + 1))
                        output.SetPixel(x - 1, y + 1, RebaseColor(output.GetPixel(x - 1, y + 1), error, .1875));
                    if (!output.OutOfBounds(x, y + 1))
                        output.SetPixel(x, y + 1, RebaseColor(output.GetPixel(x, y + 1), error, .3125));
                    if (!output.OutOfBounds(x + 1, y + 1))
                        output.SetPixel(x + 1, y + 1, RebaseColor(output.GetPixel(x + 1, y + 1), error, .0625));
                }
            }

            return output;
        }

        private static (IColor, ColorError) FindNearest(List<IColor> colors, IColor color)
        {
            return colors.Select(c => (c, Error(c, color))).MinBy(tp => tp.Item2.Avg());
        }

        private static ColorError Error(IColor input, IColor basis)
        {
            return new()
            {
                RedError = (basis.R - input.R),
                GreenError = (basis.G - input.G),
                BlueError = (basis.B - input.B),
            };
        }

        private IColor RebaseColor(IColor color, ColorError error, double factor)
        {
            return this._engine.CreateColor(
                MathUtils.Clamp(this._engine.MinValue, 
                this._engine.MaxValue, 
                color.R + (int)(factor * error.RedError)),
                MathUtils.Clamp(this._engine.MinValue, 
                this._engine.MaxValue, 
                color.G + (int)(factor * error.GreenError)),
                MathUtils.Clamp(this._engine.MinValue, 
                this._engine.MaxValue, 
                color.B + (int)(factor * error.BlueError)),
                255);
        }

        public IFilter Initialize()
        {
            this._colorList = _pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
