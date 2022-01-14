using FilterDotNet.Exceptions;
using FilterDotNet.Extensions;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class ClippingRectangle 
    { 
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ClippingRectangleFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(ClippingRectangle, IFilter)> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private IFilter? _filter;
        private ClippingRectangle? _clip;

        public ClippingRectangleFilter(IPluginConfigurator<(ClippingRectangle, IFilter)> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public string Name => "Clipping Rectangle Filter";

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = input.Clone(this._engine.CreateImage);
            IImage clippedRectImage = Clip(input);
            IFilter filter = this._filter!;
            if (filter is IConfigurableFilter icf)
                icf.Initialize();
            IImage clippedFilteredOutput = filter.Apply(clippedRectImage);
            Parallel.For(0, this._clip!.Width, (int x) => 
            {
                Parallel.For(0, this._clip!.Height, (int y) => 
                {
                    output.SetPixel(x + this._clip.X, y + this._clip.Y, clippedFilteredOutput.GetPixel(x, y));
                });
            });

            return output;
        }

        private IImage Clip(IImage input)
        {
            IImage output = this._engine.CreateImage(this._clip!.Width, this._clip!.Height);
            Parallel.For(0, this._clip.Width, (int x) => 
            {
                Parallel.For(0, this._clip.Height, (int y) => 
                { 
                    output.SetPixel(x, y,
                        input.GetPixel(x + this._clip.X, y + this._clip.Y));
                });
            });
            return output;
        }

        public IFilter Initialize()
        {
            (this._clip, this._filter) = _pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
