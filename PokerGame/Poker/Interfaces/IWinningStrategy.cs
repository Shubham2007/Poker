using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    public interface IWinningStrategy
    {
        (bool, IReadOnlyList<Card>) CheckRoyalFlush(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckStraightFlush(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckFourOfAKind(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckFullHouse(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckFlush(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckStraight(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckThreeOfAKind(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckTwoPairs(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckPair(in IReadOnlyList<Card> cards);
        (bool, IReadOnlyList<Card>) CheckHighCard(in IReadOnlyList<Card> cards);
    }
}
