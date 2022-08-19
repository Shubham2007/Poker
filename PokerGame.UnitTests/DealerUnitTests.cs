using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerGame.Core.Comparers;
using PokerGame.Poker;
using PokerGame.Poker.Interfaces;
using PokerGame.UnitTests.Extensions;
using System;
using System.Collections.Generic;
using static PokerGame.UnitTests.Common.CommonUtility;

namespace PokerGame.UnitTests
{
    [TestClass]
    public class DealerUnitTests
    {
        private readonly Mock<IDeck> _deck;
        private readonly Dealer _dealer;

        public DealerUnitTests()
        {
            _deck = new();
            _dealer = new(_deck.Object);
        }

        [TestMethod]
        public void ShuffleCards_WhenCalled_ShuffleCardsInDeck()
        {
            // Act & Assert
            Assert.That.DoesNotThrow(() => _dealer.ShuffleCards());
        }

        [TestMethod]
        public void DealCard_WhenCalled_ReturnCard()
        {
            // Arrange
            Card deckCard = GetRandomCard();
            _deck.Setup(x => x.GetCard()).Returns(DeepClone(deckCard));

            // Act
            Card card = _dealer.DealCard();

            // Assert
            Assert.IsNotNull(card);
            Assert.AreEqual(deckCard, card);
        }

        [TestMethod]
        public void GetFlop_WhenCalled_ReturnFirst3Cards()
        {
            // Arrange
            Queue<Card> cards = new Common.CommonUtility().GetRandomCards(numberOfCardsRequired: 3);
            
            _deck.Setup(x => x.GetCard()).Returns(cards.Dequeue);
            CardEqualityComparer comparer = new(); // To comapre cards
            //IEqualityComparer<Card> comparer = EqualityComparerFactory.Create<Card>((x, y) => x.Suit == y.Suit && x.Value == y.Value, x => x.Value.GetHashCode()); // To comapre cards (NOT WORKING)

            // Act
            List<Card> flop = _dealer.GetFlop();

            // Assert
            Assert.IsNotNull(flop);
            Assert.AreEqual(3, flop.Count);
            CollectionAssert.AllItemsAreNotNull(flop);
            CollectionAssert.AllItemsAreUnique(flop);
            Assert.That.AllItemsAreDifferent(comparer, flop.ToArray());
        }

        [TestMethod]
        public void GetFlop_CalledAgain_Throws_InvalidOperationException()
        {
            // Arrange
            SetFlopCalledToTrue(_dealer); // Just to create a scenario that flop is called already on the same instance

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _dealer.GetFlop());
        }

        [TestMethod]
        public void GetTurn_WhenCalledBeforeFlop_Throws_InvalidOperationException()
        {        
            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _dealer.GetTurn());
        }

        [TestMethod]
        public void GetTurn_WhenCalledAfterFlop_Return_FourthCardAfterFlop()
        {
            // Arrange
            SetFlopCalledToTrue(_dealer);
            Card deckCard = GetRandomCard();
            _deck.Setup(x => x.GetCard()).Returns(DeepClone(deckCard));

            // Act
            Card card = _dealer.GetTurn();

            // Assert
            Assert.IsNotNull(card);
            Assert.AreEqual(deckCard, card);            
        }

        [TestMethod]
        public void GetTurn_WhenCalledAgain_Throws_InvalidOperationException()
        {
            // Arrange
            SetFlopCalledToTrue(_dealer);
            SetTurnCalledToTrue(_dealer);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _dealer.GetTurn());
        }

        [TestMethod]
        public void GetRiver_WhenCalledBeforeFlop_Throws_InvalidOperationException()
        {
            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _dealer.GetRiver());
        }

        [TestMethod]
        public void GetRiver_WhenCalledAfterFlopBeforeTurn_Throws_InvalidOperationException()
        {
            // Arrange
            SetFlopCalledToTrue(_dealer);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _dealer.GetRiver());
        }

        [TestMethod]
        public void GetRiver_WhenCalledAgainAfterFlopAndTurn_Throws_InvalidOperationException()
        {
            // Arrange
            SetFlopCalledToTrue(_dealer);
            SetTurnCalledToTrue(_dealer);
            SetRiverCalledToTrue(_dealer);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _dealer.GetRiver());
        }

        [TestMethod]
        public void GetRiver_WhenCalledAfterFlopAndTurn_Return_FifthAndLastCardA()
        {
            // Arrange
            SetFlopCalledToTrue(_dealer);
            SetTurnCalledToTrue(_dealer);
            Card deckCard = GetRandomCard();
            _deck.Setup(x => x.GetCard()).Returns(DeepClone(deckCard));

            // Act
            Card card = _dealer.GetRiver();

            // Assert
            Assert.IsNotNull(card);
            Assert.AreEqual(deckCard, card);
        }

        #region Private Methods

        private static void SetFlopCalledToTrue(Dealer dealer)
            => SetPrivateProperty(dealer, "_flopCalled", true);

        private static void SetTurnCalledToTrue(Dealer dealer)
            => SetPrivateProperty(dealer, "_turnCalled", true);

        private static void SetRiverCalledToTrue(Dealer dealer)
            => SetPrivateProperty(dealer, "_riverCalled", true);

        private static void SetPrivateProperty<T>(object obj, string propertyName, T val)
            => obj.SetPrivatePropertyValue(propertyName, val);

        //private Card GetRandomCard()
        //{
        //    Suit suit = Enum<Suit>.GetRandomValue(exceptList: suits);
        //    CardValue value = Enum<CardValue>.GetRandomValue(exceptList: cardValues);

        //    suits.Add(suit);
        //    cardValues.Add(value);

        //    Card card = new(suit, value);
        //    return card;
        //}

        #endregion
    }
}
