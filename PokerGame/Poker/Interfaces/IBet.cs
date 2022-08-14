using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    interface IBet
    {
        List<Card> StartBetting(List<Player> players, IDealer dealer);
    }
}
