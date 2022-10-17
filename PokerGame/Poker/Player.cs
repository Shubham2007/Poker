using System;
using System.Collections.Generic;

namespace PokerGame.Poker
{
    public class Player
    {
        private readonly List<Card> _hand;

        public Player(int playerId)
        {
            Id = playerId;
            _hand = new(capacity: 2);
        }

        public Player RecieveCard(Card card)
        {
            if (_hand.Count >= 2)
                throw new ArgumentException("Player cannot recieve more than 2 cards");

            _hand.Add(card);
            return this;
        }

        public IReadOnlyList<Card> GetHand()
        {
            if (_hand.Count != 2)
                throw new ArgumentException("Player must have 2 cards");

            return _hand.AsReadOnly();
        }

        public int Id { get; }
    }
}
