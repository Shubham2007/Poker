using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;

namespace PokerGame.Poker
{
    class Dealer : IDealer
    {
        private readonly IDeck _deck;
        private bool _flopCalled = false;
        private bool _turnCalled = false;
        private bool _riverCalled = false;

        public Dealer(IDeck deck)
        {
            _deck = deck;
        }

        public void ShuffleCards()
            => _deck.ShuffleCards();

        public Card DealCard()
            => _deck.GetCard();

        public List<Card> GetFlop()
        {
            if(_flopCalled)
            {
                throw new InvalidOperationException("Flop cannot be called twice");
            }

            _flopCalled = true;
            List<Card> cards = new();
            for (int index = 0; index < 3; index++)
            {
                cards.Add(DealCard());
            }

            return cards;
        }

        public Card GetTurn()
        {
            if(!_flopCalled)
            {
                throw new InvalidOperationException("Please call the Flop first before calling Turn");
            }

            if(_turnCalled)
            {
                throw new InvalidOperationException("Turn cannot be called twice");
            }

            _turnCalled = true;
            return DealCard();
        }         

        public Card GetRiver()
        {
            if (!_flopCalled)
            {
                throw new InvalidOperationException("Please call the Flop first");
            }

            if (!_turnCalled)
            {
                throw new InvalidOperationException("Please call Turn first before calling River");
            }

            if (_riverCalled)
            {
                throw new InvalidOperationException("River cannot be called twice");
            }

            _riverCalled = true;
            return DealCard();
        }
    }
}
