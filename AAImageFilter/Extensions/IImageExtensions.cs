using AAImageFilter.Interfaces;

namespace AAImageFilter.Extensions
{
    public static class IImageExtensions
    {
        public static bool OutOfBounds(this IImage image, int x, int y) => x < 0 || x >= image.Width || y < 0 || y >= image.Height;
    }
}
