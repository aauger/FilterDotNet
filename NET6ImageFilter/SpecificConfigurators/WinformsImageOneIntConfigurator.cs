using FilterDotNet.Interfaces;
using NET6ImageFilter.BasicWinformsConfigurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter.SpecificConfigurators
{
    public class WinformsImageOneIntConfigurator : IPluginConfigurator<(IImage, int)>
    {
        public (IImage, int) GetPluginConfiguration()
        {
            IImage image = new WinformsGetImageConfigurator().GetPluginConfiguration();
            int value = new WinformsOneIntConfigurator("Configuration", "Value:").GetPluginConfiguration();
            return (image, value);
        }
    }
}