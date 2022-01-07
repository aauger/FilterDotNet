using AAImageFilter.Interfaces;

namespace AAImageFilter.Extensions
{
    public static class IImageExtensions
    {
        public static bool OutOfBounds(this IImage image, int x, int y) => x < 0 || x >= image.Width || y < 0 || y >= image.Height;

        public static IImage Clone(this IImage image, Func<int, int, IImage> imageCreator)
        {
            IImage output = imageCreator(image.Width, image.Height);
            Parallel.For(0, image.Width, (int x) => 
            {
                Parallel.For(0, image.Height, (int y) => 
                {
                    output.SetPixel(x, y, image.GetPixel(x, y));
                });
            });
            return output;
        }
    }
}
