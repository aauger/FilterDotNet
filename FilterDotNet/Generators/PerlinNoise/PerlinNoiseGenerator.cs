using FilterDotNet.Exceptions;
using FilterDotNet.Interfaces;
using FilterDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Generators
{
    public class PerlinNoiseGenerator : IGenerator, IConfigurableGenerator
    {
        /* DI */
        private readonly IGeneratorConfigurator<(int, int, int)> _generatorConfigurator;
        private readonly IEngine _engine;

        /* Internals */
        bool _ready = false;
        int _width;
        int _height;
        int _size;

        /* Properties */
        public string Name => "Perlin Noise";

        public PerlinNoiseGenerator(IGeneratorConfigurator<(int, int, int)> configurator, IEngine engine)
        {
            this._generatorConfigurator = configurator;
            this._engine = engine;
        }

        public IImage Generate()
        {
            if (!this._ready)
                throw new NotReadyException();

            IImage output = this._engine.CreateImage(this._width, this._height);
            Matrix<double> noise = new Matrix<double>(this._width, this._height);
            GenerateNoise(noise);

            Parallel.For(0, this._width, (int x) =>
            {
                Parallel.For(0, this._height, (int y) =>
                {
                    double col = Turbulence(noise, (double)x, (double)y, (double)this._size);
                    int cval = (int)col;
                    IColor c = this._engine.CreateColor(cval, cval, (int)MathUtils.Map(col, 0, 255, 128, 255), 255);

                    output.SetPixel(x, y, c);
                });
            });

            return output;
        }

        private void GenerateNoise(Matrix<double> noise)
        {
            Random rnd = new Random();
            Parallel.For(0, noise.Width, (int x) => 
            {
                Parallel.For(0, noise.Height, (int y) => 
                {
                    noise.Set(x, y, rnd.NextDouble());
                });
            });
        }

        private double Turbulence(Matrix<double> noise, double x, double y, double size)
        {
            double value = 0.0, initialSize = size;

            for (; size >= 1; size /= 2.0)
            {
                value += SmoothNoise(noise, x / size, y / size) * size;
                size /= 2.0;
            }

            return 128 * value / initialSize;
        }

        private double SmoothNoise(Matrix<double> noise, double x, double y)
        {
            double fractX = x - (int)x;
            double fractY = y - (int)y;

            int x1 = ((int)x + noise.Width) % noise.Width;
            int y1 = ((int)y + noise.Height) % noise.Height;

            int x2 = (x1 + noise.Width - 1) % noise.Width;
            int y2 = (y1 + noise.Height - 1) % noise.Height;

            double value = 0.0;
            value += fractX * fractY * noise.Get(x1, y1);
            value += (1 - fractX) * fractY * noise.Get(x2, y1);
            value += fractX * (1 - fractY) * noise.Get(x1, y2);
            value += (1 - fractX) * (1 - fractY) * noise.Get(x2, y2);

            return value;
        }

        public IGenerator Initialize()
        {
            (this._width, this._height, this._size) = this._generatorConfigurator.GetGeneratorConfiguration();
            this._ready = true;
            return this;
        }
    }
}
