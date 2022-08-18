using PokerGame.Enums;
using PokerGame.Extensions;
using PokerGame.Poker;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PokerGame.UnitTests.Common
{
    class CommonUtility
    {
        private readonly HashSet<Suit> suits;
        private readonly HashSet<CardValue> cardValues;

        public CommonUtility()
        {
            suits = new();
            cardValues = new();
        }

        public Queue<Card> GetRandomCards(in int numberOfCardsRequired)
        {
            Queue<Card> cards = new();

            if (numberOfCardsRequired > 3 || numberOfCardsRequired < 1)
                throw new ArgumentException("Number of cards must be 1 to 3");

            for (int index = 0; index < numberOfCardsRequired; index++)
            {
                Suit suit = Enum<Suit>.GetRandomValue(exceptList: suits);
                CardValue value = Enum<CardValue>.GetRandomValue(exceptList: cardValues);

                suits.Add(suit);
                cardValues.Add(value);

                cards.Enqueue(new(suit, value));
            }

            Reset();
            return cards;
        }

        public static Card GetRandomCard()
        {
            Suit suit = Enum<Suit>.GetRandomValue();
            CardValue value = Enum<CardValue>.GetRandomValue();

            Card card = new(suit, value);
            return card;
        }

        private void Reset()
        {
            ResetFields();
            // Reset properties;
        }

        private void ResetFields()
        {
            FieldInfo[] fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (int index = 0; index < fields.Length; ++index)
            {
                object obj = Activator.CreateInstance(fields[index].FieldType);
                fields[index].SetValue(this, obj);
            }
                
        }
    }
}
