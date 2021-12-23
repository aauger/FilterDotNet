using AAImageFilter.Extensions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;
using AAImageFilter.Exceptions;

namespace AAImageFilter.Filters
{
    public class CirclePaintingFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(int, int, int)> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private bool _ready = false;
        private int _maxDiff = 0, _minRad = 0, _maxRad = 0;

        /* Properties */
        public string Name => "Circle Painting";

        public CirclePaintingFilter(IPluginConfigurator<(int, int, int)> pluginConfigurator, Func<int,int,IImage> imageCreator, Func<int,int,int,int,IColor> colorCreator)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._imageCreator = imageCreator;
            this._colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            IImage ret = _imageCreator(input.Width, input.Height);

            List<Circle> circles = new();
            for (int x = 0; x < input.Width; x += _minRad * 2)
            {
                for (int y = 0; y < input.Height; y += _minRad * 2)
                {
                    Circle c = new();
                    c.X = x;
                    c.Y = y;
                    c.Radius = _minRad;
                    c.Color = input.GetPixel(x, y);
                    circles.Add(c);
                }
            }

            Parallel.ForEach(circles, (c) =>
            {
                for (bool f = true; f && c.Radius < _maxRad; c.Radius++)
                {
                    int originX = c.X;
                    int originY = c.Y;

                    for (int deg = 0; deg < 360; deg += 10)
                    {
                        int rx = (int)(c.Radius * Math.Cos(MathUtils.DegToRad(deg)) + originX);
                        int ry = (int)(c.Radius * Math.Sin(MathUtils.DegToRad(deg)) + originY);

                        if (rx >= 0 && rx < input.Width
                            && ry >= 0 && ry < input.Height
                            && input.GetPixel(rx, ry).Difference(c.Color!) >= _maxDiff)
                        {
                            f = false;
                            break;
                        }
                    }
                }
            });

            foreach (Circle c in circles/*.Where(c => c.Radius > _minRad)*/.OrderBy(c => c.Radius))
            {
                Parallel.For(-c.Radius, c.Radius, (int x) =>
                {
                    int height = (int)Math.Sqrt(c.Radius * c.Radius - x * x);

                    Parallel.For(-height, height, (int y) =>
                    {
                        if (!ret.OutOfBounds(c.X + x, c.Y + y))
                            ret.SetPixel(c.X + x, c.Y + y, c.Color!);
                    });
                });
            }

            return ret;
        }

        public IFilter Initialize()
        {
            (_maxDiff, _minRad, _maxRad) = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
