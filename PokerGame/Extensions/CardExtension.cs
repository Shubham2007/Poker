using PokerGame.Enums;
using PokerGame.Poker;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Extensions
{
    static class CardExtension
    {
        public static CardValue GetMinimumCardValue(this List<Card> cards)
            => cards.Min(x => x.Value);

        public static CardValue GetMaximumCardValue(this List<Card> cards)
            => cards.Max(x => x.Value);
    }
}
