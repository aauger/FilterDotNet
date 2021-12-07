using AAImageFilter;
using AAImageFilter.Interfaces;
using AAImageFilter.Filters;
using NET6ImageFilter.BasicWinformsConfigurators;

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
            List<IFilter> filters = new() {
                new Glass(new WinformIntConfigurator("Maximum distance:")),
                new Invert(),
                new Threshold(new WinformIntConfigurator("Threshold:")),
                new Solarize(new WinformIntConfigurator("Solarize threshold:")),
                new Posterize(new WinformIntConfigurator("Levels:")),
                new Pixelate(new WinformTwoIntConfigurator("Block width:", "Block height:"))
            };
            Application.Run(new MainForm(filters));
        }
    }
}