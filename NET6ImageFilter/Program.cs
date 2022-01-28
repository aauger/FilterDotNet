using FilterDotNet.Interfaces;
using FilterDotNet.Filters;
using NET6ImageFilter.BasicWinformsConfigurators;
using NET6ImageFilter.SpecificConfigurators;
using FilterDotNet.Generators;
using FilterDotNet.Analyzers;
using static NET6ImageFilter.Injectables;
using FilterDotNet.LibraryConfigurators;

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
            filters.AddRange(new IFilter[]
            {
                new BasReliefFilter(new WinformIntConfigurator("Height:"), FiEngine),
                new ChromaticAberrationFilter(FiEngine),
                new CirclePaintingFilter(new WinformThreeIntConfigurator(), FiEngine),
                new ClippingRectangleFilter(new WinformsClippingRectConfigurator(filters), FiEngine),
                new ColorOverlayFilter(new WinformGetImageDialog(), FiEngine),
                new ConvolutionFilter(new WinformsConvolutionConfigurator(), FiEngine),
                new DifferenceFilter(new WinformsDifferenceConfigurator(), FiEngine),
                new Epx2xFilter(FiEngine),
                new FloydSteinbergDitherFilter(new LambdaPluginConfigurator<List<IColor>>(() => new()
                {
                    FiEngine.CreateColor(0, 0, 0, 255), //black 
                    FiEngine.CreateColor(255, 0, 0, 255), //red
                    FiEngine.CreateColor(0, 255, 0, 255), //green
                    FiEngine.CreateColor(0, 0, 255, 255), //blue
                    FiEngine.CreateColor(255,255,255,255) //white
                }), FiEngine),
                new GlassFilter(new WinformIntConfigurator("Maximum distance:"), FiEngine),
                new GreyscaleFilter(FiEngine),
                new InvertFilter(FiEngine),
                new LineDrawingFilter(new LineDrawingConfigurator(), FiEngine),
                new MeltingFilter(FiEngine),
                new NormalMap(new WinformsNormalMapConfigurator(), FiEngine),
                new PaletteSwap(new WinformGetImageDialog(), FiEngine),
                new PatchMatchFilter(new ImageTwoIntConfigurator(), FiEngine),
                new PosterizeFilter(new WinformIntConfigurator("Levels:"), FiEngine),
                new PixelateFilter(new WinformTwoIntConfigurator("Block width:", "Block height:"), FiEngine),
                new ReversibleSplittingFilter(FiEngine),
                new SolarizeFilter(new WinformIntConfigurator("Solarize threshold:"), FiEngine),
                new SobelFilter(FiEngine),
                new StatisticalFilter(new WinformsStatisticalConfigurator(), FiEngine),
                new ThresholdFilter(new WinformIntConfigurator("Threshold:"), FiEngine),
                new VoronoiSketchFilter(new WinformIntConfigurator("Number of nodes:"), FiEngine)
            });

            // Generators
            generators.AddRange(new IGenerator[]
            {
                new LineDrawTestGenerator(FiEngine),
                new MandelbrotGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
                new PerlinNoiseGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
                new XyModGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), FiEngine),
            });

            // Analyzers
            analyzers.AddRange(new IAnalyzer[]
            {
                new DifferenceAnalyzer(new WinformGetImageDialog(), FiEngine)
            });

            Application.Run(new MainForm(filters, generators, analyzers));
        }
    }
}