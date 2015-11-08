using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restion.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}
