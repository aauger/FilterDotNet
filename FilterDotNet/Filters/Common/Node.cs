using FilterDotNet.Common;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class Node
    {
        public Point Point { get; set; }
        public IColor? Color { get; set; }

        public static IEnumerable<Node> Collect(IImage image) =>
            from x in Enumerable.Range(0, image.Width)
            from y in Enumerable.Range(0, image.Height)
            select new Node { Point = new(x, y), Color = image.GetPixel(x, y) };
    }
}