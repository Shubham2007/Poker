using PokerGame.Contracts;
using PokerGame.Enums;
using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Poker
{
    class PokerHandEvaluator : IPokerHandEvaluator
    {
        private readonly IWinningStrategyFactory _winningStrategyFactory;

        public PokerHandEvaluator(IWinningStrategyFactory winningStrategyFactory)
        {
            _winningStrategyFactory = winningStrategyFactory ?? throw new ArgumentNullException(nameof(winningStrategyFactory));
        }

        /// <summary>
        /// Evalute the best possible winning strategy for each player
        /// </summary>
        /// <returns></returns>
        public BetResult Evaluate(IReadOnlyList<Card> cardsOnTable, IReadOnlyList<Player> players)
        {
            IReadOnlyList<Card> _cardsOntable = cardsOnTable ?? throw new ArgumentNullException(nameof(cardsOnTable)); // Memory Waste
            IReadOnlyList<Player> _players = players ?? throw new ArgumentNullException(nameof(players));

            List<PlayerWinnigPriority> winnigPriority = new();

            foreach(Player player in _players)
            {
                IReadOnlyList<Card> totalCards = player.GetHand().Concat(_cardsOntable).ToList();
                (WinningPriority winningPriority, IReadOnlyList<Card> best5) = GetWinningPriorityOfPlayer(totalCards, _winningStrategyFactory.GetWinningStrategy(totalCards));
                PlayerWinnigPriority priority = new()
                {
                    Player = player,
                    Best5Cards = best5,
                    WinningPriority = winningPriority
                };
                winnigPriority.Add(priority);
            }

            return new BetResult { PlayerWinnigPriorities = winnigPriority };
        }

        private static (WinningPriority winningPriority, IReadOnlyList<Card> best5Cards) GetWinningPriorityOfPlayer(IReadOnlyList<Card> totalCards, IWinningStrategy winningStrategy)
        {
            WinningPriority priority = default;
            IReadOnlyList<Card> best5 = default;

            // Check straight
            (bool isStraight, IReadOnlyList<Card> best5Straight) = winningStrategy.CheckStraight(totalCards);
            if (isStraight)
            {
                SetWinningPriority(ref priority, WinningPriority.Straight, ref best5, best5Straight);
            }

            // Check flush
            (bool isFlush, IReadOnlyList<Card> best5Flush) = winningStrategy.CheckFlush(totalCards);
            if (isFlush)
            {
                SetWinningPriority(ref priority, WinningPriority.Flush, ref best5, best5Flush);
            }

            // Check straight flush and royal flush
            if (isStraight && isFlush)
            {
                (bool isStraightFlush, IReadOnlyList<Card> best5SF) = winningStrategy.CheckStraightFlush(totalCards);
                if (isStraightFlush)
                {
                    SetWinningPriority(ref priority, WinningPriority.StraightFlush, ref best5, best5SF);

                    // Best hand in the game: Royal Flush
                    (bool isRoyalFlush, IReadOnlyList<Card> best5RF) = winningStrategy.CheckRoyalFlush(totalCards);
                    if (isRoyalFlush)
                    {
                        SetWinningPriority(ref priority, WinningPriority.RoyalFlush, ref best5, best5RF);
                        return (priority, best5);
                    }
                }
            }

            // Check pair
            (bool isPair, IReadOnlyList<Card> best5Pair) = winningStrategy.CheckPair(totalCards);
            if (isPair)
            {
                SetWinningPriority(ref priority, WinningPriority.Pair, ref best5, best5Pair);

                // Check three of a kind
                (bool isThreeOfAKind, IReadOnlyList<Card> best5TOAK) = winningStrategy.CheckThreeOfAKind(totalCards);
                if (isThreeOfAKind)
                {
                    SetWinningPriority(ref priority, WinningPriority.ThreeOfAKind, ref best5, best5TOAK);

                    // Check four of a kind
                    (bool isFourOfAKind, IReadOnlyList<Card> best5FOAK) = winningStrategy.CheckFourOfAKind(totalCards);
                    if (isFourOfAKind)
                    {
                        SetWinningPriority(ref priority, WinningPriority.FourOfAKind, ref best5, best5FOAK);
                        priority = WinningPriority.FourOfAKind;
                        best5 = best5FOAK;
                    }
                }

                // Check two pairs
                (bool isTwoPairs, IReadOnlyList<Card> best5TwoPairs) = winningStrategy.CheckTwoPairs(totalCards);
                if (isTwoPairs)
                {
                    SetWinningPriority(ref priority, WinningPriority.TwoPairs, ref best5, best5TwoPairs);

                    // Check full house
                    (bool isFullhouse, IReadOnlyList<Card> best5FullHouse) = winningStrategy.CheckFullHouse(totalCards);
                    if (isFullhouse)
                    {
                        SetWinningPriority(ref priority, WinningPriority.FullHouse, ref best5, best5FullHouse);
                    }
                }
            }

            //HIGH CARD (When no winning is possible)
            if(priority == default && best5 == default)
            {
                (_, IReadOnlyList<Card> best5HighCard) = winningStrategy.CheckHighCard(totalCards);
                priority = WinningPriority.HighCard;
                best5 = best5HighCard;
            }

            return (priority, best5);
        }

        public static void SetWinningPriority(ref WinningPriority previousPriority, in WinningPriority currentPriority, ref IReadOnlyList<Card> previousBest5, in IReadOnlyList<Card> currentBest5)
        {
            if (previousPriority != default && previousPriority > currentPriority)
                return;

            previousPriority = currentPriority;
            previousBest5 = currentBest5;
        }
    }
}
