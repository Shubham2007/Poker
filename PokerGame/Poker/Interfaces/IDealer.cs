using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    public interface IDealer
    {
        void ShuffleCards();
        Card DealCard();
        
        List<Card> GetFlop();
        Card GetTurn();
        Card GetRiver();
    }
}
