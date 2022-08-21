using PokerGame.Enums;
using PokerGame.Extensions;
using PokerGame.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PokerGame.UnitTests.Common
{
    class CommonUtility
    {
        private readonly HashSet<Suit> suits;
        private readonly HashSet<CardValue> cardValues;
        private readonly List<Card> cards;

        public CommonUtility()
        {
            cards = new();
            suits = new();
            cardValues = new();
        }

        public Queue<Card> GetRandomCards(in int numberOfCardsRequired, bool resetDeck = false)
        {
            if (resetDeck)
                Reset();

            Queue<Card> cards = new();

            if (numberOfCardsRequired > 3 || numberOfCardsRequired < 1)
                throw new ArgumentException("Number of cards must be 1 to 3");

            for (int index = 0; index < numberOfCardsRequired; index++)
            {
                cards.Enqueue(GetRandomCard());
            }

            return cards;
        }

        public Card GetRandomCard(bool resetDeck = false)
        {
            if (resetDeck)
                Reset();
          
            CardValue value = Enum<CardValue>.GetRandomValue();
            Suit suit = Enum<Suit>.GetRandomValue();

            suits.Add(suit);
            cardValues.Add(value);

            Card card = new(suit, value);
            if (cards.Contains(card))
            {
                return GetRandomCard();
            }

            cards.Add(card);
            return card;
        }

        public static TObject DeepClone<TObject>(TObject obj) where TObject: class, ICloneable
        {
            TObject clonedObject = (TObject)obj.Clone();
            return clonedObject;
        }

        public static List<TObject> DeepClone<TObject>(IEnumerable<TObject> objects) where TObject : class, ICloneable
        {
            if (objects == null || !objects.Any())
                throw new ArgumentNullException(nameof(objects));

            List<TObject> clonedObjects = new(capacity: objects.Count());
            foreach (TObject @object in objects)
            {
                TObject clonedObject = (TObject)@object.Clone();
                clonedObjects.Add(clonedObject);
            }
            
            return clonedObjects;
        }

        #region Private Methods
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
        #endregion
    }
}
