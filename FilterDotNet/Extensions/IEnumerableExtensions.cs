namespace FilterDotNet.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T At<T>(this IEnumerable<T> enumerable, int idx) => enumerable.ElementAt(idx);
    }
}
