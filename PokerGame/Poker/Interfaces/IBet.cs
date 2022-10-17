using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    interface IBet
    {
        List<Card> StartBetting(IReadOnlyList<Player> players, IDealer dealer);
    }
}
