using FilterDotNet.Interfaces;

namespace FilterDotNet.Filters
{
    public class MirrorHorizontalFilter : IFilter
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Mirror Horizontal";

        public MirrorHorizontalFilter(IEngine engine)
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
                    IColor opposite = input.GetPixel(input.Width - 1 - x, y);
                    output.SetPixel(x, y, opposite);
                });
            });

            return output;
        }
    }
}
