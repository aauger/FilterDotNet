using FilterDotNet.Interfaces;

namespace FilterDotNet.Drawing
{
    public class Graphics
    {
        private IImage _instance;

        public static Graphics FromIImage(IImage input) => new Graphics(input);
        private Graphics(IImage input)
        {
            this._instance = input;
        }

        public void PlotLine(Point first, Point second, IColor color)
        {
            throw new NotImplementedException();
        }
    }
}
