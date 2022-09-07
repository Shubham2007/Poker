using PokerGame.Enums;
using PokerGame.Poker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Extensions
{
    static class CardExtension
    {
        public static CardValue GetMinimumCardValue(this List<Card> cards)
        {
            cards.ValidateCollectionNotNullOrEmpty();
            return cards.Min(x => x.Value);
        }

        public static CardValue GetMaximumCardValue(this List<Card> cards)
        {
            cards.ValidateCollectionNotNullOrEmpty();
            return cards.Max(x => x.Value);
        }

        public static void PrintCards(this List<Card> cards)
        {
            cards.ValidateCollectionNotNullOrEmpty();
            Console.WriteLine(string.Join(", ", cards));
        }
    }
}
