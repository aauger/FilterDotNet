using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class VoronoiSketchFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private int _voronoiNodeCount = 0;

        /* Properties */
        public string Name => "Voronoi Sketch";

        public VoronoiSketchFilter(IPluginConfigurator<int> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._engine.CreateImage(input.Width, input.Height);
            IEnumerable<Node> nodes = NodeGenerator(input);
            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    // Euclidean distance, but Manhattan distance is also interesting.
                    Node vNode = nodes
                        .AsParallel()
                        .MinBy(vn => Math.Sqrt(Math.Pow(x - vn.Point.X, 2) + Math.Pow(y - vn.Point.Y, 2)))!;
                    output.SetPixel(x, y, vNode!.Color!);
                });
            });

            return output;
        }

        private IEnumerable<Node> NodeGenerator(IImage input)
        {
            Random rnd = new();
            for (int i = 0; i < this._voronoiNodeCount; i++)
            {
                int x = rnd.Next(0, input.Width);
                int y = rnd.Next(0, input.Height);
                yield return new()
                {
                    Point = new(x, y),
                    Color = input.GetPixel(x, y)
                };
            }
        }

        public IFilter Initialize()
        {
            this._voronoiNodeCount = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
