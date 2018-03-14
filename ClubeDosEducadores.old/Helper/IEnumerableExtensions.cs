using System;
using System.Collections.Generic;
using System.Linq;

namespace Helper
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static IEnumerable<T> For<T>(this IEnumerable<T> list, Action<T, int, int> action)
        {
            var count = list.Count();

            for (int i = 0; i < count; i++)
            {
                var item = list.ElementAt(i);
                action(item, count, i);
            }


            return list;
        }

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> list, Func<T, T> function)
        {
            var newList = new HashSet<T>();

            foreach (var item in list)
            {
                newList.Add(function(item));
            }

            return newList;
        }

        public static void Foreach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }
    }
}
