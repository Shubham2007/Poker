using PokerGame.Enums;
using PokerGame.Extensions;
using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;

namespace PokerGame.Poker
{
    class Deck : IDeck
    {
        private readonly List<Card> _cards;

        public Card this[int index]
        {
            get
            {
                if (index > 51 || index < 0)
                    throw new ArgumentException("Index shoule be positive and less than 51"); // Since there is only 52 cards in deck(0 to 51)

                return _cards[index];
            }
        }

        public Deck()
        {
            _cards = new(capacity: 52);
            InitializeDeck();
        }

        public void ShuffleCards()
        {
            Random r = new();
            _cards.Shuffle(r);
        }

        public Card GetCard()
        {
            Random r = new();
            var index = r.Next(0, _cards.Count);
            Card card = _cards[index];
            RemoveCardFromDeck(card);
            return card;
        }

        private void RemoveCardFromDeck(Card card)
            => _cards.Remove(card);

        /// <summary>
        /// Creates a deck of 52 cards
        /// </summary>
        private void InitializeDeck()
        {
            foreach(Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach(CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Card card = new(suit, value);
                    _cards.Add(card);
                }
            }
        }
    }
}
