using System;
using System.Collections.Generic;

namespace G11.TourSelector.ConsoleApp
{
    public static class IEnumerableExtensions
    {
        public static void WriteInConsole<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
