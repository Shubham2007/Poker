using PokerGame.Contracts;
using PokerGame.Poker;
using PokerGame.Poker.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Core.Factories
{
    class WinningStrategyFactory : IWinningStrategyFactory
    {
        public IWinningStrategy GetWinningStrategy(IReadOnlyCollection<Card> cards)
            => cards.Any(x => x.Value == Enums.CardValue.A) 
                ? new WinningStrategyWithAce() 
                : new WinningStrategy();
    }
}
