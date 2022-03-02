using FilterDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterDotNet.Extensions;
using System.Numerics;

namespace FilterDotNet.Filters
{
    class ComplexContainer
    {
        public Complex R { get; set; }
        public Complex G { get; set; }
        public Complex B { get; set; }
    }

    public class ForwardDftFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Forward Dft";

        public ForwardDftFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);
            double width = output.Width;
            double height = output.Height;
            double wh = width * height;
            Complex p = Complex.Sqrt(-1.0d);
            ComplexContainer[,] result = new ComplexContainer[output.Width,output.Height];
            Parallel.For(0, input.Width, (int x) => 
            {
                Parallel.For(0, input.Width, (int y) => 
                {
                    Complex dr = 0;
                    Complex dg = 0;
                    Complex db = 0;
                    for (int w = 0; w < input.Width; w++)
                    {
                        for (int z = 0; z < input.Height; z++)
                        {
                            double k = (double)x;
                            double l = (double)y;
                            double i = (double)w;
                            double j = (double)z;

                            IColor inputColor = input.GetPixel(w, z);
                            double idr = this._engine.ScaleValueToFractional(inputColor.R);
                            double idg = this._engine.ScaleValueToFractional(inputColor.G);
                            double idb = this._engine.ScaleValueToFractional(inputColor.B);


                            Complex exp = -p * 2 * Math.PI * (((k*i)/width) + ((l*j)/height));
                            Complex baseFunc = Complex.Pow(Math.E, exp);
                            Complex compDr = baseFunc * idr;
                            Complex compDg = baseFunc * idg;
                            Complex compDb = baseFunc * idb;
                            dr += compDr;
                            dg += compDg;
                            db += compDb;
                        }
                    }
                    result[x, y] = new ComplexContainer()
                    {
                        R = dr,
                        G = dg,
                        B = db,
                    };
                });
            });

            double magMax = double.MinValue;
            foreach (ComplexContainer c in result)
            {
                if (c.R.Magnitude > magMax)
                    magMax = c.R.Magnitude;
                if (c.G.Magnitude > magMax)
                    magMax = c.G.Magnitude;
                if (c.B.Magnitude > magMax)
                    magMax = c.B.Magnitude;
            }
            double scaleConstant = this._engine.MaxValue / Math.Log(1 + Math.Abs(magMax));
            for (int x = 0; x < output.Width; x++)
            {
                for (int y = 0; y < output.Height; y++)
                {
                    double nr = (scaleConstant * Math.Log(1 + Math.Abs(result[x, y].R.Magnitude)));
                    double ng = (scaleConstant * Math.Log(1 + Math.Abs(result[x, y].G.Magnitude)));
                    double nb = (scaleConstant * Math.Log(1 + Math.Abs(result[x, y].B.Magnitude)));
                    int nri = this._engine.Clamp((int)nr);
                    int ngi = this._engine.Clamp((int)ng);
                    int nbi = this._engine.Clamp((int)nb);
                    output.SetPixel(x, y, this._engine.CreateColor((int)nr, (int)ng, (int)nb, this._engine.MaxValue));
                }
            }

            return output;
        }
    }
}
