using FilterDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterDotNet.Extensions;

namespace FilterDotNet.Filters
{
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
            double wh = (double)(input.Width * input.Height);

            Parallel.For(0, input.Width, (int x) => 
            {
                Parallel.For(0, input.Width, (int y) => 
                {
                    double dr = 0;
                    double dg = 0;
                    double db = 0;
                    for (int w = 0; w < input.Width; w++)
                    {
                        for (int z = 0; z < input.Height; z++)
                        {
                            double k = (double)x;
                            double l = (double)y;
                            double i = (double)w;
                            double j = (double)z;

                            IColor inputColor = input.GetPixel(x, y);
                            double idr = this._engine.ScaleValueToFractional(inputColor.R);
                            double idg = this._engine.ScaleValueToFractional(inputColor.G);
                            double idb = this._engine.ScaleValueToFractional(inputColor.B);

                            double exp = 2 * Math.PI * (((k*i)/(double)input.Width) + ((l*j)/(double)input.Height));
                            double baseFunc = Math.Pow(Math.E, exp);
                            dr += baseFunc * idr;
                            dg += baseFunc * idg;
                            db += baseFunc * idb;
                        }
                    }
                    dr /= wh;
                    dg /= wh;
                    db /= wh;
                    int nr = this._engine.ClampedScaledFromFractional(dr);
                    int ng = this._engine.ClampedScaledFromFractional(dg);
                    int nb = this._engine.ClampedScaledFromFractional(db);
                    output.SetPixel(x, y, this._engine.CreateColor(nr, ng, nb, this._engine.MaxValue));
                });
            });

            return output;
        }
    }
}
