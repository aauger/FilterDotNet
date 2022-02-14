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
            filters.AddRange(new IFilter[]
            {
                new BasReliefFilter(new WinformIntConfigurator("Bas Relief", "Height:"), FiEngine),
                new ChromaticAberrationFilter(FiEngine),
                new CirclePaintingFilter(new WinformThreeIntConfigurator("Circle Painting", "Max Difference:", "Minimum Radius:", "Maximum Radius:"), FiEngine),
                new ColorMatrixFilter(new ColorMatrixConfigurator(), FiEngine),
                new ColorMaskingFilter(new WinformGetImageDialog(), FiEngine),
                new ConvolutionFilter(new WinformsConvolutionConfigurator(), FiEngine),
                new DifferenceFilter(new WinformsDifferenceConfigurator(), FiEngine),
                new DivideFilter(new WinformGetImageDialog(), FiEngine),
                new Epx2xFilter(FiEngine),
                new FloydSteinbergDitherFilter(new LambdaPluginConfigurator<List<IColor>>(() => new()
                {
                    FiEngine.CreateColor(0, 0, 0, 255), //black 
                    FiEngine.CreateColor(255, 0, 0, 255), //red
                    FiEngine.CreateColor(0, 255, 0, 255), //green
                    FiEngine.CreateColor(0, 0, 255, 255), //blue
                    FiEngine.CreateColor(255,255,255,255) //white
                }), FiEngine),
                new GlassFilter(new WinformIntConfigurator("Glass", "Maximum distance:"), FiEngine),
                new GreyscaleFilter(FiEngine),
                new InvertFilter(FiEngine),
                new LineDrawingFilter(new LineDrawingConfigurator(), FiEngine),
                new MeltingFilter(FiEngine),
                new MultiplyFilter(new WinformGetImageDialog(), FiEngine),
                new NormalMap(new WinformsNormalMapConfigurator(), FiEngine),
                new OverlayFilter(new WinformGetImageDialog(), FiEngine),
                new PaletteSwap(new WinformGetImageDialog(), FiEngine),
                new PatchMatchFilter(new ImageTwoIntConfigurator(), FiEngine),
                new PosterizeFilter(new WinformIntConfigurator("Posterize", "Levels:"), FiEngine),
                new PixelateFilter(new WinformTwoIntConfigurator("Pixelate", "Block width:", "Block height:"), FiEngine),
                new ReversibleSplittingFilter(FiEngine),
                new ScreenFilter(new WinformGetImageDialog(), FiEngine),
                new SobelFilter(FiEngine),
                new SolarizeFilter(new WinformIntConfigurator("Solarize", "Threshold:"), FiEngine),
                new SortPixelsFilter(FiEngine),
                new StatisticalFilter(new WinformsStatisticalConfigurator(), FiEngine),
                new ThresholdFilter(new WinformIntConfigurator("Threshold", "Threshold:"), FiEngine),
                new VoronoiSketchFilter(new WinformIntConfigurator("Voronoi Sketch", "Number of nodes:"), FiEngine)
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