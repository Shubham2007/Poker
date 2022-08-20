using Microsoft.Extensions.DependencyInjection;
using PokerGame.Helper;
using PokerGame.Poker;
using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider provider = DependencyInjectionHelper.SetupDI();

            Console.WriteLine("Welcome to poker club: ");
            Console.WriteLine("Please enter the number of players");
            int numberOfPlayers = Convert.ToInt32(Console.ReadLine());

            IPokerHandEvaluator pokerHandEvaluator = provider.GetRequiredService<IPokerHandEvaluator>();
            IBet bet = provider.GetRequiredService<IBet>();
            IDealer dealer = provider.GetRequiredService<IDealer>();
            PokerTable table = new(pokerHandEvaluator, bet, dealer);

            _ = Task.Run(() => table.StartGame(numberOfPlayers));
            table.GetWinners += (IReadOnlyList<PlayerWinnigPriority> winners) => ShowWinners(in winners);
            Console.WriteLine("Game started already. Winners will be annoumced shortly");

            Console.ReadKey();
        }

        private static void ShowWinners(in IReadOnlyList<PlayerWinnigPriority> winners)
        {
            // Considering winner cannot be empty
            for (int index = 0; index < winners.Count(); index++)
            {
                Console.WriteLine($"Winner Number: {index + 1}, Player ID: {winners[index].Player.Id}");
                Console.WriteLine($"Best Combination: {winners[index].WinningPriority}");
                PrintCards(winners[index].Best5Cards);
                Console.WriteLine("--------------------------------------------" + Environment.NewLine);
            }
        }

        private static void PrintCards(List<Card> best5Cards)
        {
            foreach(Card card in best5Cards)
            {
                Console.Write(card);
            }
        }
    }
}
