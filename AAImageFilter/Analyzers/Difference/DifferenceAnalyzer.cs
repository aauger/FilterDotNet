using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Analyzers
{
    public class DifferenceAnalyzer : IAnalyzer, IConfigurableAnalyzer
    {
        /* DI */
        private readonly IAnalyzerConfigurator<IImage>? _analyzerConfigurator;

        /* Internals */
        private IImage? _other;
        private bool _ready = false;

        /* Properties */
        public string Name => "Difference";

        public string Analyze(IImage input)
        {
            int pxCount = input.Width * input.Height;
            double invFrac = Math.Pow(pxCount, -1);

            Parallel.For(0, input.Width, (int x) => 
            {
                Parallel.For(0, input.Height, (int y) => 
                { 
                    IColor here = input.GetPixel(x, y);
                    IColor there = _other!.GetPixel(x, y);


                });
            });

            return "";
        }

        public IAnalyzer Initialize()
        {
            _other = _analyzerConfigurator!.GetAnalyzerConfiguration();
            _ready = true;
            return this;
        }
    }
}
