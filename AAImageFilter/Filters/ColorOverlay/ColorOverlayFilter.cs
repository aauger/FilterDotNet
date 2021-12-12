using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Filters
{
    public class ColorOverlayFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<IImage> _pluginConfigurator;
        private Func<int, int, IImage> _imageCreator;
        private Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private IImage? _mask;
        private bool _ready = false;

        /* Properties */
        public string Name => "Color Overlay";

        public ColorOverlayFilter(IPluginConfigurator<IImage> pluginConfigurator, Func<int, int, IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        {
            _pluginConfigurator = pluginConfigurator;
            _imageCreator = imageCreator;
            _colorCreator = colorCreator;
        }


        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            IImage output = _imageCreator(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    IColor m = _mask.GetPixel(x, y);
                    bool isWhite =
                        m.R == 255 && m.G == 255 && m.B == 255;
                    if (!isWhite)
                        output.SetPixel(x, y, input.GetPixel(x, y));
                    else
                        output.SetPixel(x, y, _colorCreator(0, 0, 0, 255));            
                }
            }

            return output;
        }

        public IFilter Initialize()
        {
            _mask = _pluginConfigurator.GetPluginConfiguration();
            _ready = true;
            return this;
        }
    }
}
