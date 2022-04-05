using FilterDotNet.Interfaces;
using FilterDotNet.Exceptions;

namespace FilterDotNet.Filters
{
    public enum InversionChannel
    {
        R,
        G,
        B,
        None
    }

    public class InvertChannelFilter : IFilter, IConfigurableFilter
    {
        /* DI */
        private readonly IPluginConfigurator<InversionChannel> _configurator;
        private readonly IEngine _engine;

        /* internals */
        private bool _ready = false;
        private InversionChannel _channel = InversionChannel.None;

        /* Properties */
        public string Name => "Invert Channel";

        public InvertChannelFilter(IPluginConfigurator<InversionChannel> configurator, IEngine engine)
        {
            this._engine = engine;
            this._configurator = configurator;
        }

        public IImage Apply(IImage input)
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    IColor here = input.GetPixel(x, y);

                    IColor replacement = this._channel switch
                    {
                        InversionChannel.R => this._engine.CreateColor(this._engine.MaxValue - here.R, here.G, here.B, here.A),
                        InversionChannel.G => this._engine.CreateColor(here.R, this._engine.MaxValue - here.G, here.B, here.A),
                        InversionChannel.B => this._engine.CreateColor(here.R, here.G, this._engine.MaxValue - here.B, here.A),
                        InversionChannel.None => here,
                        _ => here,
                    };

                    output.SetPixel(x, y, replacement);
                });
            });

            return output;
        }

        public IFilter Initialize()
        {
            this._channel = this._configurator.GetPluginConfiguration();
            this._ready = true;
            return this;
        }
    }
}
