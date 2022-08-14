using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    interface IPokerTable
    {
        delegate void Winners(IReadOnlyList<PlayerWinnigPriority> winners);
        void StartGame(int totalPlayers);
    }
}
