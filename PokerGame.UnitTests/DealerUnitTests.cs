using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerGame.Core.Comparers;
using PokerGame.Enums;
using PokerGame.Extensions;
using PokerGame.Poker;
using PokerGame.Poker.Interfaces;
using PokerGame.UnitTests.Extensions;
using System;
using System.Collections.Generic;

namespace PokerGame.UnitTests
{
    [TestClass]
    public class DealerUnitTests
    {
        private readonly Mock<IDeck> _deck;
        private readonly Dealer _dealer;
        private readonly HashSet<Suit> suits;
        private readonly HashSet<CardValue> cardValues;

        public DealerUnitTests()
        {
            _deck = new();
            _dealer = new(_deck.Object);
            suits = new();
            cardValues = new();
        }
        

        [TestMethod]
        public void DealCard_WhenCalled_ReturnCard()
        {
            // Arrange
            Card deckCard = GetRandomCard();
            _deck.Setup(x => x.GetCard()).Returns(deckCard);

            // Act
            Card card = _dealer.DealCard();

            // Assert
            Assert.IsNotNull(card);
            Assert.AreEqual(card.Suit, deckCard.Suit);
            Assert.AreEqual(card.Value, deckCard.Value);
        }

        [TestMethod]
        public void GetFlop_WhenCalled_ReturnFirst3Cards()
        {
            // Arrange
            Queue<Card> cards = new();
            for (int index = 0; index < 3; index++)
            {
                cards.Enqueue(GetRandomCard());
            }
            _deck.Setup(x => x.GetCard()).Returns(cards.Dequeue);
            CardComparer comparer = new(); // To comapre cards
            //IEqualityComparer<Card> comparer = EqualityComparerFactory.Create<Card>((x, y) => x.Suit == y.Suit && x.Value == y.Value, x => x.Value.GetHashCode()); // To comapre cards (NOT WORKING)

            // Act
            List<Card> flop = _dealer.GetFlop();

            // Assert
            Assert.IsNotNull(flop);
            Assert.AreEqual(3, flop.Count);
            Assert.That.AllItemsAreDifferent(comparer, flop.ToArray());
        }

        [TestMethod]
        public void GetFlop_CalledAgain_ReturnInvalidOperationException()
        {
            // Arrange
            SetFlopCalledToTrue(_dealer); // Just to create a scenario that flop is called already on the same instance

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _dealer.GetFlop());
        }

        #region Private Methods

        private static void SetFlopCalledToTrue(Dealer dealer)
            => SetPrivateProperty(dealer, "_flopCalled", true);

        private static void SetPrivateProperty<T>(object obj, string propertyName, T val)
            => obj.SetPrivatePropertyValue(propertyName, val);

        private Card GetRandomCard()
        {
            Suit suit = Enum<Suit>.GetRandomValue(exceptList: suits);
            CardValue value = Enum<CardValue>.GetRandomValue(exceptList: cardValues);

            suits.Add(suit);
            cardValues.Add(value);

            Card card = new(suit, value);
            return card;
        }

        #endregion
    }
}
