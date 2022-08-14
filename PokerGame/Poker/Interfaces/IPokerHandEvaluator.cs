using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    interface IPokerHandEvaluator
    {
        BetResult Evaluate(List<Card> cardsOnTable, List<Player> players);
    }
}
