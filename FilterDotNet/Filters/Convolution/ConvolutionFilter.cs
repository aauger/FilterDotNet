using FilterDotNet.Exceptions;
using FilterDotNet.Extensions;
using FilterDotNet.Interfaces;
using FilterDotNet.Utils;

namespace FilterDotNet.Filters
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
        private readonly IEngine _engine;

        /* Internals */
        private ConvolutionConfiguration? _configuration;
        private bool _ready = false;

        /* Properties */
        public string Name => "Convolution";

        public ConvolutionFilter(IPluginConfigurator<ConvolutionConfiguration> pluginConfigurator, IEngine engine)
        { 
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            ConvolutionConfiguration cfg = this._configuration!;
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            IEnumerable<int> xVals = Enumerable.Range(-cfg.Values.GetLength(0) / 2, cfg.Values.GetLength(0));
            IEnumerable<int> yVals = Enumerable.Range(-cfg.Values.GetLength(1) / 2, cfg.Values.GetLength(1));

            double sumCoeff = 0;
            foreach (double d in cfg.Values) sumCoeff += d;
            double sumCoeffRGBMax = sumCoeff * (double)this._engine.MaxValue;

            Parallel.For(0, input.Width, (int x) => {
                Parallel.For(0, input.Height, (int y) => {
                    double r = 0;
                    double g = 0;
                    double b = 0;

                    for(int i = 0; i < xVals.Count(); i++)
                    {
                        for (int j = 0; j < yVals.Count(); j++)
                        {
                            int xVal = xVals.At(i);
                            int yVal = yVals.At(j);

                            if (input.OutOfBounds(x + xVal, y + yVal))
                                continue;

                            IColor c = input.GetPixel(x + xVal, y + yVal);

                            r += c.R * (cfg.Values[i, j] * cfg.Bias);
                            g += c.G * (cfg.Values[i, j] * cfg.Bias);
                            b += c.B * (cfg.Values[i, j] * cfg.Bias);
                        }
                    }

                    int ri, gi, bi;
                    if (cfg.Normalize)
                    {
                        ri = MathUtils.Clamp(this._engine.MinValue, 
                            this._engine.MaxValue, 
                            (int)MathUtils.Map(r, 0, sumCoeffRGBMax, 0, this._engine.MaxValue));
                        gi = MathUtils.Clamp(this._engine.MinValue, 
                            this._engine.MaxValue, 
                            (int)MathUtils.Map(g, 0, sumCoeffRGBMax, 0, this._engine.MaxValue));
                        bi = MathUtils.Clamp(this._engine.MinValue, 
                            this._engine.MaxValue, 
                            (int)MathUtils.Map(b, 0, sumCoeffRGBMax, 0, this._engine.MaxValue));
                    }
                    else 
                    { 
                        ri = MathUtils.Clamp(this._engine.MinValue, 
                            this._engine.MaxValue, 
                            (int)r);
                        gi = MathUtils.Clamp(this._engine.MinValue, 
                            this._engine.MaxValue, 
                            (int)g);
                        bi = MathUtils.Clamp(this._engine.MinValue, 
                            this._engine.MaxValue, 
                            (int)b);
                    }
                    
                    output.SetPixel(x, y, this._engine.CreateColor(ri, gi, bi, this._engine.MaxValue));
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._configuration = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
