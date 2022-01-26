using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IColor? Color { get; set; }
    }
}