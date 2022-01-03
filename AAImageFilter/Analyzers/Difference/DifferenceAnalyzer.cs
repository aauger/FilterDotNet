using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Analyzers
{
    public class DifferenceAnalyzer : IAnalyzer, IConfigurableAnalyzer
    {
        /* DI */
        private IAnalyzerConfigurator<IImage>? _analyzerConfigurator;

        /* Internals */
        private IImage? _other;
        private bool _ready = false;

        /* Properties */
        public string Name => "Difference";

        public DifferenceAnalyzer(IAnalyzerConfigurator<IImage> analyzerConfigurator)
        {
            this._analyzerConfigurator = analyzerConfigurator;
        }

        public string Analyze(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            int pxCount = input.Width * input.Height;
            double[,] lDiffs = new double[input.Width, input.Height];
            double totalDiffs = 0.0;

            Parallel.For(0, input.Width, (int x) => 
            {
                Parallel.For(0, input.Height, (int y) => 
                { 
                    IColor here = input.GetPixel(x, y);
                    IColor there = this._other!.GetPixel(x, y);

                    double rDiff = Math.Abs(here.R - there.R);
                    double gDiff = Math.Abs(here.G - there.G);
                    double bDiff = Math.Abs(here.B - there.B);
                    double lDiffPct = (rDiff + gDiff + bDiff) / (255 * 3);
                    lDiffs[x, y] = lDiffPct;
                });
            });

            foreach (double d in lDiffs) totalDiffs += d;

            double finalDiff = (totalDiffs / pxCount) * 100.0;

            return $"The difference between the source image and selected image was {finalDiff}%";
        }

        public IAnalyzer Initialize()
        {
            this._other = _analyzerConfigurator!.GetAnalyzerConfiguration();
            this._ready = true;
            return this;
        }
    }
}
