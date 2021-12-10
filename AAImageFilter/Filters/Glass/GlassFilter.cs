using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;

namespace AAImageFilter.Filters
{
    public class GlassFilter : IFilter, IConfigurableFilter
    {
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private int _glassDistance;
        private bool _ready = false;

        public GlassFilter(IPluginConfigurator<int> pluginConfigurator, Func<int,int,IImage> imageCreator)
        { 
            this._pluginConfigurator = pluginConfigurator;
            this._imageCreator = imageCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            Random rnd = new Random();
            IImage ret = _imageCreator(input.Width, input.Height);

            for (int x = 0; x < ret.Width; x++)
            {
                for (int y = 0; y < ret.Height; y++)
                {
                    int x2 = MathUtils.Clamp(x + rnd.Next(-_glassDistance, _glassDistance), 0, input.Width - 1);
                    int y2 = MathUtils.Clamp(y + rnd.Next(-_glassDistance, _glassDistance), 0, input.Height - 1);
                    IColor here = ret.GetPixel(x, y);
                    IColor there = ret.GetPixel(x2, y2);

                    ret.SetPixel(x, y, there);
                    ret.SetPixel(x2, y2, here);
                }
            }

            return ret;
        }

        public string GetFilterName()
        {
            return "Glass";
        }

        public IFilter Initialize()
        {
            _glassDistance = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
