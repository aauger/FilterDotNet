﻿using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Filters
{
    public class NormalMap : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<(IImage, double)> _pluginConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private IImage? _map;
        private double _multiplier = 1.0;
        private bool _ready = false;

        /* Properties */
        public string Name => "Normal Map";

        public NormalMap(IPluginConfigurator<(IImage, double)> pluginConfigurator, Func<int, int, IImage> imageCreator, Func<int, int, int, int, IColor> colorCreator)
        {
            _pluginConfigurator = pluginConfigurator;
            _imageCreator = imageCreator;
            _colorCreator = colorCreator;
        }

        public IImage Apply(IImage input)
        {
            if (!_ready)
                throw new NotReadyException();

            IImage ret = _imageCreator(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) => {
                Parallel.For(0, input.Height, (int y) => 
                {
                    IColor mapColor = _map!.GetPixel(x, y);
                    double xm = MathUtils.Map(mapColor.R, 0, 255, -1, 1);
                    double ym = MathUtils.Map(mapColor.G, 0, 255, -1, -1);
                    double zm = MathUtils.Map(mapColor.B, 128, 255, 0, -1);
                    double lat = Math.Acos(zm);
                    double lon = Math.Atan2(ym, xm);

                    int xCalc = (int)Math.Round(x + ((xm) * _multiplier));
                    int xCalcWrapF = xCalc > input.Width - 1 ? xCalc - input.Width - 1 : xCalc;
                    int xCalcWrapS = xCalcWrapF < 0 ? -xCalcWrapF : xCalcWrapF;

                    int yCalc = (int)Math.Round(y + ((ym) * _multiplier));
                    int yCalcWrapF = yCalc > input.Height - 1 ? yCalc - input.Height - 1 : yCalc;
                    int yCalcWrapS = yCalcWrapF < 0 ? -yCalcWrapF : yCalcWrapF;

                    IColor transposedLocation = input.GetPixel(xCalcWrapS, yCalcWrapS);
                    ret.SetPixel(x, y, transposedLocation);
                });
            });

            return ret;
        }

        public IFilter Initialize()
        {
            (this._map, this._multiplier) = _pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
