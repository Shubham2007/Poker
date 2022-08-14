using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    interface IWinningStrategy
    {
        (bool, List<Card>) CheckRoyalFlush(in List<Card> cards);
        (bool, List<Card>) CheckStraightFlush(in List<Card> cards);
        (bool, List<Card>) CheckFourOfAKind(in List<Card> cards);
        (bool, List<Card>) CheckFullHouse(in List<Card> cards);
        (bool, List<Card>) CheckFlush(in List<Card> cards);
        (bool, List<Card>) CheckStraight(in List<Card> cards);
        (bool, List<Card>) CheckThreeOfAKind(in List<Card> cards);
        (bool, List<Card>) CheckTwoPairs(in List<Card> cards);
        (bool, List<Card>) CheckPair(in List<Card> cards);
        (bool, List<Card>) CheckHighCard(in List<Card> cards);
    }
}
