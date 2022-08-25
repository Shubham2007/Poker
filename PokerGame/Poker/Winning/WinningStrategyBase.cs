using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Poker.Winning
{
    abstract class WinningStrategyBase
    {
        public static List<List<Card>> GetAllSequences(List<Card> list)
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
    }
}
