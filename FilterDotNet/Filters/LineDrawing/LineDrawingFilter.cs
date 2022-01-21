using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using FilterDotNet.Drawing;
using FilterDotNet.Extensions;

namespace FilterDotNet.Filters
{
    public class LineDrawingFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(int, int, int)> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private Random _random;
        private int _count;
        private int _colorDistance;
        private int _pixelDistance;

        /* Properties */
        public string Name => "Line Drawing Filter";

        public LineDrawingFilter(IPluginConfigurator<(int, int, int)> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._engine.CreateImage(input.Width, input.Height);
            Graphics g = Graphics.FromIImage(output);
            List<Point> points = GeneratePoints(input);
            IEnumerable<(Point, Point)> lines = points.AsParallel().Select(point =>
            {
                 IColor cPoint = input.GetPixel(point.X, point.Y);
                 int bestDistance = 765;
                 Point secondPoint = new Point { X = 0, Y = 0 };
                 for (int xOff = this._pixelDistance / 2; xOff > -this._pixelDistance / 2; xOff--)
                 {
                     for (int yOff = this._pixelDistance / 2; yOff > -this._pixelDistance / 2; yOff--)
                     {
                         //don't sample the origin, obviously
                         if (xOff == 0 && yOff == 0)
                             continue;

                         int ox = point.X + xOff;
                         int oy = point.Y + yOff;
                         int tempDistance = default;

                         if (!input.OutOfBounds(ox, oy) &&
                             (tempDistance = cPoint.Difference(input.GetPixel(ox, oy))) < bestDistance)
                         {
                             (secondPoint, bestDistance) = (new Point { X = ox, Y = oy }, tempDistance);

                         }
                             
                         if (bestDistance <= this._colorDistance)
                            goto SuitableFit;
                     }
                 }
            SuitableFit:
                return (point, secondPoint);
            });
            foreach ((Point p1, Point p2) in lines)
            {
                g.DrawLine(p1, p2, input.GetPixel(p1.X, p1.Y));
            }
            return output;
        }

        private List<Point> GeneratePoints(IImage input)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < this._count; i++)
            {
                Point p = new()
                {
                    X = _random.Next(0, input.Width),
                    Y = _random.Next(0, input.Height)
                };
                points.Add(p);
            }
            return points;
        }

        public IFilter Initialize()
        {
            (this._count, this._colorDistance, this._pixelDistance) = _pluginConfigurator.GetPluginConfiguration();
            this._random = new Random();
            this._ready = true;
            return this;
        }
    }
}
