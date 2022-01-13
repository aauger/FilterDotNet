using FilterDotNet;
using FilterDotNet.Interfaces;
using FilterDotNet.Filters;
using NET6ImageFilter.BasicWinformsConfigurators;
using NET6ImageFilter.SpecificConfigurators;
using FilterDotNet.Generators;
using FilterDotNet.Analyzers;
using NET6ImageFilter.ImageProviders;
using static NET6ImageFilter.Injectables;

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
            List<IFilter> filters = new()
            {
                new BasReliefFilter(new WinformIntConfigurator("Height:"), FiEngine),
                new ChromaticAberrationFilter(FiEngine),
                new CirclePaintingFilter(new WinformThreeIntConfigurator(), FiEngine),
                new ColorOverlayFilter(new WinformGetImageDialog(), FiEngine),
                new ConvolutionFilter(new WinformsConvolutionConfigurator(), FiEngine),
                new DifferenceFilter(new WinformsDifferenceConfigurator(), FiEngine),
                new GlassFilter(new WinformIntConfigurator("Maximum distance:"), FiEngine),
                new GreyscaleFilter(FiEngine),
                new InvertFilter(FiEngine),
                new MeltingFilter(FiEngine),
                new NormalMap(new WinformsNormalMapConfigurator(), FiEngine),
                new PosterizeFilter(new WinformIntConfigurator("Levels:"), FiEngine),
                new PixelateFilter(new WinformTwoIntConfigurator("Block width:", "Block height:"), FiEngine),
                new SolarizeFilter(new WinformIntConfigurator("Solarize threshold:"), FiEngine),
                new SobelFilter(FiEngine),
                new StatisticalFilter(new WinformsStatisticalConfigurator(), FiEngine),
                new ThresholdFilter(new WinformIntConfigurator("Threshold:"), FiEngine),
                new VoronoiSketchFilter(new WinformIntConfigurator("Number of nodes:"), FiEngine)
            };
            List<IGenerator> generators = new()
            {
                new MandelbrotGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
                new PerlinNoiseGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
                new XyModGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
            };
            List<IAnalyzer> analyzers = new()
            {
                new DifferenceAnalyzer(new WinformGetImageDialog())
            };
            Application.Run(new MainForm(filters, generators, analyzers));
        }
    }
}