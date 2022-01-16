using FilterDotNet.Interfaces;
using FilterDotNet.Utils;
using FastImageLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter.ImageProviders
{
    public class FIDrawingColor : IColor
    {
        public int R { get => _color.GetR(); set => _color.SetR(value); }
        public int G { get => _color.GetG(); set => _color.SetG(value); }
        public int B { get => _color.GetB(); set => _color.SetB(value); }
        public int A { get => _color.GetA(); set => _color.SetA(value); }

        private FastImageColor _color;

        public FIDrawingColor(FastImageColor f)
        {
            _color = f;
        }

        public FIDrawingColor(int r, int g, int b, int a)
        {
            _color = new FastImageColor(r, g, b, a);

        }
    }
}
