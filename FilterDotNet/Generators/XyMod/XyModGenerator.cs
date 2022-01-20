using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Generators
{
    public class XyModGenerator : IGenerator, IConfigurableGenerator
    {
        /* DI */
        private readonly IGeneratorConfigurator<(int, int, int)> _generatorConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        private bool _ready = false;
        private int _width = 0, _height = 0, _mod = 0;

        /* Properties */
        public string Name => "XY Mod Generator";

        public XyModGenerator(IGeneratorConfigurator<(int, int, int)> generatorConfigurator, IEngine engine)
        {
            this._generatorConfigurator = generatorConfigurator;
            this._engine = engine;
        }

        public IImage Generate()
        {
            if (!this._ready)
                throw new NotReadyException();

            IColor black = this._engine.CreateColor(this._engine.MinValue, this._engine.MinValue, this._engine.MinValue, this._engine.MaxValue);
            IColor white = this._engine.CreateColor(this._engine.MaxValue, this._engine.MaxValue, this._engine.MaxValue, this._engine.MaxValue);

            IImage image = this._engine.CreateImage(this._width, this._height);

            Parallel.For(0, this._width, (int x) =>
            {
                Parallel.For(0, this._height, (int y) =>
                {
                    if ((x ^ y) % _mod != 0)
                    {
                        image.SetPixel(x, y, white);
                    }
                    else
                    {
                        image.SetPixel(x, y, black);
                    }
                });
            });

            return image;
        }

        public IGenerator Initialize()
        {
            (this._width, this._height, this._mod) = _generatorConfigurator.GetGeneratorConfiguration();
            this._ready = true;
            return this;
        }
    }
}
