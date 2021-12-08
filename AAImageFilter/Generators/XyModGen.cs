using AAImageFilter.Exceptions;
using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Generators
{
    public class XyModGen : IGenerator, IConfigurableGenerator
    {
        private readonly IGeneratorConfigurator<(int, int, int)> _generatorConfigurator;
        private bool _ready = false;
        private int _width = 0, _height = 0, _mod = 0;


        public XyModGen(IGeneratorConfigurator<(int, int, int)> generatorConfigurator)
        { 
            this._generatorConfigurator = generatorConfigurator;
        }

        public Image Generate()
        {
            if (!_ready)
                throw new NotReadyException();

            Bitmap bmp = new Bitmap(this._width, this._height);

            for (int x = 0; x < this._width; x++)
            {
                for (int y = 0; y < this._height; y++)
                {
                    if ((x ^ y) % _mod != 0)
                    {
                        bmp.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                }
            }

            return bmp;
        }

        public string GetName()
        {
            return "XY Mod Generator";
        }

        public IGenerator Initialize()
        {
            (_width, _height, _mod) = _generatorConfigurator.GetGeneratorConfiguration();
            _ready = true;
            return this;
        }
    }
}
