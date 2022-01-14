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
        private List<VoronoiNode>? _voronoiNodes;

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
            GenerateNodes(input);
            Parallel.For(0, input.Width, (int x) => 
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    // Euclidean distance, but Manhattan distance is also interesting.
                    VoronoiNode vNode = this._voronoiNodes!
                        .AsParallel()
                        .MinBy(vn => Math.Sqrt(Math.Pow(x - vn.X, 2) + Math.Pow(y - vn.Y, 2)))!;
                    output.SetPixel(x, y, vNode!.Color!);
                });
            });

            return output;
        }

        private void GenerateNodes(IImage input)
        {
            Random rnd = new Random();
            for (int i = 0; i < this._voronoiNodeCount; i++)
            {
                int x = rnd.Next(0, input.Width);
                int y = rnd.Next(0, input.Height);
                this._voronoiNodes!.Add(new()
                {
                    X = x,
                    Y = y,
                    Color = input.GetPixel(x, y)
                });
            }
        }

        public IFilter Initialize()
        {
            this._voronoiNodeCount = this._pluginConfigurator.GetPluginConfiguration();
            this._voronoiNodes = new List<VoronoiNode>(this._voronoiNodeCount);
            this._ready = true;
            return this;
        }
    }
}
