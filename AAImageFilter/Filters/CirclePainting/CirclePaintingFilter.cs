using AAImageFilter.Common;
using AAImageFilter.Extensions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Filters;
using AAImageFilter.Exceptions;

namespace AAImageFilter.Filters
{
    public class CirclePaintingFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(int, int, int)> _pluginConfigurator;
        private readonly Func<IImage, FastImage> _imageAdaptor;
        private readonly Func<FastImage, IImage> _imageOutdaptor;
        
        /* Internals */
        private bool _ready = false;
        private int _maxDiff = 0, _minRad = 0, _maxRad = 0;

        /* Properties */
        public string Name => "Circle Painting";

        public CirclePaintingFilter(IPluginConfigurator<(int, int, int)> pluginConfigurator, Func<IImage,FastImage> imageAdaptor, Func<FastImage, IImage> imageOutdaptor)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._imageAdaptor = imageAdaptor;
            this._imageOutdaptor = imageOutdaptor;
        }


        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            FastImage src = _imageAdaptor(input);
            FastImage ret = new FastImage(src.Width, src.Height);
            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    ret.SetPixel(x, y, new FastImageColor(0, 0, 0));
                }
            }

            List<Circle> circles = new();
            for (int x = 0; x < src.Width; x += _minRad * 2)
            {
                for (int y = 0; y < src.Height; y += _minRad * 2)
                {
                    Circle c = new();
                    c.X = x;
                    c.Y = y;
                    c.Radius = _minRad;
                    c.Color = src.GetPixel(x, y);
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

                        if (rx >= 0 && rx < src.Width
                            && ry >= 0 && ry < src.Height
                            && src.GetPixel(rx, ry).Diff(c.Color) >= _maxDiff)
                        {
                            f = false;
                            break;
                        }
                    }
                }
            });

            foreach (Circle c in circles/*.Where(c => c.Radius > _minRad)*/.OrderBy(c => c.Radius))
            {
                for (int x = -c.Radius; x < c.Radius; x++)
                {
                    int height = (int)Math.Sqrt(c.Radius * c.Radius - x * x);

                    for (int y = -height; y < height; y++)
                    {
                        if (!ret.OutOfBounds(c.X + x, c.Y + y))
                            ret.SetPixel(c.X + x, c.Y + y, c.Color);
                    }
                }
            }

            return _imageOutdaptor(ret);
        }

        public IFilter Initialize()
        {
            (_maxDiff, _minRad, _maxRad) = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
