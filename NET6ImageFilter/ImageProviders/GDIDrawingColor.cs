using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6ImageFilter.ImageProviders
{
    internal class GDIDrawingColor : IColor
    {
        private Color _color;

        public int R { get => _color.R; set => _color = Color.FromArgb(this.A, value, this.G, this.B); }
        public int G { get => _color.G; set => _color = Color.FromArgb(this.A, this.R, value, this.B); }
        public int B { get => _color.B; set => _color = Color.FromArgb(this.A, this.R, this.G, value); }
        public int A { get => _color.A; set => _color = Color.FromArgb(value, this.R, this.G, this.B); }

        public GDIDrawingColor()
        { }

        public GDIDrawingColor(int R, int G, int B, int A)
        {
            _color = Color.FromArgb(A, R, G, B);
        }
    }
}
