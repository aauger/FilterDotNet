using AAImageFilter;
using AAImageFilter.Interfaces;
using AAImageFilter.Filters;
using NET6ImageFilter.BasicWinformsConfigurators;
using AAImageFilter.Generators;

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
                new GlassFilter(new WinformIntConfigurator("Maximum distance:")),
                new InvertFilter(),
                new PosterizeFilter(new WinformIntConfigurator("Levels:")),
                new PixelateFilter(new WinformTwoIntConfigurator("Block width:", "Block height:")),
                new SolarizeFilter(new WinformIntConfigurator("Solarize threshold:")),
                new ThresholdFilter(new WinformIntConfigurator("Threshold:")),
                new CirclePaintingFilter(new WinformThreeIntConfigurator())
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