using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IColor? Color { get; set; }

        public static IEnumerable<Node> Collect(IImage image)
        {
            return Enumerable.Range(0, image.Width)
                    .SelectMany(x => Enumerable.Range(0, image.Height)
                        .Select(y => new Node() { X = x, Y = y, Color = image.GetPixel(x, y) }));
        }
    }
}