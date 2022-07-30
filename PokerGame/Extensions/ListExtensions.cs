using System;
using System.Collections.Generic;

namespace PokerGame.Extensions
{
    public static class ListExtensions
    {
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
    }
}
