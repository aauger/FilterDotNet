using FilterDotNet.Interfaces;
using FilterDotNet.Filters;
using NET6ImageFilter.BasicWinformsConfigurators;
using NET6ImageFilter.SpecificConfigurators;
using FilterDotNet.Generators;
using FilterDotNet.Analyzers;
using FilterDotNet.LibraryConfigurators;
using static FastImageProvider.Injectables;


namespace NET6ImageFilter
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            List<IFilter> filters = new();
            List<IAnalyzer> analyzers = new();
            List<IGenerator> generators = new();

            // Filters
            FilterInitializer.AddFilters(filters);

            // Generators
            GeneratorInitializer.AddGenerators(generators);

            // Analyzers
            AnalyzerInitializer.AddAnalyzers(analyzers);

            Application.Run(new MainForm(filters, generators, analyzers));
        }
    }
}