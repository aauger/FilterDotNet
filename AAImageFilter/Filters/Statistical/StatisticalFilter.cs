using AAImageFilter.Exceptions;
using AAImageFilter.Extensions;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
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
            if (!this._ready)
                throw new NotReadyException();

            var cfg = this._configuration!;
            IImage output = this._imageCreator(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    List<int> reds = new();
                    List<int> greens = new();
                    List<int> blues = new();
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
                            gi = greens.Min();
                            bi = blues.Min();
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

                    output.SetPixel(x, y, this._colorCreator(ri, gi, bi, 255));
                });
            });

            return output;
        }

        private static int Median(List<int> xs)
        {
            var c = xs.Count;
            var cm = c % 2;
            if (cm == 0 && c >= 2)
                return (xs[cm] + xs[cm + 1]) / 2;
            return xs[cm];
        }

        private static int Mode(List<int> xs)
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
            this._configuration = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
