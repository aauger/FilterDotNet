using FilterDotNet.Interfaces;
using FastImageLibrary;
using NET6ImageFilter.ImageProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastImageProvider;

namespace NET6ImageFilter.BasicWinformsConfigurators
{
    public class WinformGetImageDialog : IPluginConfigurator<IImage>, IAnalyzerConfigurator<IImage>
    {
        public IImage GetAnalyzerConfiguration()
        {
            using OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var image = Image.FromFile(dialog.FileName);
                if (image != null)
                    return new FIDrawingImage(new FastImage(image));
            }
            return new FIDrawingImage(1, 1);
        }

        public IImage GetPluginConfiguration()
        {
            using OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            { 
                var image = Image.FromFile(dialog.FileName);
                if (image != null)
                    return new FIDrawingImage(new FastImage(image));
            }
            return new FIDrawingImage(1, 1);
        }
    }
}
