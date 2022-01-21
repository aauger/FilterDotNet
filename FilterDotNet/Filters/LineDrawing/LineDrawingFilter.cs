using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using FilterDotNet.Drawing;
using FilterDotNet.Extensions;
using FilterDotNet.Utils;

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
        private int _segmentCount = 65;
        private int _segmentRandomDistance = 6;

        /* Properties */
        public string Name => "Line Drawing";

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
                int bestDistance = this._engine.MaxValue * 3;
                Point secondPoint = new() { X = 0, Y = 0 };

                IEnumerable<(int, int)> pointsRandom = 
                    Enumerable.Range(-this._pixelDistance/2, this._pixelDistance)
                        .SelectMany(ex => Enumerable.Range(-this._pixelDistance/2, this._pixelDistance)
                                            .Select(ey => (ex, ey)))
                        .OrderBy(t => this._random.Next());

                foreach ((int xOff, int yOff) in pointsRandom)
                {
                    //don't sample the origin
                    if (xOff == 0 && yOff == 0)
                        continue;

                    int ox = point.X + xOff;
                    int oy = point.Y + yOff;
                    int tempDistance = default;

                    if (!input.OutOfBounds(ox, oy) &&
                        (tempDistance = cPoint.Difference(input.GetPixel(ox, oy))) < bestDistance)
                    {
                        (secondPoint, bestDistance) = (new() { X = ox, Y = oy }, tempDistance);
                    }

                    if (bestDistance <= this._colorDistance)
                        goto SuitableFit;
                }
            SuitableFit:
                return (point, secondPoint);
            });

            IEnumerable<IEnumerable<Point>> linesWithSegments = lines.Select(pp => GenerateSegmented(pp.Item1, pp.Item2, input));
            foreach (IEnumerable<Point> segmentedLine in linesWithSegments)
            {
                IColor lineColor = input.GetPixel(segmentedLine.At(0).X, segmentedLine.At(0).Y);
                for (int i = 0; i < segmentedLine.Count() - 1; i++)
                {
                    g.DrawLine(segmentedLine.At(i), segmentedLine.At(i + 1), lineColor);
                }
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

        private IEnumerable<Point> GenerateSegmented(Point p1, Point p2, IImage input)
        {
            List<Point> wavyLine = new List<Point>();
            double dSeg = (double)this._segmentCount;
            int segDist = this._segmentRandomDistance;
            wavyLine.Add(p1);
            for (int i = 0; i < this._segmentCount; i++)
            {
                wavyLine.Add(
                    new Point
                    {
                        X = (int)MathUtils.Clamp(MathUtils.Lerp(p1.X, p2.X, i/dSeg) + this._random.Next(-segDist,segDist+1), 0, input.Width-1),
                        Y = (int)MathUtils.Clamp(MathUtils.Lerp(p1.Y, p2.Y, i/dSeg) + this._random.Next(-segDist,segDist+1), 0, input.Height-1)
                    }
                );
            }
            wavyLine.Add(p2);
            return wavyLine;

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
