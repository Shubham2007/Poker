using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Poker;
using PokerGame.UnitTests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static PokerGame.UnitTests.Common.CommonUtility;

namespace PokerGame.UnitTests
{
    [TestClass]
    public class PlayerUnitTests
    {
        private readonly Player _player;
        private readonly Common.CommonUtility _utility;

        public PlayerUnitTests()
        {
            _utility = new();
            _player = new(playerId: 0); // Any id dosent matter(All oprations are related to player with id 0)
        }

        [TestMethod]
        public void RecieveCard_WhenCalledOneTime_PlayerRecievesTheCard()
        {
            // Arrange
            Card card = GetRandomCard();

            // Act & Assert
            Assert.That.DoesNotThrow(() => _player.RecieveCard(card));
        }

        [TestMethod]
        public void RecieveCard_WhenCalledTwoTime_PlayerRecievesAnotherCard()
        {
            // Arrange
            Queue<Card> cards = _utility.GetRandomCards(numberOfCardsRequired: 2);

            // Act & Assert
            Assert.That.DoesNotThrow(() => _player.RecieveCard(cards.Dequeue())); // First Card
            Assert.That.DoesNotThrow(() => _player.RecieveCard(cards.Dequeue())); // Second Card
        }

        [TestMethod]
        public void RecieveCard_WhenCalledMoreThanTwoTimes_Throws_ArgumentException()
        {
            // Arrange
            Queue<Card> cards = _utility.GetRandomCards(numberOfCardsRequired: 3);

            // Act & Assert
            Assert.That.DoesNotThrow(() => _player.RecieveCard(cards.Dequeue())); // First Card
            Assert.That.DoesNotThrow(() => _player.RecieveCard(cards.Dequeue())); // Second Card
            Assert.ThrowsException<ArgumentException>(() => _player.RecieveCard(cards.Dequeue())); // Third Card throw error
        }

        [TestMethod]
        public void GetHand_WhenPlayerHaveTwoCards_Returns_TwoCards()
        {
            // Arrange
            Queue<Card> cards = _utility.GetRandomCards(numberOfCardsRequired: 2);
            List<Card> originalCards = cards.ToList();
            _player.RecieveCard(cards.Dequeue()); // Distribute first card
            _player.RecieveCard(cards.Dequeue()); // Distribute second card

            // Act
            List<Card> playerCards = _player.GetHand();

            // Assert
            Assert.IsNotNull(playerCards);
            CollectionAssert.AllItemsAreNotNull(playerCards);
            CollectionAssert.AllItemsAreUnique(playerCards);
            CollectionAssert.AreEqual(playerCards, originalCards);
        }

        [TestMethod]
        [DynamicData(nameof(GetCardData), DynamicDataSourceType.Method)]
        public void GetHand_WhenCalledWithLessThanTwoCards_Throws_ArgumentException(IEnumerable<Card> cards)
        {
            // Arrange
            foreach (Card card in cards)
            {
                _player.RecieveCard(card);
            }

            // Act & Assert 
            Assert.ThrowsException<ArgumentException>(() => _player.GetHand());
        }

        private static IEnumerable<object[]> GetCardData()
        {
            return new List<object[]>
            {
                new object[] { Array.Empty<Card>() },
                new object[] {new List<Card>(new Common.CommonUtility().GetRandomCards(1)) }
            };
        }
    }
}
