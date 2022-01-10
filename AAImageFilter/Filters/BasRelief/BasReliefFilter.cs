using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using FilterDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Filters
{
    public class BasReliefFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<int> _pluginConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private int _basReliefHeight;
        private bool _ready = false;

        /* Properties */
        public string Name => "Bas Relief";

        public BasReliefFilter(IPluginConfigurator<int> pluginConfigurator, IEngine engine)
        {
            this._pluginConfigurator = pluginConfigurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width - this._basReliefHeight, (int x) => 
            {
                Parallel.For(0, input.Height - this._basReliefHeight, (int y) => 
                {
                    IColor fs = input.GetPixel(x, y);
                    IColor sn = input.GetPixel(x + this._basReliefHeight, y + this._basReliefHeight);
                    int red = MathUtils.RGBClamp(fs.R + (255 / 2) - sn.R);
                    int green = MathUtils.RGBClamp(fs.G + (255 / 2) - sn.G);
                    int blue = MathUtils.RGBClamp(fs.B + (255 / 2) - sn.B);

                    output.SetPixel(x, y, this._engine.CreateColor(red, green, blue, 255));
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._basReliefHeight = this._pluginConfigurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
