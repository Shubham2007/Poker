using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerGame.Enums;
using PokerGame.Poker;
using PokerGame.Poker.Interfaces;

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
        public void DealCard_WhenCalled_ReturnCard()
        {
            // Arrange
            Card deckCard = new(Suit.Club, CardValue.J);
            _deck.Setup(x => x.GetCard()).Returns(deckCard);

            // Act
            Card card = _dealer.DealCard();

            // Assert
            Assert.IsNotNull(card);
            Assert.AreEqual(card.Suit, deckCard.Suit);
            Assert.AreEqual(card.Value, deckCard.Value);
        }
    }
}
