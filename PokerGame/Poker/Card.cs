using PokerGame.Enums;

namespace PokerGame.Poker
{
    class Card
    {
        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }
        public Suit Suit { get; set; }
        public CardValue Value { get; set; }

        /// <summary>
        /// Represent current card
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}
