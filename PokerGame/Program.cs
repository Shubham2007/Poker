using PokerGame.Poker;
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
            Console.WriteLine("Welcome to poker club: ");
            Console.WriteLine("Please enter the number of players");
            int numberOfPlayers = Convert.ToInt32(Console.ReadLine());

            PokerTable table = new(numberOfPlayers);
            _ = Task.Run(() => table.StartGame());
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
                Console.WriteLine($"Best Combination: {winners[index].WinningPriority}, Best Cards: {winners[index].Best5Cards}");
                Console.WriteLine("--------------------------------------------" + Environment.NewLine);
            }
        }
    }
}
