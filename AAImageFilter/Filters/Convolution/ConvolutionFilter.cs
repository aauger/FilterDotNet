using AAImageFilter.Common;
using AAImageFilter.Exceptions;
using AAImageFilter.Extensions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;

namespace AAImageFilter.Filters
{
    public class ConvolutionConfiguration
    {
        public double[,] Values { get; set; } = new double[,] { };
        public double Bias { get; set; } = 0.0;
        public bool Normalize { get; set; } = true;
    }

    public class ConvolutionFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<ConvolutionConfiguration> _pluginConfigurator;
        private readonly Func<IImage, FastImage> _fastImageAdaptor;
        private readonly Func<FastImage, IImage> _fastImageOutdaptor;

        /* Internals */
        private ConvolutionConfiguration? _configuration;
        private bool _ready = false;

        /* Properties */
        public string Name => "Convolution";

        public ConvolutionFilter(IPluginConfigurator<ConvolutionConfiguration> pluginConfigurator, Func<IImage, FastImage> fastImageAdaptor, Func<FastImage, IImage> fastImageOutdaptor)
        { 
            _pluginConfigurator = pluginConfigurator;
            _fastImageAdaptor = fastImageAdaptor;
            _fastImageOutdaptor = fastImageOutdaptor;
        }

        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            ConvolutionConfiguration cfg = _configuration!;
            FastImage refn = _fastImageAdaptor(input);
            FastImage ret = new FastImage(refn.Width, refn.Height);

            IEnumerable<int> xVals = Enumerable.Range(-cfg.Values.GetLength(0) / 2, cfg.Values.GetLength(0));
            IEnumerable<int> yVals = Enumerable.Range(-cfg.Values.GetLength(1) / 2, cfg.Values.GetLength(1));

            double sumCoeff = 0;
            foreach (double d in cfg.Values) sumCoeff += d;
            double sumCoeffRGBMax = sumCoeff * 255.0;

            Parallel.For(0, refn.Width, x => {
                Parallel.For(0, refn.Height, y => {
                    double r = 0;
                    double g = 0;
                    double b = 0;

                    for(int i = 0; i < xVals.Count(); i++)
                    {
                        for (int j = 0; j < yVals.Count(); j++)
                        {
                            int xVal = xVals.At(i);
                            int yVal = yVals.At(j);

                            if (refn.OutOfBounds(x + xVal, y + yVal))
                                continue;

                            FastImageColor c = refn.GetPixel(x + xVal, y + yVal);

                            r += c.GetR() * (cfg.Values[i, j] * cfg.Bias);
                            g += c.GetG() * (cfg.Values[i, j] * cfg.Bias);
                            b += c.GetB() * (cfg.Values[i, j] * cfg.Bias);
                        }
                    }

                    int ri, gi, bi;
                    if (cfg.Normalize)
                    {
                        ri = MathUtils.RGBClamp((int)MathUtils.Map(r, 0, sumCoeffRGBMax, 0, 255));
                        gi = MathUtils.RGBClamp((int)MathUtils.Map(g, 0, sumCoeffRGBMax, 0, 255));
                        bi = MathUtils.RGBClamp((int)MathUtils.Map(b, 0, sumCoeffRGBMax, 0, 255));
                    }
                    else 
                    { 
                        ri = MathUtils.RGBClamp((int)r);
                        gi = MathUtils.RGBClamp((int)g);
                        bi = MathUtils.RGBClamp((int)b);                    
                    }
                    
                    ret.SetPixel(x, y, new FastImageColor(ri, gi, bi));
                });
            });

            return _fastImageOutdaptor(ret);
        }

        public IFilter Initialize()
        {
            _configuration = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
