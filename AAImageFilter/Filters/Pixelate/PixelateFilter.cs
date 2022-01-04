﻿using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;

namespace AAImageFilter.Filters
{
    public class PixelateFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(int, int)> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;

        /* Internals */
        private int _width = 0;
        private int _height = 0;
        private bool _ready = false;

        /* Properties */
        public string Name => "Pixelate";

        public PixelateFilter(IPluginConfigurator<(int, int)> pluginConfigurator, Func<int,int,IImage> imageCreator)
        { 
            this._pluginConfigurator = pluginConfigurator;
            this._imageCreator = imageCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._imageCreator(input.Width, input.Height);

            Parallel.For(0, input.Width / this._width, (int xMu) => {
                Parallel.For(0, input.Height / this._height, (int yMu) => {
                    int x = xMu * this._width;
                    int y = yMu * this._height;

                    IColor r = input.GetPixel(x, y);

                    for (int xi = x; xi < (x + this._width) && xi < input.Width; xi++)
                    {
                        for (int yi = y; yi < (y + this._height) && yi < input.Height; yi++)
                        {
                            output.SetPixel(xi, yi, r);
                        }
                    }
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            (this._width, this._height) = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
