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
            generators.AddRange(new IGenerator[]
            {
                new CircleFillTestGenerator(FiEngine),
                new LineDrawTestGenerator(FiEngine),
                new MandelbrotGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
                new PerlinNoiseGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
                new TriangleDrawTestGenerator(FiEngine),
                new TriangleFillTestGenerator(FiEngine),
                new XyModGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
            });

            // Analyzers
            analyzers.AddRange(new IAnalyzer[]
            {
                new DifferenceAnalyzer(new WinformsGetImageConfigurator(), FiEngine)
            });

            Application.Run(new MainForm(filters, generators, analyzers));
        }
    }
}