using FilterDotNet.Analyzers;
using FilterDotNet.Interfaces;
using NET6ImageFilter.BasicWinformsConfigurators;
using static FastImageProvider.Injectables;

namespace NET6ImageFilter
{
    public class AnalyzerInitializer
    {
        public static void AddAnalyzers(List<IAnalyzer> list)
        {
            list.AddRange(new IAnalyzer[]
            {
                new DifferenceAnalyzer(new WinformsGetImageConfigurator(), FiEngine)
            });
        }
    }
}
