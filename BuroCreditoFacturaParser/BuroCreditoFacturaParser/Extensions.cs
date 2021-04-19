using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jorgelig.BuroCreditoFacturaParser
{
    public static class Extensions
    {
        public static List<List<T>> BreakDown<T>(this IEnumerable<T> source, int blockSize)
        {
            var result = new List<List<T>>();

            var temp = new List<T>();
            foreach (var item in source)
            {
                temp.Add(item);
                if (temp.Count == blockSize)
                {
                    result.Add(new List<T>(temp));
                    temp.Clear();
                }
            }
            if (temp.Count > 0) result.Add(new List<T>(temp));
            return result;
        }

        public static string RemoveAllNonAlphanumeric(this string source)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            source = rgx.Replace(source, "");
            return source;
        }
    }
}
