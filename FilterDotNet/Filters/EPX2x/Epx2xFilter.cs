using FilterDotNet.Extensions;
using FilterDotNet.Interfaces;
using FilterDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDotNet.Filters
{
    public class Epx2xFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Scaler: EPX 2x";

        public Epx2xFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width * 2, input.Height * 2);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    int ox = x * 2;
                    int oy = y * 2;

                    IColor origin = input.GetPixel(x, y);
                    IColor top    = input.GetPixel(x, MathUtils.Clamp(y - 1, 0, input.Height - 1));
                    IColor left   = input.GetPixel(MathUtils.Clamp(x - 1, 0, input.Width - 1), y);
                    IColor right  = input.GetPixel(MathUtils.Clamp(x + 1, 0, input.Width - 1), y);
                    IColor bottom = input.GetPixel(x, MathUtils.Clamp(y + 1, 0, input.Height - 1));

                    if (SameRegion(new[] { top, left, bottom, right }) > 2)
                    {
                        top    = origin;
                        left   = origin;
                        bottom = origin;
                        right  = origin;
                    }

                    output.SetPixel(ox, oy, top.Equivalent(left) ? top : origin);
                    output.SetPixel(ox + 1, oy, top.Equivalent(right) ? top : origin);
                    output.SetPixel(ox, oy + 1, bottom.Equivalent(left) ? bottom : origin);
                    output.SetPixel(ox + 1, oy + 1, bottom.Equivalent(right) ? bottom : origin);

                });
            });

            return output;
        }

        public static int SameRegion(IEnumerable<IColor> colorList) 
        {
            int same = 0;
            for (int x = 0; x < colorList.Count(); x++)
            {
                for (int y = x + 1; y < colorList.Count(); y++)
                {
                    IColor first = colorList.At(x);
                    IColor second = colorList.At(y);

                    if (first.Equivalent(second))
                        same++;
                }
            }
            return same;
        }

        

    }
}
