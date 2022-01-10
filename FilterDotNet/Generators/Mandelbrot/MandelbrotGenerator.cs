using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using FilterDotNet.Utils;

namespace FilterDotNet.Generators
{
    public class MandelbrotGenerator : IGenerator, IConfigurableGenerator
    {
        /* DI */
        private readonly IGeneratorConfigurator<(int, int, int)> _generatorConfigurator;
        private readonly Func<int, int, IImage> _imageCreator;
        private readonly Func<int, int, int, int, IColor> _colorCreator;

        /* Internals */
        private bool _ready = false;
        private int _width = 0, _height = 0, _iters = 0;
        private readonly Random _random = new();

        /* Properties */
        public string Name => "Mandelbrot Set";

        public MandelbrotGenerator(IGeneratorConfigurator<(int, int, int)> generatorConfigurator, Func<int,int,IImage> imageCreator, Func<int,int,int,int,IColor> colorCreator)
        {
            this._generatorConfigurator = generatorConfigurator;
            this._imageCreator = imageCreator;
            this._colorCreator = colorCreator;
        }

        public IImage Generate()
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage image = this._imageCreator(this._width, this._height);

            List<IColor> colors = new List<IColor>();
            for (int i = 0; i <= this._iters; i++) colors.Add(this._colorCreator(
                     _random.Next(0, 256),
                     _random.Next(0, 256),
                     _random.Next(0, 256),
                     255
                 ));



            Parallel.For(0, this._width, (int x) =>
            {
                Parallel.For(0, this._height, (int y) =>
                {
                    double x0 = MathUtils.Map(x, 0, this._width, -2.00, 0.47);
                    double y0 = MathUtils.Map(y, 0, this._height, -1.12, 1.12);
                    double zx = 0.0;
                    double zy = 0.0;
                    int iter = 0;
                    for (; iter < this._iters && zx * zx + zy * zy <= 4.0; ++iter)
                    {
                        double xtemp = zx * zx - zy * zy + x0;
                        zy = 2.0 * zx * zy + y0;
                        zx = xtemp;
                    }

                    IColor px = colors[iter];
                    image.SetPixel(x, y, px);
                });
            });

            return image;
        }

        public IGenerator Initialize()
        {
            (this._width, this._height, this._iters) = this._generatorConfigurator.GetGeneratorConfiguration();
            this._ready = true;
            return this;
        }
    }
}
