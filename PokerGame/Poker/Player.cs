using System;
using System.Collections.Generic;

namespace PokerGame.Poker
{
    class Player
    {
        private readonly List<Card> _hand;

        public Player(int playerId)
        {
            Id = playerId;
            _hand = new(capacity: 2);
        }

        public void RecieveCard(Card c)
            => _hand.Add(c);

        public List<Card> GetHand()
        {
            if (_hand.Count != 2)
                throw new ArgumentException("Player must have 2 cards");

            return _hand;
        }

        public int Id { get; }
    }
}
