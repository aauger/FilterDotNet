using FilterDotNet.Filters;
using FilterDotNet.Interfaces;
using FilterDotNet.LibraryConfigurators;
using NET6ImageFilter.BasicWinformsConfigurators;
using NET6ImageFilter.SpecificConfigurators;
using static FastImageProvider.Injectables;

namespace NET6ImageFilter
{
    public class FilterInitializer
    {
        public static void AddFilters(List<IFilter> list)
        {
            list.AddRange(
                new IFilter[]
                {
                    new AddFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new BasReliefFilter(new WinformsOneIntConfigurator("Bas Relief", "Height:"), FiEngine),
                    new BrightnessFilter(new WinformsOneIntConfigurator("Brightness", "Percent"), FiEngine),
                    new ChromaticAberrationFilter(FiEngine),
                    new CirclePaintingFilter(new WinformsThreeIntConfigurator("Circle Painting", "Max Difference:", "Minimum Radius:", "Maximum Radius:"), FiEngine),
                    new ColorBurnFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new ColorDodgeFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new ColorMatrixFilter(new WinformsColorMatrixConfigurator(), FiEngine),
                    new ColorMaskingFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new ConvolutionFilter(new WinformsConvolutionConfigurator(), FiEngine),
                    new DarkenFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new DifferenceFilter(new WinformsDifferenceConfigurator(), FiEngine),
                    new DivideFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new DissolveFilter(new WinformsImageOneIntConfigurator(), FiEngine),
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
                    new HardLightFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new InvertFilter(FiEngine),
                    new InvertChannelFilter(new WinformsChannelInversionConfigurator(), FiEngine),
                    new LightenFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new LineDrawingFilter(new WinformsLineDrawingConfigurator(), FiEngine),
                    new MeltingFilter(FiEngine),
                    new MirrorHorizontalFilter(FiEngine),
                    new MirrorVerticalFilter(FiEngine),
                    new MultiplyFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new NormalMap(new WinformsNormalMapConfigurator(), FiEngine),
                    new OverlayFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new PaletteSwap(new WinformsGetImageConfigurator(), FiEngine),
                    new PatchMatchFilter(new WinformsImageTwoIntConfigurator(), FiEngine),
                    new PosterizeFilter(new WinformsOneIntConfigurator("Posterize", "Levels:"), FiEngine),
                    new PixelateFilter(new WinformsTwoIntConfigurator("Pixelate", "Block width:", "Block height:"), FiEngine),
                    new ReversibleSplittingFilter(FiEngine),
                    new RotateChannelsFilter(FiEngine),
                    new RotateNinetyCounterClockwiseFilter(FiEngine),
                    new ScreenFilter(new WinformsGetImageConfigurator(), FiEngine),
                    new SobelFilter(FiEngine),
                    new SolarizeFilter(new WinformsOneIntConfigurator("Solarize", "Threshold:"), FiEngine),
                    new SortPixelsFilter(FiEngine),
                    new StatisticalFilter(new WinformsStatisticalConfigurator(), FiEngine),
                    new ThresholdFilter(new WinformsOneIntConfigurator("Threshold", "Threshold:"), FiEngine),
                    new VoronoiSketchFilter(new WinformsOneIntConfigurator("Voronoi Sketch", "Number of nodes:"), FiEngine)
                }
            );
        }
    }
}
