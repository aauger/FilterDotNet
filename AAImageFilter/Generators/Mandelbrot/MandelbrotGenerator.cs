using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;
using AAImageFilter.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Generators
{
    public class MandelbrotGenerator : IGenerator, IConfigurableGenerator
    {
        private readonly IGeneratorConfigurator<(int, int, int)> _generatorConfigurator;
        private bool _ready = false;
        private int _width = 0, _height = 0, _iters = 0;
        private readonly Random _random = new Random();

        public MandelbrotGenerator(IGeneratorConfigurator<(int, int, int)> generatorConfigurator)
        {
            this._generatorConfigurator = generatorConfigurator;
        }

        public Image Generate()
        {
            if (!_ready)
                throw new NotReadyException();

            Bitmap bmp = new Bitmap(this._width, this._height);

            List<Color> colors = new List<Color>();
            for (int i = 0; i <= this._iters; i++) colors.Add(Color.FromArgb(
                     _random.Next(0, 256),
                     _random.Next(0, 256),
                     _random.Next(0, 256)
                 ));

            for (int x = 0; x < this._width; x++)
            {
                for (int y = 0; y < this._height; y++)
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

                    Color px = colors[iter];
                    bmp.SetPixel(x, y, px);
                }
            }

            return bmp;
        }

        public string GetName()
        {
            return "Mandelbrot Set";
        }

        public IGenerator Initialize()
        {
            (_width, _height, _iters) = _generatorConfigurator.GetGeneratorConfiguration();
            _ready = true;
            return this;
        }
    }
}
