using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using FilterDotNet.Drawing;
using FilterDotNet.Extensions;
using FilterDotNet.Utils;

namespace FilterDotNet.Filters
{
    public class LineDrawingConfiguration
    {
        public int Count { get; set; } = default;
        public int ColorDistance { get; set; } = default;
        public int PixelDistanceHorizontal { get; set; } = default;
        public int PixelDistanceVertical { get; set; } = default;
        public int SegmentCount { get; set; } = default;
        public int SegmentRandomDistanceHorizontal { get; set; } = default;
        public int SegmentRandomDistanceVertical { get; set; } = default;
    }

    public class LineDrawingFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<LineDrawingConfiguration> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private Random _random;
        private LineDrawingConfiguration _configuration;

        /* Properties */
        public string Name => "Line Drawing";

        public LineDrawingFilter(IPluginConfigurator<LineDrawingConfiguration> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            LineDrawingConfiguration cfg = this._configuration;
            IImage output = this._engine.CreateImage(input.Width, input.Height);
            Graphics g = Graphics.FromIImage(output);
            List<Point> points = GeneratePoints(input);
            IEnumerable<(Point, Point)> lines = points.AsParallel().Select(point =>
            {
                IColor cPoint = input.GetPixel(point.X, point.Y);
                int bestDistance = this._engine.MaxValue * 3;
                Point secondPoint = new() { X = 0, Y = 0 };

                IEnumerable<(int, int)> pointsRandom = 
                    Enumerable.Range(-cfg.PixelDistanceHorizontal/2, cfg.PixelDistanceHorizontal)
                        .SelectMany(ex => Enumerable.Range(-cfg.PixelDistanceVertical/2, cfg.PixelDistanceVertical)
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

                    if (bestDistance <= cfg.ColorDistance)
                        break;
                }
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
            LineDrawingConfiguration cfg = this._configuration;
            List<Point> points = new List<Point>();
            for (int i = 0; i < cfg.Count; i++)
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
            LineDrawingConfiguration cfg = this._configuration;
            List<Point> segmentLine = new List<Point>();
            double dSeg = (double)cfg.SegmentCount;
            int segHDist = cfg.SegmentRandomDistanceHorizontal;
            int segVDist = cfg.SegmentRandomDistanceVertical;
            segmentLine.Add(p1);
            for (int i = 0; i < cfg.SegmentCount; i++)
            {
                segmentLine.Add(
                    new Point
                    {
                        X = (int)MathUtils.Clamp(MathUtils.Lerp(p1.X, p2.X, i/dSeg) + this._random.Next(-segHDist,segHDist+1), 0, input.Width-1),
                        Y = (int)MathUtils.Clamp(MathUtils.Lerp(p1.Y, p2.Y, i/dSeg) + this._random.Next(-segVDist,segVDist+1), 0, input.Height-1)
                    }
                );
            }
            segmentLine.Add(p2);
            return segmentLine;

        }

        public IFilter Initialize()
        {
            this._configuration = _pluginConfigurator.GetPluginConfiguration();
            this._random = new Random();
            this._ready = true;
            return this;
        }
    }
}
