using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;

namespace PokerGame.Poker.Winning
{
    class WinningStrategyWithAce : WinningStrategyBase, IWinningStrategy
    {
        public (bool, IReadOnlyList<Card>) CheckRoyalFlush(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckStraightFlush(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckFourOfAKind(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckFullHouse(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckFlush(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckStraight(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckThreeOfAKind(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckTwoPairs(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckPair(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, IReadOnlyList<Card>) CheckHighCard(in IReadOnlyList<Card> cards)
        {
            throw new NotImplementedException();
        }
    }
}
