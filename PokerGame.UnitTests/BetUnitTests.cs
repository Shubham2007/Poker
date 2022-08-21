using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerGame.Core.Comparers;
using PokerGame.Poker;
using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static PokerGame.UnitTests.Common.CommonUtility;

namespace PokerGame.UnitTests.Extensions
{
    [TestClass]
    public class BetUnitTests
    {
        private readonly Mock<IDealer> _dealer;
        private readonly Bet _bet;
        private readonly Common.CommonUtility _utility;

        public BetUnitTests()
        {
            _bet = new();
            _dealer = new();
            _utility = new();
        }

        [TestMethod]
        public void StartBetting_WhenCalledWithNullPlayer_Throws_ArgumentNullException()
        {
            // Act And Assert
            Assert.ThrowsException<ArgumentNullException>(() => _bet.StartBetting(null, _dealer.Object));
        }

        [TestMethod]
        public void StartBetting_WhenCalledWithNullDealer_Throws_ArgumentNullException()
        {
            // Arrange
            List<Player> players = new List<Player>
            {
                new Player(playerId: 0)
            };

            // Act And Assert
            Assert.ThrowsException<ArgumentNullException>(() => _bet.StartBetting(players, null));
        }

        [TestMethod]
        public void StartBetting_WhenCalledWithValidArguments_Returns_FiveCardsOnTable()
        {
            // Arrange
            Queue<Card> flop = _utility.GetRandomCards(numberOfCardsRequired: 3, true);
            _dealer.Setup(x => x.GetFlop()).Returns(DeepClone(flop));

            Card turn = _utility.GetRandomCard();
            _dealer.Setup(x => x.GetTurn()).Returns(DeepClone(turn));

            Card river = _utility.GetRandomCard();
            _dealer.Setup(x => x.GetRiver()).Returns(DeepClone(river));

            List<Card> originalCards = ComposeCards(flop, turn, river);

            List<Player> players = new()
            {
                new Player(playerId: 0).RecieveCard(_utility.GetRandomCard()).RecieveCard(_utility.GetRandomCard()),
                new Player(playerId: 1).RecieveCard(_utility.GetRandomCard()).RecieveCard(_utility.GetRandomCard())
            };

            CardEqualityComparer comparer = new(); // To comapre cards

            // Act
            List<Card> cardsOnTable = _bet.StartBetting(players, _dealer.Object);

            // Assert
            Assert.IsNotNull(cardsOnTable);
            CollectionAssert.AllItemsAreNotNull(cardsOnTable);
            CollectionAssert.AllItemsAreUnique(cardsOnTable);
            Assert.That.AllItemsAreDifferent(comparer, cardsOnTable.ToArray());
            CollectionAssert.AreEqual(originalCards, cardsOnTable);
        }

        private static List<Card> ComposeCards(Queue<Card> flop, Card turn, Card river)
        {
            List<Card> cards = new();
            cards = cards.Concat(flop).Append(turn).Append(river).ToList();
            return cards;
        }
    }
}
