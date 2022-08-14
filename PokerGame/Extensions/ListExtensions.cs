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
    }
}
