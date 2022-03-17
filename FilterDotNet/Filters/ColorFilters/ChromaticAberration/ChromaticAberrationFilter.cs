using FilterDotNet.Interfaces;
using FilterDotNet.Utils;

namespace FilterDotNet.Filters
{
    public class ChromaticAberrationFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Chromatic Aberration";

        public ChromaticAberrationFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);
            Parallel.For(2, input.Width - 2, x => {
                Parallel.For(2, input.Height - 2, y => {
                    IColor here = input.GetPixel(x, y);
                    IColor upLeft = input.GetPixel(x - 2, y - 2);
                    IColor boRight = input.GetPixel(x + 2, y + 2);

                    int R = this._engine.Clamp((int)(here.R * .25 + upLeft.R * .75));
                    int B = this._engine.Clamp((int)(here.B * .25 + boRight.B * .75));

                    output.SetPixel(x, y, this._engine.CreateColor(R, here.G, B, this._engine.MaxValue));
                });
            });
            return output;
        }
    }
}
