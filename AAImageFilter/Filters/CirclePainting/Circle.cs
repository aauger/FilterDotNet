using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    internal class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public IColor? Color { get; set; }
    }
}
