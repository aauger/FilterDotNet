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
            if (dy < 0)
            {
                yi = -1;
                dy = -dy;
            }
            int d = (2 * dy) - dx;
            int y = first.Y;
            for (int x = first.X; x <= second.X; x++)
            {
                _instance.SetPixel(x, y, color);
                if (d > 0)
                {
                    y += yi;
                    d += 2 * (dy - dx);
                }
                else
                {
                    d += 2 * dy;
                }
            }
        }

        private void DrawLineHigh(Point first, Point second, IColor color)
        {
            int dx = second.X - first.X;
            int dy = second.Y - first.Y;
            int xi = 1;
            if (dy < 0)
            {
                xi = -1;
                dx = -dx;
            }
            int d = (2 * dx) - dy;
            int x = first.X;
            for (int y = first.Y; y <= second.Y; y++)
            {
                _instance.SetPixel(x, y, color);
                if (d > 0)
                {
                    x += xi;
                    d += 2 * (dx - dy);
                }
                else
                {
                    d += 2 * dx;
                }
            }
        }
    }
}
