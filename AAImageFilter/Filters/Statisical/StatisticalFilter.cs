using AAImageFilter.Exceptions;
using AAImageFilter.Extensions;
using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Filters.Statisical
{
    public enum StatisticalFilterMode { 
        MIN,
        MAX,
        MEDIAN,
        MODE,
        AVERAGE
    }
 
    public class StatisticalFilterConfiguration {
        public StatisticalFilterMode Mode { get; set; } = StatisticalFilterMode.AVERAGE;
        public bool Thresholding { get; set; } = false;
        public int Threshold { get; set; } = 0;
        public int BlockSize { get; set; } = 0;
    }

    public class StatisticalFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<StatisticalFilterConfiguration> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private StatisticalFilterConfiguration? _configuration;
        private bool _ready = false;

        /* Properties */
        public string Name => "Statistical (Min, Max, Median, Mode, ...)";

        public StatisticalFilter(IPluginConfigurator<StatisticalFilterConfiguration> pluginConfigurator, 
            Func<int,int,IImage> imageCreator,
            Func<int,int,int,int,IColor> colorCreator) 
        { 
            this._pluginConfigurator = pluginConfigurator;
            this._imageCreator = imageCreator;
            this._colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            var cfg = _configuration!;

            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    List<int> reds = new List<int>();
                    List<int> greens = new List<int>();
                    List<int> blues = new List<int>();
                    IColor home = input.GetPixel(x, y);

                    foreach (int xOff in Enumerable.Range(-cfg.BlockSize / 2, cfg.BlockSize))
                    {
                        foreach (int yOff in Enumerable.Range(-cfg.BlockSize / 2, cfg.BlockSize))
                        {
                            if (input.OutOfBounds(x + xOff, y + yOff))
                                continue;

                            IColor c = input.GetPixel(x + xOff, y + yOff);

                            if (cfg.Thresholding && home.Difference(c) > cfg.Threshold)
                                continue;

                            reds.Add(c.R);
                            greens.Add(c.G);
                            blues.Add(c.B);
                        }
                    }

                    reds.Sort();
                    greens.Sort();
                    blues.Sort();

                    int ri = 0, gi = 0, bi = 0;
                    switch (cfg.Mode)
                    {
                        case StatisticalFilterMode.AVERAGE:
                            ri = (int)reds.Average();
                            gi = (int)greens.Average();
                            bi = (int)blues.Average();
                            break;
                        case StatisticalFilterMode.MAX:
                            ri = reds.Max();
                            gi = greens.Max();
                            bi = blues.Max();
                            break;
                        case StatisticalFilterMode.MIN:
                            ri = reds.Min();
                            gi = reds.Min();
                            bi = greens.Min();
                            break;
                        case StatisticalFilterMode.MEDIAN:
                            ri = Median(reds);
                            gi = Median(greens);
                            bi = Median(blues);
                            break;
                        case StatisticalFilterMode.MODE:
                            ri = Mode(reds);
                            gi = Mode(greens);
                            bi = Mode(blues);
                            break;
                    }
                }
            }

            throw new NotImplementedException();
        }

        private int Median(List<int> xs)
        {
            var c = xs.Count;
            if (c % 2 == 0)
                return (xs[c] + xs[c + 1]) / 2;
            return xs[c];
        }

        private int Mode(List<int> xs)
        {
            Dictionary<int, int> counts = new();
            foreach (int x in xs)
            {
                if (!counts.ContainsKey(x))
                    counts[x] = 0;
                else
                    counts[x] += 1;
            }
            return counts.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }

        public IFilter Initialize()
        {
            this._configuration = _pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
