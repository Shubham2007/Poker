using System;
using System.Linq;
using System.Collections.Generic;
using PokerGame.Enums;
using PokerGame.Poker.Interfaces;
using PokerGame.Extensions;

namespace PokerGame.Poker.Winning
{
    class WinningStrategy : WinningStrategyBase, IWinningStrategy
    {
        /// <summary>
        /// Check for royal flush for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckRoyalFlush(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            (bool isStraightFlush, IReadOnlyList<Card> best5) = CheckStraightFlush(cards);

            // Royal Flush possible
            if(isStraightFlush)
            {
                if(best5.GetMinimumCardValue() == CardValue.Ten && best5.GetMaximumCardValue() == CardValue.A) // Royal Flush
                {
                    return (true, best5);
                }

                return (false, null);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for straight flush for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckStraightFlush(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            (bool isFlush, IReadOnlyList<Card> best5) = CheckFlush(cards);

            // Straight flush possible
            if (isFlush) 
            {
                // Flush + Straight = Straight Flush
                if(best5.IsSequential(x => (int)x.Value))
                {
                    return (true, best5);
                }

                return (false, null);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for four of a kind for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckFourOfAKind(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            if(HasAnyGroupWithExactDesiredCount(cards, x => x.Value, 4))
            {
                List<Card> best5;
                List<Card> groupOf4 = GetSimplifiedGrouping(cards, x => x.Value).First(x => x.ToList().Count == 4);
                Card remainingHighestCard = cards.Except(groupOf4).OrderByDescending(x => x.Value).First();
                best5 = groupOf4.Append(remainingHighestCard).ToList();

                return (true, best5);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for full house for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckFullHouse(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            IEnumerable<List<Card>> groupedCards = GetSimplifiedGrouping(cards, x => x.Value);

            if (groupedCards.Count(x => x.Count == 3) == 2 || (groupedCards.Count(x => x.Count == 3) == 1 && groupedCards.Count(x => x.Count == 2) <= 1))
            {
                List<Card> best5;
                List<Card> groupOf3 = GetFirstItemFromSimplifiedOrderedGroup(cards, x => x.Value, x => x.Count == 3);
                List<Card> groupOf2 = cards.Except(groupOf3).OrderByDescending(x => x.Value).GroupBy(x => x.Value).Select(x => x.ToList()).First().Take(2).ToList(); // GetOrderedSimplifiedGrouping(can be used if it is extension)
                best5 = groupOf3.Concat(groupOf2).ToList();

                return (true, best5);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for flush for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckFlush(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            // Flush Confirmed
            if (HasAnyGroupWithMinimumDesiredCount(cards, x => x.Suit, 5))
            {
                List<Card> best5Cards = GetFirstItemFromSimplifiedOrderedGroup(cards, x => x.Value, x => x.Count >= 5).Take(5).ToList();
                return (true, best5Cards);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for straight for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckStraight(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            List<List<Card>> allSequences = GetAllSequences(cards);
            if(allSequences.Any(x => x.Count >= 5)) // Straight Confirmed
            {
                List<Card> best5 = allSequences.OrderByDescending(x => x.Count).First().OrderByDescending(x => x.Value).Take(5).ToList();
                return (true, best5);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for three of a kind for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckThreeOfAKind(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            if (HasAnyGroupWithMinimumDesiredCount(cards, x => x.Value, 3))
            {
                List<Card> best5;
                List<Card> groupedCards = GetFirstItemFromSimplifiedOrderedGroup(cards, x => x.Value, x => x.Count >= 3);
                List<Card> remainingHighestCards = cards.Except(groupedCards).OrderByDescending(x => x.Value).Take(5 - groupedCards.Count).ToList();
                best5 = groupedCards.Concat(remainingHighestCards).ToList();

                return (true, best5);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for two pairs for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckTwoPairs(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            // If two pairs are possible
            if (HasMultipleGroupWithMinimumDesiredCount(cards, x => x.Value, 2, 2))
            {
                List<Card> best5;
                List<Card> groupedCards = GetOrderedSimplifiedGrouping(cards, x => x.Value).Where(x => x.Count >= 2).Take(2).SelectMany(_ => _).ToList();

                if (groupedCards.Count == 5)
                    return (true, groupedCards);

                if (groupedCards.Count == 6)
                {
                    _ = groupedCards.Remove(groupedCards.Last());
                    return (true, groupedCards);
                }
                    
                Card remainingHighestCard = cards.Except(groupedCards).OrderByDescending(x => x.Value).First();
                best5 = groupedCards.Append(remainingHighestCard).ToList();

                return (true, best5);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for pair for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckPair(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            if (HasAnyGroupWithMinimumDesiredCount(cards, x => x.Value, 2))
            {
                List<Card> best5;
                List<Card> groupedCards = GetFirstItemFromSimplifiedOrderedGroup(cards, x => x.Value, x => x.Count >= 2);
                List<Card> remainingHighestCards = cards.Except(groupedCards).OrderByDescending(x => x.Value).Take(5 - groupedCards.Count).ToList();
                best5 = groupedCards.Concat(remainingHighestCards).ToList();

                return (true, best5);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for high card for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, IReadOnlyList<Card>) CheckHighCard(in IReadOnlyList<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            List<Card> best5 = cards.OrderByDescending(x => x.Value).Take(5).ToList();

            return (true, best5);
        }
    }
}
