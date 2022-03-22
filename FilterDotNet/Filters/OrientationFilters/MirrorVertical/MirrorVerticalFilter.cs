using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class MirrorVerticalFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Mirror Vertical";

        public MirrorVerticalFilter(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Apply(IImage input)
        {
            IImage output = this._engine.CreateImage(input.Width, input.Height);

            Parallel.For(0, input.Width, (int x) =>
            {
                Parallel.For(0, input.Height, (int y) =>
                {
                    IColor opposite = input.GetPixel(x, input.Height - 1 - y);
                    output.SetPixel(x, y, opposite);
                });
            });

            return output;
        }
    }
}
