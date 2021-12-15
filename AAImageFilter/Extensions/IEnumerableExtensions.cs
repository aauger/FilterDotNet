using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAImageFilter.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T At<T>(this IEnumerable<T> enumerable, int idx) => enumerable.ElementAt(idx);
    }
}
