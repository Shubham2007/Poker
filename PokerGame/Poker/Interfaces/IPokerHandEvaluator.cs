using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    interface IPokerHandEvaluator
    {
        BetResult Evaluate(IReadOnlyList<Card> cardsOnTable, IReadOnlyList<Player> players);
    }
}
