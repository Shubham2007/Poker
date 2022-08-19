using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Poker;
using PokerGame.UnitTests.Extensions;

namespace PokerGame.UnitTests
{
    [TestClass]
    public class DeckUnitTests
    {
        private readonly Deck _deck;

        public DeckUnitTests()
        {
            _deck = new();
        }

        [TestMethod]
        public void ShuffleCards_WhenCalled_ShuffleCardsInDeck()
        {
            // Act & Assert
            Assert.That.DoesNotThrow(() => _deck.ShuffleCards());
        }

        [TestMethod]
        public void GetCard_WhenCalled_Returns_RandomCardFromDeck()
        {
            // Act
            Card card = _deck.GetCard();

            // Assert
            Assert.IsNotNull(card);
            Assert.IsTrue(card.Value != default); // Default = 0 if no 0 is in Enum
        }
    }
}
