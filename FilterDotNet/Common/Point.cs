namespace FilterDotNet.Common
{
    public readonly struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        { 
            this.X = x;
            this.Y = y;
        }
    }
}
