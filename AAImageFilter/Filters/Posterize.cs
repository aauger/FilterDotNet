using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Interfaces;
using AAImageFilter.Exceptions;
using System.Drawing;

namespace AAImageFilter.Filters
{
    public class Posterize : IFilter, IConfigurableFilter
    {
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private int _levels;
        private bool _ready = false;

        public Posterize(IPluginConfigurator<int> pluginConfigurator)
        { 
            this._pluginConfigurator = pluginConfigurator;
        }

        public Image Apply(Image input)
        {
            if (!_ready)
                throw new NotReadyException();

            Bitmap b = (Bitmap)input;
            Bitmap ret = new Bitmap(b.Width, b.Height);
            int bs = 255 / _levels;

            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    int nr, nb, ng;
                    Color c = b.GetPixel(x, y);
                    // ((i / n) * n) will get the nearest n to i
                    nr = (int)(Math.Round(c.R / (float)bs) * bs);
                    ng = (int)(Math.Round(c.G / (float)bs) * bs);
                    nb = (int)(Math.Round(c.B / (float)bs) * bs);
                    ret.SetPixel(x, y, Color.FromArgb(nr, ng, nb));
                }
            }

            return ret;
        }

        public string GetFilterName()
        {
            return "Posterize";
        }

        public IFilter Initialize()
        {
            this._levels = _pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
