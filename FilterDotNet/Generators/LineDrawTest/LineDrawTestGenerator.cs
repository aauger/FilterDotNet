using FilterDotNet.Drawing;
using FilterDotNet.Interfaces;

namespace FilterDotNet.Generators
{
    public class LineDrawTestGenerator : IGenerator
    {
        /* DI */
        private readonly IEngine _engine;

        /* Properties */
        public string Name => "Line Drawing";

        public LineDrawTestGenerator(IEngine engine)
        {
            this._engine = engine;
        }

        public IImage Generate()
        {
            Random rnd = new Random();
            IImage output = this._engine.CreateImage(1920, 1080);
            Graphics g = Graphics.FromIImage(output);
            int count = rnd.Next(1500, 5000);
            for (int i = 0; i < count; i++)
            {
                Point first = new
                (
                    rnd.Next(0, output.Width),
                    rnd.Next(0, output.Height)
                );
                Point second = new
                (
                    rnd.Next(0, output.Width),
                    rnd.Next(0, output.Height)
                );
                g.DrawLine(first, second, RandomColor(rnd));
            }

            return output;
        }

        private IColor RandomColor(Random random)
        {
            int r = random.Next(this._engine.MinValue, this._engine.MaxValue + 1);
            int g = random.Next(this._engine.MinValue, this._engine.MaxValue + 1);
            int b = random.Next(this._engine.MinValue, this._engine.MaxValue + 1);
            return this._engine.CreateColor(r, g, b, this._engine.MaxValue);
        }
    }
}
