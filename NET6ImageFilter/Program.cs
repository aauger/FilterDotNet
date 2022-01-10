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
            FastImageEngine fie = new FastImageEngine();
            List<IFilter> filters = new()
            {
                new BasReliefFilter(new WinformIntConfigurator("Height:"), fie),
                new ChromaticAberrationFilter(fie),
                new CirclePaintingFilter(new WinformThreeIntConfigurator(), fie),
                new ColorOverlayFilter(new WinformGetImageDialog(), fie),
                new ConvolutionFilter(new WinformsConvolutionConfigurator(), fie),
                new DifferenceFilter(new WinformsDifferenceConfigurator(), fie),
                new GlassFilter(new WinformIntConfigurator("Maximum distance:"), fie),
                new GreyscaleFilter(fie),
                new InvertFilter(fie),
                new MeltingFilter(fie),
                new NormalMap(new WinformsNormalMapConfigurator(), fie),
                new PosterizeFilter(new WinformIntConfigurator("Levels:"), fie),
                new PixelateFilter(new WinformTwoIntConfigurator("Block width:", "Block height:"), fie),
                new SolarizeFilter(new WinformIntConfigurator("Solarize threshold:"), fie),
                new SobelFilter(fie),
                new StatisticalFilter(new WinformsStatisticalConfigurator(), fie),
                new ThresholdFilter(new WinformIntConfigurator("Threshold:"), fie),
            };
            List<IGenerator> generators = new()
            {
                new MandelbrotGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiImageCreator, FiColorCreator),
                new XyModGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiImageCreator, FiColorCreator),
            };
            List<IAnalyzer> analyzers = new()
            {
                new DifferenceAnalyzer(new WinformGetImageDialog())
            };
            Application.Run(new MainForm(filters, generators, analyzers));
        }
    }
}