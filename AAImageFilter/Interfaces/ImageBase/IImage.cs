namespace FilterDotNet.Interfaces
{
    public interface IImage
    {
        /* Properties */
        int Width { get; }
        int Height { get; }

        /* Mutation */
        void SetPixel(int x, int y, IColor color);
        IColor GetPixel(int x, int y);
    }
}
