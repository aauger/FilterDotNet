using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class SortPixelsFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Sort Pixels";

        public SortPixelsFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            IEnumerable<Node> sortedNodes = CollectNodes(input)
                .OrderBy(n => n.Color!.R)
                .ThenBy(n => n.Color!.G)
                .ThenBy(n => n.Color!.B);

            IEnumerator<Node> sortedNodesIter = sortedNodes.GetEnumerator();
            sortedNodesIter.MoveNext();

            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    IColor color = sortedNodesIter.Current.Color!;
                    output.SetPixel(x, y, color);
                    sortedNodesIter.MoveNext();
                }
            }

            return output;
        }

        public IEnumerable<Node> CollectNodes(IImage image)
        {
            return Enumerable.Range(0, image.Width)
                .SelectMany(x => Enumerable.Range(0, image.Height)
                                    .Select(y => new Node() { X = x, Y = y, Color = image.GetPixel(x, y) }));
        }
    }
}
