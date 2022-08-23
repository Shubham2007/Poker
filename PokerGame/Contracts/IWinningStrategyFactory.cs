using PokerGame.Poker;
using PokerGame.Poker.Interfaces;
using System.Collections.Generic;

namespace PokerGame.Contracts
{
    public interface IWinningStrategyFactory
    {
        IWinningStrategy GetWinningStrategy(IReadOnlyCollection<Card> cards);
    }
}
