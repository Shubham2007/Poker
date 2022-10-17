using PokerGame.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Poker
{
    class BetResult
    {
        public List<PlayerWinnigPriority> PlayerWinnigPriorities { get; set; }
    }

    class PlayerWinnigPriority
    {
        public Player Player { get; set; }
        public WinningPriority WinningPriority { get; set; }

        private IReadOnlyList<Card> _best5Cards;
        public IReadOnlyList<Card> Best5Cards
        {
            get { return _best5Cards; }
            set
            {
                _best5Cards = value;
                TotalValueOfCards = _best5Cards.Select(x => (int)x.Value).Sum();
                HighestCard = _best5Cards.OrderByDescending(x => x.Value).First();
            }
        }
        public Card HighestCard { get; private set; }
        public int TotalValueOfCards { get; private set; }
    }
}