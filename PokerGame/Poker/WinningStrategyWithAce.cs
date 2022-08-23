using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;

namespace PokerGame.Poker
{
    class WinningStrategyWithAce : IWinningStrategy
    {
        public (bool, List<Card>) CheckFlush(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckFourOfAKind(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckFullHouse(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckHighCard(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckPair(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckRoyalFlush(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckStraight(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckStraightFlush(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckThreeOfAKind(in List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public (bool, List<Card>) CheckTwoPairs(in List<Card> cards)
        {
            throw new NotImplementedException();
        }
    }
}
