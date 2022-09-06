using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Poker.Winning
{
    abstract class WinningStrategyBase
    {
        protected static List<List<Card>> GetAllSequences(List<Card> list)
        {
            list = list.OrderBy(x => x.Value).ToList();

            var result = new List<List<Card>>();
            var tempList = new List<Card> { list[0] };
            Card lastResult = list[0];
            for (var index = 1; index < list.Count; index++)
            {
                if ((int)lastResult.Value + 1 == (int)list[index].Value)
                    tempList.Add(list[index]);
                else
                {
                    result.Add(tempList);
                    tempList = new List<Card> { list[index] };
                }
                lastResult = list[index];
            }
            result.Add(tempList);
            return result;
        }

        protected static bool HasAnyGroupWithMinimumDesiredCount<T, TKey>(IEnumerable<T> items, Func<T, TKey> selector, int minimunItemsInGroup)
        {
            return items.GroupBy(selector).Any(x => x.ToList().Count >= minimunItemsInGroup);
        }

        protected static bool HasAnyGroupWithExactDesiredCount<T, TKey>(IEnumerable<T> items, Func<T, TKey> selector, int exactItemsInGroup)
        {
            return items.GroupBy(selector).Any(x => x.ToList().Count == exactItemsInGroup);
        }

        protected static bool HasMultipleGroupWithMinimumDesiredCount<T, TKey>(IEnumerable<T> items, Func<T, TKey> selector, int minimunItemsInGroup, int minimumGroupCount)
        {
            return items.GroupBy(selector).Select(x => x.ToList()).Count(x => x.Count >= minimunItemsInGroup) >= minimumGroupCount;
        }

        protected static IEnumerable<List<T>> GetOrderedSimplifiedGrouping<T, TKey>(IEnumerable<T> items, Func<T, TKey> selector)
        {
            return items.OrderByDescending(selector).GroupBy(selector).Select(x => x.ToList());
        }

        protected static IEnumerable<List<T>> GetSimplifiedGrouping<T, TKey>(IEnumerable<T> items, Func<T, TKey> selector)
        {
            return items.GroupBy(selector).Select(x => x.ToList());
        }

        protected static List<T> GetFirstItemFromSimplifiedOrderedGroup<T, TKey>(IEnumerable<T> items, Func<T, TKey> selector, Func<List<T>, bool> predicate)
        {
            return GetOrderedSimplifiedGrouping(items, selector).First(predicate);
        }
    }
}
