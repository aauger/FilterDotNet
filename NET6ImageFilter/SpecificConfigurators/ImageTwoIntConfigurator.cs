using FilterDotNet.Interfaces;
using NET6ImageFilter.BasicWinformsConfigurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter.SpecificConfigurators
{
    public class ImageTwoIntConfigurator : IPluginConfigurator<(IImage, int, int)>
    {
        public (IImage, int, int) GetPluginConfiguration()
        {
            IImage image = new WinformGetImageDialog().GetPluginConfiguration();
            (int first, int second) = new WinformTwoIntConfigurator("Configuration", "Width:", "Height:").GetPluginConfiguration();
            return (image, first, second);
        }
    }
}
