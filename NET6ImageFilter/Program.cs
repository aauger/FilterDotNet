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
                new AddFilter(new WinformsGetImageConfigurator(), FiEngine),
                new BasReliefFilter(new WinformsOneIntConfigurator("Bas Relief", "Height:"), FiEngine),
                new ChromaticAberrationFilter(FiEngine),
                new CirclePaintingFilter(new WinformsThreeIntConfigurator("Circle Painting", "Max Difference:", "Minimum Radius:", "Maximum Radius:"), FiEngine),
                new ColorBurnFilter(new WinformsGetImageConfigurator(), FiEngine),
                new ColorDodgeFilter(new WinformsGetImageConfigurator(), FiEngine),
                new ColorMatrixFilter(new ColorMatrixConfigurator(), FiEngine),
                new ColorMaskingFilter(new WinformsGetImageConfigurator(), FiEngine),
                new ConvolutionFilter(new WinformsConvolutionConfigurator(), FiEngine),
                new DarkenFilter(new WinformsGetImageConfigurator(), FiEngine),
                new DifferenceFilter(new WinformsDifferenceConfigurator(), FiEngine),
                new DivideFilter(new WinformsGetImageConfigurator(), FiEngine),
                new Epx2xFilter(FiEngine),
                new ExclusionFilter(new WinformsGetImageConfigurator(), FiEngine),
                new FixChromaticAberrationFilter(new WinformsOneIntConfigurator("Fix Chromatic Aberration", "Distance:"), FiEngine),
                new FloydSteinbergDitherFilter(new LambdaPluginConfigurator<List<IColor>>(() => new()
                {
                    FiEngine.CreateColor(0, 0, 0, 255), //black 
                    FiEngine.CreateColor(255, 0, 0, 255), //red
                    FiEngine.CreateColor(0, 255, 0, 255), //green
                    FiEngine.CreateColor(0, 0, 255, 255), //blue
                    FiEngine.CreateColor(255,255,255,255) //white
                }), FiEngine),
                new ForwardDftFilter(FiEngine),
                new GlassFilter(new WinformsOneIntConfigurator("Glass", "Maximum distance:"), FiEngine),
                new GreyscaleFilter(FiEngine),
                new InvertFilter(FiEngine),
                new LightenFilter(new WinformsGetImageConfigurator(), FiEngine),
                new LineDrawingFilter(new LineDrawingConfigurator(), FiEngine),
                new MeltingFilter(FiEngine),
                new MirrorHorizontalFilter(FiEngine),
                new MirrorVerticalFilter(FiEngine),
                new MultiplyFilter(new WinformsGetImageConfigurator(), FiEngine),
                new NormalMap(new WinformsNormalMapConfigurator(), FiEngine),
                new OverlayFilter(new WinformsGetImageConfigurator(), FiEngine),
                new PaletteSwap(new WinformsGetImageConfigurator(), FiEngine),
                new PatchMatchFilter(new ImageTwoIntConfigurator(), FiEngine),
                new PosterizeFilter(new WinformsOneIntConfigurator("Posterize", "Levels:"), FiEngine),
                new PixelateFilter(new WinformsTwoIntConfigurator("Pixelate", "Block width:", "Block height:"), FiEngine),
                new ReversibleSplittingFilter(FiEngine),
                new RotateNinetyCounterClockwiseFilter(FiEngine),
                new ScreenFilter(new WinformsGetImageConfigurator(), FiEngine),
                new SobelFilter(FiEngine),
                new SolarizeFilter(new WinformsOneIntConfigurator("Solarize", "Threshold:"), FiEngine),
                new SortPixelsFilter(FiEngine),
                new StatisticalFilter(new WinformsStatisticalConfigurator(), FiEngine),
                new ThresholdFilter(new WinformsOneIntConfigurator("Threshold", "Threshold:"), FiEngine),
                new VoronoiSketchFilter(new WinformsOneIntConfigurator("Voronoi Sketch", "Number of nodes:"), FiEngine)
            });

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