using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles the elements in the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="random"></param>
        public static void Shuffle<T>(this List<T> list, Random random)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                var index = random.Next(0, list.Count);
                list[i] = list[index];
                list[index] = temp;
            }
        }

        /// <summary>
        /// Check if integers is in sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static bool IsSequential<T>(this List<T> list, Func<T, int> selector)
        => list
            .Select(selector)
            .Zip(list.Select(selector)
            .Skip(1), (a, b) => (a + 1) == b)
            .All(x => x);

        /// <summary>
        /// Collection group by selector and order the groups descending and return first group items
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static List<TSource> FirstMaxGroupedItem<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null || !source.Any())
                throw new ArgumentNullException(nameof(source));

            List<TSource> firstItemOfMaxGroup = source.OrderByDescending(keySelector)
                .GroupBy(keySelector)
                .Select(x => x.ToList())
                .First();

            return firstItemOfMaxGroup;
        }

        /// <summary>
        /// Throw if collection is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public static void ValidateCollectionNotNullOrEmpty<T>(this IEnumerable<T> items)
        {
            if (items == null || !items.Any())
                throw new ArgumentNullException(nameof(items), "List items are either null or empty");
        }
    }
}
