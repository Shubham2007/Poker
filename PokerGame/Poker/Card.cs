using PokerGame.Enums;
using System;
using static PokerGame.Core.Cloners.ReflectionCloning;

namespace PokerGame.Poker
{
    public class Card : IComparable, IEquatable<Card>, ICloneable
    {
        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }
        public Suit Suit { get; set; }
        public CardValue Value { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is not Card other)
                throw new ArgumentException("Obj is not Card type");

            if (other.Value == Value)
                return 0;

            return Value > other.Value ? 1 : -1;
        }

        public bool Equals(Card other)
            => other.Suit == Suit && other.Value == Value;

        /// <summary>
        /// Represent current card
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Card);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public object Clone()
        {
            return Clone<Card>(this);
        }

        //public static bool operator ==(Card a, Card b)
        //    => a.Value == b.Value;

        //public static bool operator !=(Card a, Card b)
        //    => a.Value != b.Value;

        //public static bool operator <(Card a, Card b)
        //    => a.Value < b.Value;

        //public static bool operator >(Card a, Card b)
        //    => a.Value > b.Value;
    }
}
