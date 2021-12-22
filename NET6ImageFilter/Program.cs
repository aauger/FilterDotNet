using AAImageFilter;
using AAImageFilter.Interfaces;
using AAImageFilter.Filters;
using NET6ImageFilter.BasicWinformsConfigurators;
using NET6ImageFilter.SpecificConfigurators;
using AAImageFilter.Generators;
using AAImageFilter.Analyzers;
using NET6ImageFilter.ImageProviders;
using AAImageFilter.Common;
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
                new ChromaticAberrationFilter(FiImageCreator, FiColorCreator),
                new CirclePaintingFilter(new WinformThreeIntConfigurator(), FiImageCreator, FiColorCreator),
                new ColorOverlayFilter(new WinformGetImageDialog(), FiImageCreator, FiColorCreator),
                new ConvolutionFilter(new WinformsConvolutionConfigurator(), FiImageCreator, FiColorCreator),
                new DifferenceFilter(new WinformsDifferenceConfigurator(), FiImageCreator, FiColorCreator),
                new GlassFilter(new WinformIntConfigurator("Maximum distance:"), FiImageCreator),
                new GreyscaleFilter(FiColorCreator),
                new InvertFilter(FiColorCreator),
                new NormalMap(new WinformsNormalMapConfigurator(), FiImageCreator, FiColorCreator),
                new PosterizeFilter(new WinformIntConfigurator("Levels:"), FiImageCreator, FiColorCreator),
                new PixelateFilter(new WinformTwoIntConfigurator("Block width:", "Block height:")),
                new SolarizeFilter(new WinformIntConfigurator("Solarize threshold:"), FiColorCreator),
                new StatisticalFilter(new WinformsStatisticalConfigurator(), FiImageCreator, FiColorCreator),
                new ThresholdFilter(new WinformIntConfigurator("Threshold:"), FiColorCreator),
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