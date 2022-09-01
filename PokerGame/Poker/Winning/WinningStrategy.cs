﻿using System;
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
        public (bool, List<Card>) CheckRoyalFlush(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            (bool isStraightFlush, List<Card> best5) = CheckStraightFlush(cards);

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
        public (bool, List<Card>) CheckStraightFlush(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            (bool isFlush, List<Card> best5) = CheckFlush(cards);

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
        public (bool, List<Card>) CheckFourOfAKind(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            if(cards.GroupBy(x => x.Value).Any(x => x.ToList().Count == 4))
            {
                List<Card> best5 = new(capacity: 5);
                List<Card> groupOf4 = cards.GroupBy(x => x.Value).Select(x => x.ToList()).First(x => x.ToList().Count == 4);
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
        public (bool, List<Card>) CheckFullHouse(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            IEnumerable<List<Card>> groupedCards = cards.GroupBy(x => x.Value).Select(x => x.ToList());

            if (groupedCards.Where(x => x.Count == 3).Count() == 2 || (groupedCards.Where(x => x.Count == 3).Count() == 1 && groupedCards.Where(x => x.Count == 2).Count() <= 1))
            {
                List<Card> best5 = new(capacity: 5);
                List<Card> groupOf3 = cards.OrderByDescending(x => x.Value).GroupBy(x => x.Value).Select(x => x.ToList()).Where(x => x.Count == 3).First();
                List<Card> groupOf2 = cards.Except(groupOf3).OrderByDescending(x => x.Value).GroupBy(x => x.Value).Select(x => x.ToList()).First().Take(2).ToList();
                best5 = groupOf3.Concat(groupOf2).ToList();

                return (true, best5);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for flush for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, List<Card>) CheckFlush(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            // Flush Confirmed
            if (cards.GroupBy(x => x.Suit).Any(x => x.ToList().Count >= 5))
            {
                List<Card> best5Cards = cards.OrderByDescending(x => x.Value).GroupBy(x => x.Suit).Select(x => x.ToList()).First(x => x.Count >= 5).Take(5).ToList();
                return (true, best5Cards);
            }

            return (false, null);
        }

        /// <summary>
        /// Check for straight for all 7 cards(5 cards on table + 2 cards in players hand)
        /// </summary>
        /// <param name="cards"></param>
        public (bool, List<Card>) CheckStraight(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            List<List<Card>> allSequences = GetAllSequences(cards);
            if(allSequences.Any(x => x.Count >= 5))
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
        public (bool, List<Card>) CheckThreeOfAKind(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            if (cards.GroupBy(x => x.Value).Any(x => x.ToList().Count >= 3))
            {
                List<Card> best5 = new(capacity: 5);
                List<Card> groupedCards = cards.OrderByDescending(x => x.Value).GroupBy(x => x.Value).Select(x => x.ToList()).First(x => x.ToList().Count >= 3);
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
        public (bool, List<Card>) CheckTwoPairs(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            // If two pairs are possible
            if (cards.GroupBy(x => x.Value).Select(x => x.ToList()).Where(x => x.Count >= 2).Count() >= 2)
            {
                List<Card> best5 = new(capacity: 5);
                List<Card> groupedCards = cards.OrderByDescending(x => x.Value).GroupBy(x => x.Value).Select(x => x.ToList()).Where(x => x.Count >= 2).Take(2).SelectMany(x => x).ToList();

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
        public (bool, List<Card>) CheckPair(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            if (cards.GroupBy(x => x.Value).Any(x => x.ToList().Count >= 2))
            {
                List<Card> best5 = new(capacity: 5);
                List<Card> groupedCards = cards.OrderByDescending(x => x.Value).GroupBy(x => x.Value).Select(x => x.ToList()).First(x => x.ToList().Count >= 2);
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
        public (bool, List<Card>) CheckHighCard(in List<Card> cards)
        {
            if (cards.Count != 7)
                throw new ArgumentException("Cards Count should be 7");

            List<Card> best5 = cards.OrderByDescending(x => x.Value).Take(5).ToList();

            return (true, best5);
        }
    }
}