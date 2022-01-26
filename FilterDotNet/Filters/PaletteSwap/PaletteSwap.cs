using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Filters
{
    public class PaletteSwap : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<IImage> _configurator;
        private readonly IEngine _engine;

        /* Internals */
        bool _ready = false;
        IImage _colorSource;

        /* Properties */
        public string Name => "Palette Swap";

        public PaletteSwap(IPluginConfigurator<IImage> configurator, IEngine engine)
        {
            this._configurator = configurator;
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            if ((input.Width * input.Height) != (this._colorSource.Width * this._colorSource.Height))
                throw new BadConfigurationException("Source & destination must have same number of pixels");

            List<Node> inputNodes = new();
            List<Node> sourceNodes = new();
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            //Collect input nodes
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    inputNodes.Add(new() { X = x, Y = y, Color = input.GetPixel(x, y) });
                }
            }

            //Collect source nodes
            for (int x = 0; x < this._colorSource.Width; x++)
            {
                for (int y = 0; y < this._colorSource.Height; y++)
                {
                    sourceNodes.Add(new() { X = x, Y = y, Color = this._colorSource.GetPixel(x, y) });
                }
            }

            //sort image nodes by radiance
            IEnumerable<Node> sortedInputNodes = inputNodes.OrderBy(px => Radiance(px.Color!));
            IEnumerable<Node> sortedSourceNodes = sourceNodes.OrderBy(px => Radiance(px.Color!));
            IEnumerable<(Node, Node)> zippedNodes = sortedInputNodes.Zip(sortedSourceNodes);

            foreach ((Node inNode, Node scNode) in zippedNodes)
            {
                output.SetPixel(inNode.X, inNode.Y, scNode.Color!);
            }

            return output;
        }

        private float Radiance(IColor color)
        {
            return color.R * 0.265f + color.G * 0.67f + color.B * 0.065f;
        }

        public IFilter Initialize()
        {
            this._colorSource = this._configurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
