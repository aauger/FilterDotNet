using AAImageFilter;
using AAImageFilter.Interfaces;
using AAImageFilter.Filters;
using NET6ImageFilter.BasicWinformsConfigurators;
using AAImageFilter.Generators;
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
                new CirclePaintingFilter(new WinformThreeIntConfigurator(), FastImageAdaptor, FastImageOutdaptor),
                new ColorOverlayFilter(new WinformGetImageDialog(), GdiImageCreator, GdiColorCreator),
                new GlassFilter(new WinformIntConfigurator("Maximum distance:"), GdiImageCreator),
                new GreyscaleFilter(GdiColorCreator),
                new InvertFilter(GdiColorCreator),
                new PosterizeFilter(new WinformIntConfigurator("Levels:"), GdiImageCreator, GdiColorCreator),
                new PixelateFilter(new WinformTwoIntConfigurator("Block width:", "Block height:")),
                new SolarizeFilter(new WinformIntConfigurator("Solarize threshold:"), GdiColorCreator),
                new ThresholdFilter(new WinformIntConfigurator("Threshold:"), GdiColorCreator),
            };
            List<IGenerator> generators = new()
            {
                new MandelbrotGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), GdiImageCreator, GdiColorCreator),
                new XyModGenerator(new WinformsGeneratorConfigurators.GeneratorThreeIntConfigurator(), GdiImageCreator, GdiColorCreator),
            };
            Application.Run(new MainForm(filters, generators));
        }
    }
}