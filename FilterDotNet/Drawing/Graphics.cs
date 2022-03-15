using FilterDotNet.Common;
using FilterDotNet.Extensions;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Drawing
{
    public class Graphics
    {
        private readonly IImage _instance;

        private Graphics(IImage input)
        {
            this._instance = input;
        }

        public static Graphics FromIImage(IImage input) => new(input);

        /// <summary>
        /// This is merely a convenience, and you should theoretically never need it.
        /// If you don't control the instance, you should not be tinkering on its internals
        /// </summary>
        public IImage Unwrap() => _instance;

        #region Bresenham's Line Algorithm

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
                if (!_instance.OutOfBounds(x, y))
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
                if (!_instance.OutOfBounds(x, y))
                    _instance.SetPixel(x, y, color);
                (x, d) = d > 0 ? (x + xi, d + 2 * (dx - dy)) : (x, d + 2 * dx);
            }
        }

        #endregion

        #region Bresenham's Circle Plotting/Filling

        public void DrawCircle(Point point, int radius, IColor color) => CircleInternal(point, DrawPoints, radius, color);
        public void FillCircle(Point point, int radius, IColor color) => CircleInternal(point, FillLine, radius, color);

        private void CircleInternal(Point point, Action<Point, Point, IColor> fn, int radius, IColor color)
        {
            int x = 0;
            int y = radius;
            int m = 5 - 4 * radius;

            while (x <= y)
            {
                fn(new Point(point.X - x, point.Y - y),
                    new Point(point.X + x, point.Y - y),
                    color);
                fn(new Point(point.X - y, point.Y - x),
                    new Point(point.X + y, point.Y - x),
                    color);
                fn(new Point(point.X - y, point.Y + x),
                    new Point(point.X + y, point.Y + x),
                    color);
                fn(new Point(point.X - x, point.Y + y),
                    new Point(point.X + x, point.Y + y),
                    color);

                if (m > 0)
                {
                    y--;
                    m -= 8 * y;
                }

                x++;
                m += 8 * x + 4;
            }
        }

        private void FillLine(Point first, Point second, IColor color) => DrawLine(first, second, color);
        private void DrawPoints(Point first, Point second, IColor color)
        {
            if (!_instance.OutOfBounds(first.X, first.Y))
                _instance.SetPixel(first.X, first.Y, color);
            if (!_instance.OutOfBounds(second.X, second.Y))
                _instance.SetPixel(second.X, second.Y, color);
        }

        #endregion

        #region Bresenham's Triangle Plotting/Filling

        public void DrawTriangle(Point first, Point second, Point third, IColor color)
        {
            DrawLine(first, second, color);
            DrawLine(second, third, color);
            DrawLine(first, third, color);
        }

        public void FillTriangle(Point first, Point second, Point third, IColor color)
        {
            IEnumerable<Point> orderedPoints = (new List<Point>() { first, second, third }).OrderBy(p => p.Y);

            Point v1 = orderedPoints.At(0);
            Point v2 = orderedPoints.At(1);
            Point v3 = orderedPoints.At(2);

            if (v2.Y == v3.Y)
                FillBottomFlatTriangle(v1, v2, v3, color);
            else if (v1.Y == v2.Y)
                FillTopFlatTriangle(v1, v2, v3, color);
            else
            {
                Point v4 = new Point(
                    (int)(v1.X + ((double)(v2.Y - v1.Y) / (double)(v3.Y - v1.Y)) * (v3.X - v1.X)), 
                    v2.Y
                );
                FillBottomFlatTriangle(v1, v2, v4, color);
                FillTopFlatTriangle(v2, v4, v3, color);
            }
        }

        private void FillTopFlatTriangle(Point first, Point second, Point third, IColor color)
        {
            double invSlopeTF = (double)(third.X - first.X) / (double)(third.Y - first.Y);
            double invSlopeTS = (double)(third.X - second.X) / (double)(third.Y - second.Y);

            double curXF = third.X;
            double curXS = third.X;

            for (int scanlineY = third.Y; scanlineY > first.Y; scanlineY--)
            {
                DrawLine(new Point((int)curXF, scanlineY), new Point((int)curXS, scanlineY), color);
                curXF -= invSlopeTF;
                curXS -= invSlopeTS;
            }
        }

        private void FillBottomFlatTriangle(Point first, Point second, Point third, IColor color)
        {
            double invSlopeTF = (double)(second.X - first.X) / (double)(second.Y - first.Y);
            double invSlopeTS = (double)(third.X - first.X) / (double)(third.Y - first.Y);

            double curXF = first.X;
            double curXS = first.X;

            for (int scanlineY = first.Y; scanlineY <= second.Y; scanlineY++)
            {
                DrawLine(new Point((int)curXF, scanlineY), new Point((int)curXS, scanlineY), color);
                curXF += invSlopeTF;
                curXS += invSlopeTS;
            }
        }

        #endregion

        public void Fill(IColor color)
        {
            for (int x = 0; x < _instance.Width; x++)
            {
                for (int y = 0; y < _instance.Height; y++)
                {
                    _instance.SetPixel(x, y, color);
                }
            }
        }
    }
}
