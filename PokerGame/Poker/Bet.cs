using PokerGame.Helper;
using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PokerGame.Poker
{
    public class Bet : IBet
    {
        private List<Player> _players;
        private IDealer _dealer;
        private List<Card> _cardsOnTable;

        public Bet()
        {
            _cardsOnTable = new(capacity: 5);           
        }

        /// <summary>
        /// Start the betting on the table
        /// </summary>
        /// <returns></returns>
        public List<Card> StartBetting(List<Player> players, IDealer dealer)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players)); // For future reference( When player actually bet money before every step)
            _dealer = dealer ?? throw new ArgumentNullException(nameof(dealer));

            PrintPlayersHand();

            Thread.Sleep(1500);

            ConsoleHelper.PrintStringToConsole("Current Cards On Table: ");
            List<Card> flop = _dealer.GetFlop();
            PrintFlop(flop);

            Thread.Sleep(1500);

            Card turn = _dealer.GetTurn();
            PrintTurn(turn);

            Thread.Sleep(1500);

            Card river = _dealer.GetRiver();
            PrintRiver(river);

            ComposeAllCards(flop, turn, river);

            ConsoleHelper.PrintStringToConsole("Analyzing result...");
            Thread.Sleep(1000);
            return _cardsOnTable;
        }

        private void ComposeAllCards(List<Card> flop, Card turn, Card river)
        {
            _cardsOnTable.AddRange(flop);
            _cardsOnTable.Add(turn);
            _cardsOnTable.Add(river);
        }

        private static void PrintRiver(Card river)
            => ConsoleHelper.PrintObjectToConsole(river, false);

        private static void PrintTurn(Card turn)
            => ConsoleHelper.PrintObjectToConsole(turn, false);

        private static void PrintFlop(List<Card> flop)
            => ConsoleHelper.PrintListToConsole<Card>(flop, false);

        private void PrintPlayersHand()
        {
            ConsoleHelper.PrintStringToConsole("Current Players Hand: ");

            for(int index = 0; index < _players.Count; index++)
            {
                ConsoleHelper.PrintStringToConsole($"Player {index}");
                ConsoleHelper.PrintListToConsole(_players[index].GetHand(), false);
            }
            ConsoleHelper.PrintStringToConsole(null);
        }
    }
}
