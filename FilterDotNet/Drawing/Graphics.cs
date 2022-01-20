using FilterDotNet.Interfaces;

namespace FilterDotNet.Drawing
{
    public class Graphics
    {
        private IImage _instance;

        public static Graphics FromIImage(IImage input) => new Graphics(input);
        private Graphics(IImage input)
        {
            this._instance = input;
        }

        /// <summary>
        /// This is merely a convenience, and you should theoretically never need it.
        /// If you don't control the instance, you should not be tinkering on its internals
        /// </summary>
        public IImage Unwrap() => _instance;

        public void DrawLine(Point first, Point second, IColor color)
        {
            if (Math.Abs(second.Y - first.Y) < Math.Abs(second.X - first.X))
            {
                if (first.X > second.X)
                    DrawLineLow(second, first, color);
                else
                    DrawLineLow(first, second, color);
            }
            else
            {
                if (first.Y > second.Y)
                    DrawLineHigh(second, first, color);
                else
                    DrawLineHigh(first, second, color);
                   
            }
        }

        private void DrawLineLow(Point first, Point second, IColor color)
        { 
            int dx = second.X - first.X;
            int dy = second.Y - first.Y;
            int yi = 1;
            (yi, dy) = dy < 0 ? (-1, -dy) : (yi, dy);
            for (int x = first.X, y = first.Y, d = (2 * dy) - dx; x <= second.X; x++)
            {
                _instance.SetPixel(x, y, color);
                (y, d) = d > 0 ? (y + yi, d + 2 * (dy - dx)) : (y, d + 2 * dy);
            }
        }

        private void DrawLineHigh(Point first, Point second, IColor color)
        {
            int dx = second.X - first.X;
            int dy = second.Y - first.Y;
            int xi = 1;
            (xi, dx) = dx < 0 ? (-1, -dx) : (xi, dx);
            for (int y = first.Y, x = first.X, d = (2 * dx) - dy; y <= second.Y; y++)
            {
                _instance.SetPixel(x, y, color);
                (x, d) = d > 0 ? (x + xi, d + 2 * (dx - dy)) : (x, d + 2 * dx);
            }
        }
    }
}
