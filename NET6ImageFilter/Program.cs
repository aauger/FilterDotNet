using AAImageFilter;
using AAImageFilter.Interfaces;
using AAImageFilter.Filters;
using NET6ImageFilter.BasicWinformsConfigurators;
using AAImageFilter.Generators;
using NET6ImageFilter.ImageProviders;

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
            static IImage gdiImageCreator(int x, int y) => new GDIDrawingImage(x, y);
            static IColor gdiColorCreator(int r, int g, int b, int a) => new GDIDrawingColor(r, g, b, a);


            List<IFilter> filters = new() 
            {
                new GlassFilter(new WinformIntConfigurator("Maximum distance:"), gdiImageCreator),
                new InvertFilter(gdiColorCreator),
                new PosterizeFilter(new WinformIntConfigurator("Levels:"), gdiImageCreator, gdiColorCreator),
                new PixelateFilter(new WinformTwoIntConfigurator("Block width:", "Block height:")),
                //new SolarizeFilter(new WinformIntConfigurator("Solarize threshold:")),
                new ThresholdFilter(new WinformIntConfigurator("Threshold:"), gdiColorCreator),
                //new CirclePaintingFilter(new WinformThreeIntConfigurator())
            };
            List<IGenerator> generators = new()
            {
                new XyModGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator()),
                new MandelbrotGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator()),
            };
            Application.Run(new MainForm(filters, generators));
        }
    }
}