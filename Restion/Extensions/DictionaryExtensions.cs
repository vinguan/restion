using System.Collections.Generic;
using System.Linq;

namespace Restion.Extensions
{
    /// <summary>
    /// Contains the extensions methods for Dictionary
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Transform a dictionary into a query string
        /// </summary>
        /// <param name="sourceDictionary">The source dictionary</param>
        /// <returns>The query string mounted</returns>
        public static string ToQueryString(this IDictionary<string, string> sourceDictionary)
        {
            if (sourceDictionary == null || sourceDictionary.Count == 0)
                return string.Empty;

            var list = sourceDictionary.Select(item => item.Key + "=" + item.Value).ToList();

            return "?" + string.Join("&", list);
        }
    }
}
