using PokerGame.Poker;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PokerGame.Core.Comparers
{
    public class CardComparer : IEqualityComparer<Card>
    {
        public bool Equals(Card x, Card y)
        {
            return x.Suit == y.Suit && x.Value == y.Value;
        }

        public int GetHashCode([DisallowNull] Card obj)
        {
            return base.GetHashCode();
        }
    }
}
