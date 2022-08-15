using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerGame.Enums;
using PokerGame.Extensions;
using PokerGame.Poker;
using PokerGame.Poker.Interfaces;
using System.Collections.Generic;

namespace PokerGame.UnitTests
{
    [TestClass]
    public class DealerUnitTests
    {
        private readonly Mock<IDeck> _deck;
        private readonly Dealer _dealer;
        private HashSet<Suit> suits;
        private HashSet<CardValue> cardValues;

        public DealerUnitTests()
        {
            _deck = new();
            _dealer = new(_deck.Object);
        }
        

        [TestMethod]
        public void DealCard_WhenCalled_ReturnCard()
        {
            // Arrange
            Card deckCard = GetRandonCard();
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
            _deck.Setup(x => x.GetCard()).Returns(GetRandonCard());

            // Act
            List<Card> flop = _dealer.GetFlop();

            // Assert
            Assert.IsNotNull(flop);
            Assert.AreEqual(3, flop.Count);

        }

        private Card GetRandonCard()
        {
            Suit suit = Enum<Suit>.GetRandomValue(exceptList: suits);
            CardValue value = Enum<CardValue>.GetRandomValue(exceptList: cardValues);

            suits.Add(suit);
            cardValues.Add(value);

            Card card = new(suit, value);
            return card;
        }
    }
}
