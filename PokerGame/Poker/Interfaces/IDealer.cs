using System.Collections.Generic;

namespace PokerGame.Poker.Interfaces
{
    interface IDealer
    {
        void ShuffleCards();
        Card DealCard();
        
        List<Card> GetFlop();
        Card GetTurn();
        Card GetRiver();
    }
}
